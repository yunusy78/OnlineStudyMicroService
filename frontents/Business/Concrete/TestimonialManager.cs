using System.Linq.Expressions;
using Business.Abstract;
using Business.Dtos.Contact;
using OnlineStudyShared;

namespace Business.Concrete;

public class TestimonialManager : ITestimonialService
{
    public Task<ResponseDto<TestimonialDto>> AddAsync(TestimonialDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<TestimonialDto>> UpdateAsync(TestimonialDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<TestimonialDto>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<TestimonialDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<TestimonialDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<TestimonialDto>>> GetListByFilterAsync(Expression<Func<TestimonialDto, bool>> filter)
    {
        throw new NotImplementedException();
    }
}