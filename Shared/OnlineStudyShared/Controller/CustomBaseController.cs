using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyShared.Controller;

public class CustomBaseController: ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(ResponseDto<T> serviceResponse)
    {
        return new ObjectResult(serviceResponse)
        {
            StatusCode = serviceResponse.StatusCode
        };
    }
}