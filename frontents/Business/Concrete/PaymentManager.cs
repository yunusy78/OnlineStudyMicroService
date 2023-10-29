using System.Net.Http.Json;
using Business.Abstract;
using Business.Models.FakePayment;

namespace Business.Concrete;

public class PaymentManager : IPaymentService
{
    private readonly HttpClient _httpClient;
    
    public PaymentManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
    {
        var response = await _httpClient.PostAsJsonAsync("payments", paymentInfoInput);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }
}