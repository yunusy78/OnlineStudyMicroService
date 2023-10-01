using StackExchange.Redis;

namespace CartMicroServices.Services;

public class RedisService
{
    private readonly string _host;
    private readonly int _port;
    private ConnectionMultiplexer _connectionMultiplexer;

    public RedisService(string host, int port)
    {
        _host = host;
        _port = port;
    }
    
    public void Connect()
    {
        var configurationOptions = new ConfigurationOptions
        {
            EndPoints = { $"{_host}:{_port}" }
        };
        _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationOptions);
    }
    
    public IDatabase GetDb(int db = 1)
    {
        return _connectionMultiplexer.GetDatabase(db);
    }
}