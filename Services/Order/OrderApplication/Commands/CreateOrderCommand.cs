using MediatR;
using OnlineStudyShared;
using OrderApplication.Dtos;

namespace OrderApplication.Commands;

public class CreateOrderCommand : IRequest<ResponseDto<CreatedOrderDto>>
{
    public string BuyerId { get; set; }
    
    public AddressDto AddressDto { get; set; }
    
    public List<OrderItemDto> OrderItems { get; set; }
    
}