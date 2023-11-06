using Business.Abstract;
using Business.Concrete;
using Business.Handler;
using Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions;

public static class ServicesExtensions
{
    public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
        services.AddHttpClient<IImageStockService, ImageStockManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();
        
        services.AddHttpClient<ICatalogService, CatalogManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<ICartService, CartManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Cart.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        services.AddHttpClient<IContactService, ContactManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Contact.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        services.AddHttpClient<IContactService, ContactManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Contact.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        services.AddHttpClient<ICommentService, CommentManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Contact.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        services.AddHttpClient<ITestimonialService, TestimonialManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Contact.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        
        services.AddHttpClient<IInstructorService, InstructorManager>(opt =>
        {
            opt.BaseAddress = new Uri(configuration.GetSection(nameof(ServiceApiSettings)).Get<ServiceApiSettings>()!
                .IdentityBaseUri);
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        services.AddHttpClient<IDiscountService, DiscountManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        services.AddHttpClient<IPaymentService, PaymentManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Payment.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        
        services.AddHttpClient<IOrderService, OrderManager>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings!.GatewayBaseUri}/{serviceApiSettings.Order.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
    }
    
    
}