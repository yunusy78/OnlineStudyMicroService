using Business.Models;

namespace Business.Abstract;

public interface IUserService
{
    Task<UserViewModel> GetUser();
    
    
}