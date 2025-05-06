using Domain.Entities.Identity;
using Domain.Entities.Order_Modules;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() {
            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();
            CreateMap<OrderItems, OrderItemDto>()
                .ForMember(d => d.Id, options => options.MapFrom(o => o.Product.Id))
                .ForMember(d => d.Name, options => options.MapFrom(o => o.Product.Name))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(o => o.Product.PictureUrl));
            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.PaymentStatus, options => options.MapFrom(o => o.PaymentStatus.ToString()))
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, options => options.MapFrom(o => o.SubTotal + o.DeliveryMethod.Price));
            CreateMap<DeliveryMethod, DeliveryMethodResult>().ReverseMap();
        }

    }
}
