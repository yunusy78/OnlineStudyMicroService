using System.Net.Http.Json;
using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete;

public class UserManager : IUserService
{
    private readonly HttpClient _httpClient;
    
    public UserManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
       
    }
    
    public async Task<UserViewModel> GetUser()
    {
        return (await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/getUser"))!;
    }
}