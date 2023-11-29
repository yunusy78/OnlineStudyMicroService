using System.Net.Http.Json;
using Business.Abstract;
using Business.Models.FakePayment;
using Business.Models.Order;
using OnlineStudyShared;
using OnlineStudyShared.Services;
using OrderApplication.Dtos;

namespace Business.Concrete;

public class OrderManager : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly IPaymentService _paymentService;
    private readonly ICartService _cartService;
    private readonly ISharedIdentity _sharedIdentity;
    
    public OrderManager(HttpClient httpClient, IPaymentService paymentService, ICartService cartService, ISharedIdentity sharedIdentity)
    {
        _httpClient = httpClient;
        _paymentService = paymentService;
        _cartService = cartService;
        _sharedIdentity = sharedIdentity;
    }
    
    
   

    public async Task<OrderStatusViewModel> CreateOrder(OrderCheckOutInfoInput orderCheckOutInfoInput)
    {
        var cart = await _cartService.GetCart();
        var payment = new PaymentInfoInput()
        {
            CardNumber = orderCheckOutInfoInput.CardNumber,
            CardHolderName = orderCheckOutInfoInput.CardHolderName,
            ExpirationMonth = orderCheckOutInfoInput.ExpirationMonth,
            ExpirationYear = orderCheckOutInfoInput.ExpirationYear,
            Cvv = orderCheckOutInfoInput.Cvv,
            TotalPrice = cart.TotalPrice

        };
        var paymentResult = await _paymentService.ReceivePayment(payment);
        if (!paymentResult)
        {
           return new OrderStatusViewModel(){error = "Payment is not successful",Success = false};
        }
        
        var order =new OrderCreateInput()
        {
            BuyerId = _sharedIdentity.GetUserId,
            Address = new AddressDto
            {
                Province = orderCheckOutInfoInput.Province,
                Street = orderCheckOutInfoInput.Street,
                District = orderCheckOutInfoInput.District,
                Line = orderCheckOutInfoInput.Line,
                ZipCode = orderCheckOutInfoInput.ZipCode
            }
            };
        cart.CartItems.ForEach(x =>
        {
            var orderItem = new OrderItemViewModel()
            {
                ProductId = x.CourseId,
                ProductName = x.CourseName,
                Price = x.Price,
                PictureUrl = x.PictureUrl,
                Quantity = x.Quantity
            };
            order.OrderItems.Add(orderItem);
                
            });
        
        var response = await _httpClient.PostAsJsonAsync("orders", order);
        if (!response.IsSuccessStatusCode)
        {
            return new OrderStatusViewModel(){error = "Order could not be created",Success = false};
        }
        var orderCreated = await response.Content.ReadFromJsonAsync<ResponseDto<OrderStatusViewModel>>();
        orderCreated!.Data.Success = true;

        await _cartService.Delete();
        return orderCreated.Data;

    }

    public async Task<OrderStatusViewModel> SuspectOrder(OrderCheckOutInfoInput orderCheckOutInfoInput)
    {
        var cart = await _cartService.GetCart();
        
        var order =new OrderCreateInput()
        {
            BuyerId = _sharedIdentity.GetUserId,
            Address = new AddressDto
            {
                Province = orderCheckOutInfoInput.Province,
                Street = orderCheckOutInfoInput.Street,
                District = orderCheckOutInfoInput.District,
                Line = orderCheckOutInfoInput.Line,
                ZipCode = orderCheckOutInfoInput.ZipCode
            }
        };
        
        cart.CartItems.ForEach(x =>
        {
            var orderItem = new OrderItemViewModel()
            {
                ProductId = x.CourseId,
                ProductName = x.CourseName,
                Price = x.Price,
                PictureUrl = x.PictureUrl,
                Quantity = x.Quantity
            };
            order.OrderItems.Add(orderItem);
                
        });
        
        var payment = new PaymentInfoInput()
        {
            CardNumber = orderCheckOutInfoInput.CardNumber,
            CardHolderName = orderCheckOutInfoInput.CardHolderName,
            ExpirationMonth = orderCheckOutInfoInput.ExpirationMonth,
            ExpirationYear = orderCheckOutInfoInput.ExpirationYear,
            Cvv = orderCheckOutInfoInput.Cvv,
            TotalPrice = cart.TotalPrice,
            Order = order

        };

        
        var paymentResult = await _paymentService.ReceivePayment(payment);
        if (!paymentResult)
        {
            return new OrderStatusViewModel(){error = "Payment is not successful",Success = false};
        }
        
        await _cartService.Delete();
        return new OrderStatusViewModel(){Success = true}; 
        
        
    }

    public async Task<List<OrderViewModel>> GetOrders()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<OrderViewModel>>>("orders");
            return response!.Data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return  new List<OrderViewModel>();
        }

    }
    
    public async Task<List<OrderViewModel>> GetAllOrder()
    {
        var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<OrderViewModel>>>("orders/GetAllOrders");
        return response!.Data;
        
    }
}