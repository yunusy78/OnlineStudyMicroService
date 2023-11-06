using DataAccess.Abstract;
using DataAccess.Concrete;
using EntityLayer.Concrete;

namespace DataAccess.EntityFramework;

public class ContactEfRepository : GenericEfRepository<Contact> , IContactDal
{
    public ContactEfRepository(ContactDbContext context) : base(context)
    {
    }
}
