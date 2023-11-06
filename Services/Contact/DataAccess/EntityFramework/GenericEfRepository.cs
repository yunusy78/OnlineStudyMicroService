using DataAccess.Abstract;
using System.Linq.Expressions;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using OnlineStudyShared;

namespace DataAccess.EntityFramework;

public class GenericEfRepository<T> : IGenericDal<T> where T : class, new() 
{
    private readonly ContactDbContext _context;
    
    public GenericEfRepository(ContactDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseDto<T>> AddAsync(T entity)
    {
        var addedEntity = _context.Entry(entity);
        addedEntity.State = EntityState.Added;
        await _context.SaveChangesAsync();
        return ResponseDto<T>.Success(entity,200);
    }

    public async Task<ResponseDto<T>> UpdateAsync(T entity)
    {
        var updatedEntity = _context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return ResponseDto<T>.Success(entity,200);
    }

    public async Task<ResponseDto<T>> DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return ResponseDto<T>.Fail("Entity not found",404);
        }
        var deletedEntity = _context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return ResponseDto<T>.Success(entity,200);
    }
    

    public async  Task<ResponseDto<List<T>>> GetAllAsync()
    {
        var list = await _context.Set<T>().ToListAsync();
        
        return ResponseDto<List<T>>.Success(list,200);
    }

    public async Task<ResponseDto<T>> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        return ResponseDto<T>.Success(entity!,200);
    }

    public async Task<ResponseDto<List<T>>> GetListByFilterAsync(Expression<Func<T, bool>> filter)
    {
        var list = await _context.Set<T>().Where(filter).ToListAsync();
        return ResponseDto<List<T>>.Success(list,200);
    }
}