using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared;
using OnlineStudyShared.Controller;

namespace StripePaymentMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance((ResponseDto<NoContent>.Success(200)));
        }
    }
}
