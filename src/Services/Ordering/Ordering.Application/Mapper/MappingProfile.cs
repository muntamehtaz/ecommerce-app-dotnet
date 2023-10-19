using AutoMapper;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            //CreateMap<Order, OrdersVm>().ReverseMap();
            //CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            //CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
