using System.Linq.Expressions;
using Business.Abstract;
using Business.Dtos.Contact;
using OnlineStudyShared;

namespace Business.Concrete;

public class InstructorManager : IInstructorService
{
    public Task<ResponseDto<InstructorDto>> AddAsync(InstructorDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<InstructorDto>> UpdateAsync(InstructorDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<InstructorDto>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<InstructorDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<InstructorDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<InstructorDto>>> GetListByFilterAsync(Expression<Func<InstructorDto, bool>> filter)
    {
        throw new NotImplementedException();
    }
}