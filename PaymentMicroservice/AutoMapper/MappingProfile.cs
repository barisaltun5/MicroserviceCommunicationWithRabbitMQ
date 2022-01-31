using AutoMapper;
using PaymentMicroservice.DAL.Entities;
using PaymentMicroservice.Models.RequestModels;
using PaymentMicroservice.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, Payment>()
                .ForMember(
                    d => d.Price,
                    c => c.MapFrom(
                        s => s.Price
                    ))
                .ForMember(
                    d => d.OrderId,
                    c => c.MapFrom(
                        s => s.Id
                    ))
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Payment, PaymentResponseModel>()
                .ForMember(
                d => d.Status,
                c => c.MapFrom(
                       s => s.Status
                    ));
        }
    }
}
