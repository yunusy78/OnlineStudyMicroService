using AutoMapper;
using OrderApplication.Dtos;
using OrderDomain.OrderAggregate;

namespace OrderApplication.Mapping;

public class CustomMapper : Profile
{
    public CustomMapper()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }
    
}