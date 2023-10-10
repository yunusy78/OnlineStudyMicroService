using System.Net.Http.Json;
using Business.Abstract;
using Business.Dtos.Catalog.Course;
using Business.Models;
using Business.Models.Catalog;
using OnlineStudyShared;

namespace Business.Concrete;

public class CatalogManager : ICatalogService
{
    private readonly HttpClient _httpClient;
    
    public CatalogManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    
    public async Task<List<CourseViewModel>> GetCourseAsync()
    {
        var response = await _httpClient.GetAsync("course");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();
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
        return responseSuccess!.Data;
    }

    public async Task<bool> CreateCourseAsync(CreateCourseDto createCourseDto)
    {
        var response = await _httpClient.PostAsJsonAsync("course", createCourseDto);
        if (!response.IsSuccessStatusCode)
        {
            return false;
            
        }
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(UpdateCourseDto updateCourseDto)
    {
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