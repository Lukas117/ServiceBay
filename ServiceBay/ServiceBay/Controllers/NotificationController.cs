using Microsoft.AspNetCore.Mvc;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Controllers
{
    public class NotificationController : Controller
    {
        public readonly MvcAuctionController mvc;

        public NotificationController()
        {
            mvc = new MvcAuctionController();
        }

        public JsonResult AllSellerAuctions(int sellerId)
        {
            return mvc.AllSellerAuctions(sellerId);
        }

        public JsonResult SetPrevPrices()
        {
            var auctions = mvc.AllAuctions();
            var prices = InsertIntoPrevPrices((IEnumerable<Auction>)auctions.Value);
            return Json(prices);
        }
        public JsonResult InsertIntoPrevPrices(IEnumerable<Auction> result)
        {
            var auctions = result.ToList();
            foreach (var a in auctions)
            {
                Lists.prevPrices.Add(a.Price);
            }
            return Json(Lists.prevPrices);
        }

        public void GetPrices(IEnumerable<Auction> result)
        {
            var auctions = result.ToList();
            Lists.newPrices.Clear();
            foreach (var a in auctions)
            {
                Lists.newPrices.Add(a.Price);
            }
        }

        //public JsonResult CompareAndGetBids()
        //{
        //    var auctions = mvc.AllAuctions();
        //    HashSet<Bid> newBids = new HashSet<Bid>();
        //    var x = GetBids((IEnumerable<Auction>)auctions.Value);
        //    newBids = (HashSet<Bid>)x.Value;
        //    //var res = GetBids((IEnumerable<Auction>)auctions.Value);
        //    //var newBids = (HashSet<Bid>)res.Value;
        //    var all = new HashSet<Bid>();
        //    List<Bid> inOrderNew = (List<Bid>)newBids.OrderByDescending(b => b.AuctionId);
        //    List<Bid> inOrderOld = (List<Bid>)prevBids.OrderByDescending(b => b.AuctionId);
        //    for (int i = 0; i < inOrderNew.Count; i++)
        //    {
        //        if (inOrderNew[i] != inOrderOld[i])
        //        {
        //            all.Add(inOrderOld[i]);
        //            inOrderOld[i] = inOrderNew[i];
        //        }
        //    }
        //    return Json(all);
        //}

        public JsonResult CompareAndGetPrices()
        {
            var auctions = mvc.AllAuctions();
            GetPrices((IEnumerable<Auction>)auctions.Value);
            var all = new List<double?>();
            for (int i = 0; i < Lists.newPrices.Count; i++)
            {
                if (Lists.prevPrices.Count != 0)
                {
                    if (!Lists.newPrices[i].Equals(Lists.prevPrices[i]))
                    {
                        all.Add(Lists.newPrices[i]);
                        Lists.prevPrices[i] = Lists.newPrices[i];
                    }
                }
            }
            return Json(all);
        }
    }
}
