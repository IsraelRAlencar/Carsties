import React from 'react'
import { Auction, AuctionFinished } from '../types'
import Link from 'next/link'
import Image from 'next/image'
import { finished } from 'stream'

type Props = {
    finishedAuction: AuctionFinished
    auction: Auction
}

export default function AuctionFinishedToast({auction, finishedAuction}: Props) {
    return (
        <Link href={`/auctions/details/${auction.id}`} className='flex flex-col items-center'>
            <div className="flex flex-row items-center gap-2">
                <Image
                    src={auction.imageUrl}
                    alt='image'
                    height={80}
                    width={80}
                    className='rounded-lg w-auto h-auto'
                />
            </div>
            <div className="flex flex-col">
                <span>Auction for {auction.make} {auction.model} has finished</span>
                {finishedAuction.itemSold && finishedAuction.amount ? (
                    <p>Congratulations to {finishedAuction.winner} who has won this auction for $${finishedAuction.amount}</p>
                ) : (
                    <p>The auction has ended without a winner</p>
                )}
            </div>
        </Link>
    )
}
