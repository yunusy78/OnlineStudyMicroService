using Business.Models.Images;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract;

public interface IImageStockService
{
    Task<ImageViewModel> UploadImageCourseAsync(IFormFile imageFile);
    Task<bool> DeleteImageCourseAsync(string imageUrl);
    
    
    
}