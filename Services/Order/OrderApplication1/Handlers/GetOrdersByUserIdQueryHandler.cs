using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStudyShared;
using OrderApplication.Dtos;
using OrderApplication.Mapping;
using OrderApplication.Queries;
using OrderInfrastructure;

namespace OrderApplication.Handlers;

public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, ResponseDto<List<OrderDto>>>
{
    private readonly OrderDbContext _context;
    
    public GetOrdersByUserIdQueryHandler(OrderDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Include(x => x.OrderItems)
            .Include(x=>x.Address)
            .Where(x => x.BuyerId == request.UserId).ToListAsync();
        if (!orders.Any())
        {
            return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(), 200);
        }
        var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
        return ResponseDto<List<OrderDto>>.Success(ordersDto, 200);
       
    }
}