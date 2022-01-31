using AutoMapper;
using OrderMicroservice.DAL.Entities;
using OrderMicroservice.Models.RequestModels;
using OrderMicroservice.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderRequestModel, Order>()
                .ForMember(
                    d => d.Price,
                    c => c.MapFrom(
                        s => s.Price
                    ));
            CreateMap<Order, OrderResponseModel>()
                .ForMember(
                d => d.Status,
                c => c.MapFrom(
                       s => s.Status
                    ));
        }
    }
}
