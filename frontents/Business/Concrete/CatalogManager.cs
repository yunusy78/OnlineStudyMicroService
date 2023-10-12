using System.Net.Http.Json;
using Business.Abstract;
using Business.Dtos.Catalog.Course;
using Business.Helpers;
using Business.Models;
using Business.Models.Catalog;
using OnlineStudyShared;

namespace Business.Concrete;

public class CatalogManager : ICatalogService
{
    private readonly HttpClient _httpClient;
    private readonly IImageStockService _imageStockService;
    private readonly PhotoStockHelper _photoStockHelper;
    
    public CatalogManager(HttpClient httpClient, IImageStockService imageStockService ,PhotoStockHelper photoStockHelper)
    {
        _httpClient = httpClient;
        _imageStockService = imageStockService;
        _photoStockHelper = photoStockHelper;
    }
    
    
    public async Task<List<CourseViewModel>> GetCourseAsync()
    {
        var response = await _httpClient.GetAsync("course");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();
        
        responseSuccess!.Data.ForEach(x =>
        {
            x.StockImageUrl = _photoStockHelper.GetPhotoStockUrl(x.CourseImage);
        });
        
        return responseSuccess!.Data;
    }
    

    public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"course/GetAllByUserId/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();
        
        responseSuccess!.Data.ForEach(x =>
        {
            x.StockImageUrl = _photoStockHelper.GetPhotoStockUrl(x.CourseImage);
        });
        return responseSuccess!.Data;
    }

    public async Task<List<CategoryViewModel>> GetCategoryAsync()
    {
        var response = await _httpClient.GetAsync("category");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<CategoryViewModel>>>();
        return responseSuccess!.Data;
    }

    public async Task<CourseViewModel> GetCourseByIdAsync(string courseId)
    {
        var response = await _httpClient.GetAsync($"course/{courseId}");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<CourseViewModel>>();
        responseSuccess!.Data.StockImageUrl= _photoStockHelper.GetPhotoStockUrl(responseSuccess!.Data.CourseImage);
        return responseSuccess!.Data;
    }

    public async Task<bool> CreateCourseAsync(CreateCourseDto createCourseDto)
    {
        var imageResult = await _imageStockService.UploadImageCourseAsync(createCourseDto.ImageFormFile);
        if (imageResult != null)
        {
            createCourseDto.CourseImage= imageResult.Url;
        }

        var response = await _httpClient.PostAsJsonAsync("course", createCourseDto);
        if (!response.IsSuccessStatusCode)
        {
            return false;
            
        }
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(UpdateCourseDto updateCourseDto)
    {
        
        var imageResult = await _imageStockService.UploadImageCourseAsync(updateCourseDto.ImageFormFile);
        if (imageResult != null)
        {
            _imageStockService.DeleteImageCourseAsync(updateCourseDto.CourseImage);
            updateCourseDto.CourseImage= imageResult.Url;
        }
        
        var response = await _httpClient.PutAsJsonAsync("course", updateCourseDto);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return response.IsSuccessStatusCode;
    }

    public Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = _httpClient.DeleteAsync($"course/{courseId}");
        return response.Result.IsSuccessStatusCode ? Task.FromResult(true) : Task.FromResult(false);
    }
}