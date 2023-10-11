using System.Net.Http.Json;
using Business.Abstract;
using Business.Models.Images;
using Microsoft.AspNetCore.Http;
using OnlineStudyShared;

namespace Business.Concrete;

public class ImageStockManager : IImageStockService
{
    private readonly HttpClient _httpClient;
    
    public ImageStockManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    public async Task<ImageViewModel> UploadImageCourseAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length <= 0)
        {
            return null!;
        }
        var randomName = Guid.NewGuid().ToString();
        var extension = Path.GetExtension(imageFile.FileName);
        var fileName = randomName + extension;
        using var ms = new MemoryStream();
        await imageFile.CopyToAsync(ms);
        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(new ByteArrayContent(ms.ToArray()),"file",fileName);
        var response = await _httpClient.PostAsync("images",multipartContent);
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<ImageViewModel>>();
        return responseSuccess!.Data;
        

    }

    public async Task<bool> DeleteImageCourseAsync(string imageUrl)
    {
        var response = await _httpClient.DeleteAsync($"images?fileNameUrl={imageUrl}");
        return response.IsSuccessStatusCode;
    }
}