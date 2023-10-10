namespace Business.Models;

public class ClientSettings
{
    public Client WebClient { get; set; }
    public Client WebClientUser { get; set; }
    
}

public class Client
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    
}