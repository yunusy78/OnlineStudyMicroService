using CartMicroServices.Dtos;
using CartMicroServices.Services;
using MassTransit;
using Newtonsoft.Json;
using OnlineStudyShared.Message.Command;
using OnlineStudyShared.Message.Event;
using OnlineStudyShared.Services;

namespace CartMicroServices.Consumer;

public class CreateCartMessageEventConsumer : IConsumer<CourseUpdateEvent>
{
    
    private readonly RedisService _redisService;
    private readonly ISharedIdentity _sharedIdentity;
    private readonly ICartService _cartService;
    
    
    public CreateCartMessageEventConsumer(RedisService redisService, ISharedIdentity sharedIdentity , ICartService cartService)
    {
        _redisService = redisService;
        _sharedIdentity = sharedIdentity;
        _cartService = cartService;
    }
    
    
    public async Task Consume(ConsumeContext<CourseUpdateEvent> context)
    {
        // Kullanıcının sepetini bul
        var allCartKeys = await _redisService.GetDb().ListRangeAsync("carts");

        var tasks = allCartKeys.Select(async cartKey =>
        {
            var cartData = await _cartService.GetCart2(cartKey);

            if (cartData != null)
            {
                // Sepet verilerini güncelle
                foreach (var item in cartData.Data.cartItems)
                {
                    // Örnek olarak, item'ın ID'si eşleştiği durumda güncelleme yapabilirsiniz.
                    if (item.CourseId == context.Message.CourseId)
                    {
                        //item.PictureUrl = context.Message.NewCourseImage;
                        item.Price = context.Message.NewCoursePrice;
                        item.CourseName = context.Message.NewCourseName;
                    }
                }


                // Güncellenmiş sepet verilerini Redis'e geri yaz
                await _cartService.SaveOrUpdateCart(cartData.Data);
            }
        });

        await Task.WhenAll(tasks);

    }
}