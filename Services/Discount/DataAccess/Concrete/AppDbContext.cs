using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete;

public class AppDbContext
{
    private readonly IConfiguration _configuration;
    
    private readonly IDbConnection _dbConnection;
    
    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
    
    public IDbConnection CreateConnection() => _dbConnection;

}