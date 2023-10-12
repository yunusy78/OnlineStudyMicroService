using Business.Models;
using Microsoft.Extensions.Options;

namespace Business.Helpers;

public class PhotoStockHelper
{
    private readonly ServiceApiSettings _serviceApiSettings;
    
    public PhotoStockHelper(IOptions<ServiceApiSettings> serviceApiSettings)
    {
        _serviceApiSettings = serviceApiSettings.Value;
    }
    
    public string GetPhotoStockUrl(string path)
    {
        return $"{_serviceApiSettings.PhotoStockUri}/Images/{path}";
    }
    
    
}