
using System.Net;
using System.Net.Http.Json;
using AuctionService.Data;
using AuctionService.DTOs;
using Contracts;
using MassTransit.SagaStateMachine;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionService.IntegrationTests;

[Collection("Shared collection")]
public class AuctionBusTests : IAsyncLifetime
{
    private readonly CustomWebAppFactory _factory;
    private readonly HttpClient _httpClient;
    private ITestHarness _testHarness;
    private const string GT_ID = "afbee524-5972-4075-8800-7d1f9d7b0a0c";

    public AuctionBusTests(CustomWebAppFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
        _testHarness = _factory.Services.GetTestHarness();
    }
    
    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
        DbHelper.ReinitDbForTests(db);
        return Task.CompletedTask;
    }

    private CreateAuctionDto GetAuctionForCreate()
    {
        return new CreateAuctionDto
        {
            Make = "test",
            Model = "testModel",
            ImageUrl = "test",
            Color = "test",
            Mileage = 10,
            Year = 10,
            ReservePrice = 10
        };
    }

    [Fact]
    public async void CreateAuction_WithValidObject_ShouldPublishAuctionCreated()
    {
        var auction = GetAuctionForCreate();
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

        var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

        response.EnsureSuccessStatusCode();
        Assert.True(await _testHarness.Published.Any<AuctionCreated>());
    }

    [Fact]
    public async void CreateAuction_WithInvalidObject_ShouldNotPublishAuctionCreated()
    {
        var auction = GetAuctionForCreate();
        auction.Make = null;
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

        var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

        Assert.False(await _testHarness.Published.Any<AuctionCreated>());
    }

    [Fact]
    public async void UpdateAuction_WithValidObject_ShouldPublishAuctionUpdated()
    {
        var auction = GetAuctionForCreate();
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

        var response = await _httpClient.PutAsJsonAsync($"api/auctions/{GT_ID}", auction);

        response.EnsureSuccessStatusCode();
        Assert.True(await _testHarness.Published.Any<AuctionUpdated>());
    }

    // [Fact]
    // public async void UpdateAuction_WithInvalidObjectId_ShouldNotPublishAuctionUpdated()
    // {
    //     var auction = null as CreateAuctionDto;
    //     _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

    //     var response = await _httpClient.PutAsJsonAsync($"api/auctions/{Guid.NewGuid()}", auction);

    //     Assert.False(await _testHarness.Published.Any<AuctionUpdated>());
    // }

    [Fact]
    public async void DeleteAuction_WithValidId_ShouldPublishAuctionDeleted()
    {
        var auction = GetAuctionForCreate();
        auction.Make = null;
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

        var response = await _httpClient.DeleteAsync($"api/auctions/{GT_ID}");

        response.EnsureSuccessStatusCode();
        Assert.True(await _testHarness.Published.Any<AuctionDeleted>());
    }

    // [Fact]
    // public async void DeleteAuction_WithInvalidId_ShouldNotPublishAuctionDeleted()
    // {
    //     var auction = GetAuctionForCreate();
    //     auction.Make = null;
    //     _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

    //     var response = await _httpClient.DeleteAsync($"api/auctions/{Guid.NewGuid()}");

    //     Assert.False(await _testHarness.Published.Any<AuctionUpdated>());
    // }
}
