using System.Linq.Expressions;
using Business.Abstract;
using DataAccess.Abstract;
using EntityLayer.Concrete;
using OnlineStudyShared;

namespace Business.Concrete;

public class InstructorManager : IInstructorService
{
    private readonly IInstructorDal _instructorDal;
    
    public InstructorManager(IInstructorDal instructorDal)
    {
        _instructorDal = instructorDal;
    }


    public async Task<ResponseDto<Instructor>> AddAsync(Instructor entity)
    {
       return await _instructorDal.AddAsync(entity);
    }

    public async Task<ResponseDto<Instructor>> UpdateAsync(Instructor entity)
    {
        return  await _instructorDal.UpdateAsync(entity);
    }

    public async Task<ResponseDto<Instructor>> DeleteAsync(int id)
    {
        return await _instructorDal.DeleteAsync(id);
    }
    

    public async Task<ResponseDto<List<Instructor>>> GetAllAsync()
    {
        return await _instructorDal.GetAllAsync();
    }

    public async Task<ResponseDto<Instructor>> GetByIdAsync(int id)
    {
        return await _instructorDal.GetByIdAsync(id);
    }

    public async Task<ResponseDto<List<Instructor>>> GetListByFilterAsync(Expression<Func<Instructor, bool>> filter)
    {
        return await _instructorDal.GetListByFilterAsync(filter);
    }
}