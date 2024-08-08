using System;
using System.Net.NetworkInformation;
using BiddingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidsController : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Bid>> PlaceBid(string auctionId, int amount)
    {
        var auction = await DB.Find<Auction>().OneAsync(auctionId);

        if (auction == null)
        {
            // TODO: check with auction service if it has the auction
            return NotFound();
        }

        if (auction.Seller == User.Identity.Name)
        {
            return BadRequest("You cannot bid on your own auction.");
        }

        var bid = new Bid
        {
            Amount = amount,
            AuctionId = auctionId,
            Bidder = User.Identity.Name
        };

        if (auction.AuctionEnd < DateTime.UtcNow)
        {
            bid.bidStatus = BidStatus.Finished;
        }

        else 
        {
            var highBid = await DB.Find<Bid>()
                .Match(a => a.AuctionId == auctionId)
                .Sort(b => b.Descending(x => x.Amount))
                .ExecuteFirstAsync();
            
            if (highBid != null && amount > highBid.Amount || highBid == null)
            {
                bid.bidStatus = amount > auction.ReservePrice ? BidStatus.Accepted : BidStatus.AcceptedBelowReserve;
            }

            if (highBid != null && bid.Amount <= highBid.Amount)
            {
                bid.bidStatus = BidStatus.TooLow;
            }
            }

        await DB.SaveAsync(bid);
        return Ok(bid);
    }

    [HttpGet("{auctionId}")]
    public async Task<ActionResult<List<Bid>>> GetBidsForAuction(string auctionId)
    {
        var auction = await DB.Find<Auction>().OneAsync(auctionId);

        if (auction == null)
        {
            // TODO: check with auction service if it has the auction
            return NotFound();
        }

        var bids = await DB.Find<Bid>()
            .Match(a => a.AuctionId == auctionId)
            .Sort(b => b.Descending(x => x.BidTime))
            .ExecuteAsync();

        return bids;
    }
}
