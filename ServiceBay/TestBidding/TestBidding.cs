using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceBay.Contracts;
using ServiceBay.Controllers;
using ServiceBay.Models;
using Xunit;

namespace TestBidding
{
    public class TestBidding
    {
        private readonly ApiAuctionController auctionApi;
        private readonly ApiBidController bidApi;
        private readonly Mock<IAuctionRepository> auctionRepoMock = new Mock<IAuctionRepository>();
        private readonly Mock<IBidRepository> bidRepoMock = new Mock<IBidRepository>();

        public TestBidding()
        {
            auctionApi = new ApiAuctionController(auctionRepoMock.Object);
            bidApi = new ApiBidController(bidRepoMock.Object);
        }

        [Fact]
        public async Task GetAuctionById_ShouldReturnAuction_WhenAuctionExist()
        {
            //Arrange
            var auctionId = 1;
            var auctionName = "Test";
            var auctionDescription = "Test";
            var auctionStartingDate = DateTime.Now;
            var auctionEndDate = DateTime.Now.AddDays(1);
            var auctionSPrice = 10;
            var auctionPrice = 10;
            var auctionSellerId = 1;

            var auction = new Auction
            {
                Id = auctionId,
                AuctionName = auctionName,
                AuctionDescription = auctionDescription,
                StartingDate = auctionStartingDate,
                EndDate = auctionEndDate,
                StartingPrice = auctionSPrice,
                SellerId = auctionSellerId,
                Price = auctionPrice
            };

            auctionRepoMock.Setup(x => x.GetAuction(auctionId)).ReturnsAsync(auction);

            //Act
             var value = await auctionApi.GetAuction(auctionId);

            //Assert
            Assert.Equal(auctionId, value.Value.Id);
            Assert.Equal(auctionName, value.Value.AuctionName);
            Assert.Equal(auctionDescription, value.Value.AuctionDescription);
            Assert.Equal(auctionStartingDate, value.Value.StartingDate);
            Assert.Equal(auctionEndDate, value.Value.EndDate);
            Assert.Equal(auctionSPrice, value.Value.StartingPrice);
            Assert.Equal(auctionSellerId, value.Value.SellerId);
            Assert.Equal(auctionPrice, value.Value.Price);

        }

        [Fact]
        public async Task GetAuctionById_ShouldReturnNothing_WhenAuctionDoesNotExist()
        {
            //Arrange
            auctionRepoMock.Setup(x => x.GetAuction(0)).ReturnsAsync(() => null) ;

            //Act
            var value = await auctionApi.GetAuction(1000);

            //Assert
            var actionResult = Assert.IsType<ActionResult<Auction>>(value);
            Assert.IsType<NotFoundResult>(actionResult.Result);

        }

        [Fact]
        public async Task UpdatePrice_ShouldUpdateAuctionPrice_WhenAuctionAndBidIsValid()
        {
            //Arrange
            var auctionId = 1;
            var auctionName = "Test";
            var auctionDescription = "Test";
            var auctionStartingDate = DateTime.Now;
            var auctionEndDate = DateTime.Now.AddDays(1);
            var auctionSPrice = 10;
            var auctionPrice = 10;
            var auctionSellerId = 1;

            var auction = new Auction
            {
                Id = auctionId,
                AuctionName = auctionName,
                AuctionDescription = auctionDescription,
                StartingDate = auctionStartingDate,
                EndDate = auctionEndDate,
                StartingPrice = auctionSPrice,
                SellerId = auctionSellerId,
                Price = auctionPrice
            };

            var auctionPrice2 = 100;

            var auctionModified = new Auction
            {
                Id = auctionId,
                AuctionName = auctionName,
                AuctionDescription = auctionDescription,
                StartingDate = auctionStartingDate,
                EndDate = auctionEndDate,
                StartingPrice = auctionSPrice,
                SellerId = auctionSellerId,
                Price = auctionPrice2
            };


            var bidId = 1;
            var bidPrice = 100;
            var bidBuyer = 2;
            var bidAuctionid = 1;

            var bid = new Bid
            {
                Id = bidId,
                Price = bidPrice,
                BuyerId = bidBuyer,
                AuctionId = bidAuctionid
            };


            auctionRepoMock.Setup(x => x.GetAuction(auctionId)).ReturnsAsync(() => auctionModified);
            bidRepoMock.Setup(x => x.CreateBid(bid)).ReturnsAsync(() => 1);

            //Act
            var value = await bidApi.PostBid(bid);
            var auctionValue = await auctionApi.GetAuction(auctionId);
             
            var actionResult = Assert.IsType<ActionResult<Bid>>(value);
            Assert.IsType<OkResult>(actionResult.Result);

            Assert.Equal(auctionId, auctionValue.Value.Id);
            Assert.Equal(auctionName, auctionValue.Value.AuctionName);
            Assert.Equal(auctionDescription, auctionValue.Value.AuctionDescription);
            Assert.Equal(auctionStartingDate, auctionValue.Value.StartingDate);
            Assert.Equal(auctionEndDate, auctionValue.Value.EndDate);
            Assert.Equal(auctionSPrice, auctionValue.Value.StartingPrice);
            Assert.Equal(auctionSellerId, auctionValue.Value.SellerId);
            Assert.Equal(auctionPrice2, auctionValue.Value.Price);

        }

        

    }
}
