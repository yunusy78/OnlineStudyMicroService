﻿namespace Business.Models;

public class ServiceApiSettings
{
    public string IdentityBaseUri { get; set; }
    public string GatewayBaseUri { get; set; }
    public string PhotoStockUri { get; set; }
    
    public string ContactUri { get; set; }
    
    public ServiceApi Catalog { get; set; }
    public ServiceApi PhotoStock { get; set; }
    
    public ServiceApi Cart { get; set; }
    
    public ServiceApi Discount { get; set; }
    
    public ServiceApi Payment { get; set; }
    
    public ServiceApi Order { get; set; }
    
    public ServiceApi Contact { get; set; }
    
}


public class ServiceApi
{
    public string Path { get; set; }
    
    
}