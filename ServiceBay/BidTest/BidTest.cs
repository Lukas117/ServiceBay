using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ServiceBay.Models;

namespace BidTest
{
    [TestClass]
    public class BidTest
    {
        private Auction auction;
        private Bid bid;

        [TestInitialize]
        public void Setup()
        {
            auction = new Auction();
            bid = new Bid();
        }

        [TestCleanup]
        public void CleanUp()
        {
            auction = null;
        }

        [TestMethod]
        public void BidUpdateTest()
        {
            
        }
    }
}