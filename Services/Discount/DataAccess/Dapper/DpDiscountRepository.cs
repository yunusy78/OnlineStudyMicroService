using System.Data;
using Dapper;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity.Concrete;
using OnlineStudyShared;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace DataAccess.Dapper;

public class DpDiscountRepository : IDiscountDal
{
    private readonly IConfiguration _configuration;
    
    private readonly IDbConnection _dbConnection;
    
    public DpDiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
    
    public async Task<ResponseDto<List<Discount>>> GetAllAsync()
    {
        var discounts = await _dbConnection.QueryAsync<Discount>("SELECT * FROM discounts");
        return ResponseDto<List<Discount>>.Success(discounts.ToList(),200);
    }

    public async Task<ResponseDto<Discount>> GetByIdAsync(int id)
    {
        var discount = await _dbConnection
            .QueryFirstOrDefaultAsync<Discount>
                ("SELECT * FROM discounts WHERE Id = @Id", new {Id = id});
        if (discount == null)
        {
            return ResponseDto<Discount>.Fail("Discount not found",404);
        }
        
        return ResponseDto<Discount>.Success(discount,200);
    }

    public async Task<ResponseDto<Discount>> AddAsync(Discount discount)
    {
        if (discount == null)
        {
            return ResponseDto<Discount>.Fail("Discount is null",404);
        }
        var sql = "INSERT INTO discounts (code, userId, rate, createdTime, status) VALUES (@Code, @UserId, @Rate, @CreatedTime, @Status)";
        var parameters = new DynamicParameters();
        parameters.Add("@Code", discount.Code, DbType.String);
        parameters.Add("@UserId", discount.UserId, DbType.String);
        parameters.Add("@Rate", discount.Rate, DbType.Int32);
        parameters.Add("@CreatedTime", discount.CreatedTime, DbType.DateTime);
        parameters.Add("@Status", discount.Status, DbType.Boolean);
        using (var connection = _dbConnection)
        {
            await connection.ExecuteAsync(sql, parameters);
        }
        
        return ResponseDto<Discount>.Success(200);
        
    }

    public async Task<ResponseDto<Discount>> UpdateAsync(Discount discount)
    {
        if (discount == null)
        {
            return ResponseDto<Discount>.Fail("Discount is null",404);
        }
        string sql = "UPDATE discounts SET code=@Code, userId=@UserId, rate=@Rate, createdTime=@CreatedTime, status=@Status WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", discount.Id, DbType.Int32);
        parameters.Add("@Code", discount.Code, DbType.String);
        parameters.Add("@UserId", discount.UserId, DbType.String);
        parameters.Add("@Rate", discount.Rate, DbType.Int32);
        parameters.Add("@CreatedTime", discount.CreatedTime, DbType.DateTime);
        parameters.Add("@Status", discount.Status, DbType.Boolean);
        using (var connection = _dbConnection)
        {
           await connection.ExecuteAsync(sql, parameters);
        }
        
        return ResponseDto<Discount>.Success(200);
    }
    

    public async Task<ResponseDto<Discount>> DeleteAsync(int id)
    {
        if (id == 0)
        {
            return ResponseDto<Discount>.Fail("Id is null",404);
        }
        
         string sql = "DELETE FROM discounts WHERE id = @id";
         var parameters = new DynamicParameters();
         parameters.Add("@Id", id, DbType.Int32);
         using (var connection = _dbConnection)
         {
            await connection.ExecuteAsync(sql, parameters);
         }
         
         return ResponseDto<Discount>.Success(200);
    }

    public async Task<ResponseDto<Discount>> GetByCodeAndUserAsync(string code, string userId)
    {
        string sql = "SELECT * FROM discounts WHERE code = @Code AND userId = @UserId";
        var parameters = new DynamicParameters();
        parameters.Add("@Code", code, DbType.String);
        parameters.Add("@UserId", userId, DbType.String);
        using (var connection = _dbConnection)
        {
            var result = await connection.QueryFirstOrDefaultAsync<Discount>(sql, parameters);
            return ResponseDto<Discount>.Success(result,200);
        }
        
        
    }
}