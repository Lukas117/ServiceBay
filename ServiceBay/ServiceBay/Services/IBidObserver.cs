using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Services
{
    public interface IBidObserver
    {
        public void updateBid(Bid bid);
    }
}
