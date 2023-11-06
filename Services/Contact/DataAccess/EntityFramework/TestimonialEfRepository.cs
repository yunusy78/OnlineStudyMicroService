using DataAccess.Abstract;
using DataAccess.Concrete;
using EntityLayer.Concrete;

namespace DataAccess.EntityFramework;

public class TestimonialEfRepository : GenericEfRepository<Testimonial>, ITestimonialDal
{
    public TestimonialEfRepository(ContactDbContext context) : base(context)
    {
    }
}