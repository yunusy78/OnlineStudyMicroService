using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStudyShared;
using OrderApplication.Dtos;
using OrderApplication.Mapping;
using OrderApplication.Queries;
using OrderInfrastructure;

namespace OrderApplication.Handlers;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, ResponseDto<List<OrderDto>>>
{
    private readonly OrderDbContext _context;
    
    public GetAllOrdersQueryHandler(OrderDbContext context)
    {
        _context = context;
    }
    

    public async Task<ResponseDto<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _context.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.Address)
                .ToListAsync();

            if (orders == null)
            {
                // Veri yoksa işlemi burada ele alabilirsiniz.
                return ResponseDto<List<OrderDto>>.Fail("No data found", 404);
            }
            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return ResponseDto<List<OrderDto>>.Success(ordersDto, 200);
            // Burada verileri kullan veya sonuçları görüntüle.
        }
        catch (Exception ex)
        {
            // Hata mesajını veya detaylarını yazdır.
            Console.WriteLine(ex.Message);
        }
        
        return ResponseDto<List<OrderDto>>.Fail("An error occurred while getting all orders", 500);
        
    }
}