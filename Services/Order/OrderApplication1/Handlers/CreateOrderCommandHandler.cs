using System.Net;
using MediatR;
using OnlineStudyShared;
using OrderApplication.Commands;
using OrderApplication.Dtos;
using OrderDomain.OrderAggregate;
using OrderInfrastructure;

namespace OrderApplication.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseDto<CreatedOrderDto>>
{
    private readonly OrderDbContext _context;
    
    public CreateOrderCommandHandler(OrderDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseDto<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(request.AddressDto.Province,request.AddressDto.District,
            request.AddressDto.Line,request.AddressDto.ZipCode,request.AddressDto.Line);
         
        Order newOrder = new(request.BuyerId,newAddress);
        foreach (var item in request.OrderItems)
        {
            newOrder.AddOrderItem(item.ProductId,item.ProductName,item.PictureUrl,item.Price);
        }
        
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();
        
        return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto{OrderId = newOrder.Id},200);
    }
}