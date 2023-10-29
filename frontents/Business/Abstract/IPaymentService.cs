using Business.Models.FakePayment;

namespace Business.Abstract;

public interface IPaymentService
{
    Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    
}