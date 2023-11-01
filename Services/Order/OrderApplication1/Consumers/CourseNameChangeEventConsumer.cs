using MassTransit;
using Microsoft.EntityFrameworkCore;
using OnlineStudyShared.Message.Event;
using OrderInfrastructure;

namespace OrderApplication1.Consumers;

public class CourseNameChangeEventConsumer : IConsumer<CourseNameChangeEvent>
{
    private readonly OrderDbContext _orderDbContext;
    
    public CourseNameChangeEventConsumer(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }
    
    public async Task Consume(ConsumeContext<CourseNameChangeEvent> context)
    {
        var orderItems = await _orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToListAsync();
        orderItems.ForEach(x =>
        {
            x.UpdateOrderItem(context.Message.NewCourseName, x.PictureUrl, x.Price);
        });
        await _orderDbContext.SaveChangesAsync();
    }
}