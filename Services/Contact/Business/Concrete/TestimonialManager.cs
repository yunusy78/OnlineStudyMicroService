using System.Linq.Expressions;
using Business.Abstract;
using DataAccess.Abstract;
using EntityLayer.Concrete;
using Mysqlx;
using OnlineStudyShared;

namespace Business.Concrete;

public class TestimonialManager : ITestimonialService
{
    private readonly ITestimonialDal _testimonialDal;


    public TestimonialManager(ITestimonialDal testimonialDal)
    {
        _testimonialDal = testimonialDal;
    }


    public async Task<ResponseDto<Testimonial>> AddAsync(Testimonial entity)
    {
        return await _testimonialDal.AddAsync(entity);
    }

    public async Task<ResponseDto<Testimonial>> UpdateAsync(Testimonial entity)
    {
        return await _testimonialDal.UpdateAsync(entity);
    }

    public async Task<ResponseDto<Testimonial>> DeleteAsync(int id)
    {
        return await _testimonialDal.DeleteAsync(id);
    }
   

    public async Task<ResponseDto<List<Testimonial>>> GetAllAsync()
    {
        return await _testimonialDal.GetAllAsync();
    }

    public async Task<ResponseDto<Testimonial>> GetByIdAsync(int id)
    {
        return await _testimonialDal.GetByIdAsync(id);
    }

    public async Task<ResponseDto<List<Testimonial>>> GetListByFilterAsync(Expression<Func<Testimonial, bool>> filter)
    {
        return await _testimonialDal.GetListByFilterAsync(filter);
    }
}