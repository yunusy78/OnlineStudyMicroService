namespace Business.Abstract;

public interface IClientCredentialTokenService
{
    Task <string> GetToken();
}