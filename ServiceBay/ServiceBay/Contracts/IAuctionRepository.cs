﻿using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    interface IAuctionRepository
    {
        public Task<AuctionForCreationDto> CreateAuction(AuctionForCreationDto auctionDto);
        public Task<Auction> GetAuction();
        public Task<Auction> UpdateAuction();
        public Task<Auction> DeleteAuction();
    }
}
