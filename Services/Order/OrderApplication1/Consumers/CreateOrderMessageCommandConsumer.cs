using MassTransit;
using OnlineStudyShared.Message.Command;
using OrderDomain.OrderAggregate;
using OrderInfrastructure;

namespace OrderApplication1.Consumers;

public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
{
    private readonly OrderDbContext _context;
    
    public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
    {
        _context = orderDbContext;
    }
    
    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        var newAddress = new Address(context.Message.Province,context.Message.District,
            context.Message.Line,context.Message.ZipCode,context.Message.Street);
        
        Order newOrder = new(context.Message.BuyerId,newAddress);
        foreach (var item in context.Message.OrderItems)
        {
            newOrder.AddOrderItem(item.ProductId,item.ProductName,item.PictureUrl,item.Price);
        }
        
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();
    }
}