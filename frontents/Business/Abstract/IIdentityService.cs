
using Business.Models;
using Frontents.Business.Dtos.Auth;
using IdentityModel.Client;
using OnlineStudyShared;

namespace Business.Abstract;

public interface IIdentityService
{
    Task<ResponseDto<bool>> SignIn(SignInInput signInInput);
    Task<TokenResponse>GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();
    
    Task<ResponseDto<bool>> SignUp(SignUpDto signUpInput);
}