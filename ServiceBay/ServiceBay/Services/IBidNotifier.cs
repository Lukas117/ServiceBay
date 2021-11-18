using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Services
{
    public interface IBidNotifier
    {
        public void Attach(IBidObserver observer);
        public void Detach(IBidObserver observer);
        public void Notify(Bid bid, String email);
    }
}
