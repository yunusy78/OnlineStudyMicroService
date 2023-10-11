using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared;
using OnlineStudyShared.Controller;
using PhotoStockMicroServices.Dtos;

namespace PhotoStockMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : CustomBaseController
    {
        
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file,CancellationToken cancellationToken)
        {
            if (file==null || file.Length<=0)
            {
                return CreateActionResultInstance(ResponseDto<ImageDto>.Fail("Image is not available",400));
            }
           var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", file.FileName);
           
           using var stream = new FileStream(path,FileMode.Create);
           await file.CopyToAsync(stream,cancellationToken);
           var returnPath = file.FileName;
           
           ImageDto imageDto = new ImageDto()
           {
               Url = returnPath
           };
           
           return CreateActionResultInstance(ResponseDto<ImageDto>.Success(imageDto,200));
           
        }
        
        [HttpGet]
        
        public IActionResult Delete(string fileNameUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileNameUrl);
            
            if(!System.IO.File.Exists(path))
            {
               return CreateActionResultInstance(ResponseDto<NoContent>.Fail("Image not found",404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(ResponseDto<NoContent>.Success(204));
        }
        
    }
}
