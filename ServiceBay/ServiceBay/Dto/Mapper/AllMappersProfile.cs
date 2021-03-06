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
            CreateMap<AuctionForCreationDto, Auction>();
            CreateMap<Person, PersonForCreationDto>();
            CreateMap<PersonForCreationDto, Person>();
            CreateMap<AddressForCreationDto, Address>();
            CreateMap<CityForCreationDto, City>();
            CreateMap<AuctionForUpdateDto, Auction>();
        }

    }
}
