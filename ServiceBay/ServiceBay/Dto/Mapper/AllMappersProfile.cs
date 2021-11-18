using System;
using AutoMapper;
using ServiceBay.Models;

namespace ServiceBay.Dto.Mapper
{
    public class AllMappersProfile : Profile
    {
        public AllMappersProfile()
        {
            CreateMap<Auction, AuctionForCreationDto>();
            
        }
    }
}
