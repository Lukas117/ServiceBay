using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Models
{
    public static class StaticVar
    {
        public static List<double?> prevPrices = new List<double?>();
        public static List<double?> newPrices = new List<double?>();
        public static Person currentUser;
    }
}
