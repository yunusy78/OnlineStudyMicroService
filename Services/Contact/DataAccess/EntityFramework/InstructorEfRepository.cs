using System.Linq.Expressions;
using DataAccess.Abstract;
using DataAccess.Concrete;
using EntityLayer.Concrete;

namespace DataAccess.EntityFramework;

public class InstructorEfRepository : GenericEfRepository<Instructor>, IInstructorDal
{
    public InstructorEfRepository(ContactDbContext context) : base(context)
    {
    }
}