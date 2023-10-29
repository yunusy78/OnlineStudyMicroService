using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared;
using OnlineStudyShared.Controller;
using Stripe;
using Stripe.Checkout;
using StripePaymentMicroService.Dtos;
using StripePaymentMicroService.Dtos.PaymentDto;

namespace StripePaymentMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            var response = CreateActionResultInstance(ResponseDto<NoContent>.Success(200));
            
            return response;

        }
    }
}
