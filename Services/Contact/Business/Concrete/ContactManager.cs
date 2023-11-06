using System.Linq.Expressions;
using Business.Abstract;
using DataAccess.Abstract;
using EntityLayer.Concrete;
using OnlineStudyShared;

namespace Business.Concrete;

public class ContactManager : IContactService
{
    private readonly IContactDal _contactDal;
    
    public ContactManager(IContactDal contactDal)
    {
        _contactDal = contactDal;
    }


    public async  Task<ResponseDto<Contact>> AddAsync(Contact entity)
    {
        return await _contactDal.AddAsync(entity);
    }

    public async Task<ResponseDto<Contact>> UpdateAsync(Contact entity)
    {
        return await _contactDal.UpdateAsync(entity);
    }

    public async Task<ResponseDto<Contact>> DeleteAsync(int id)
    {
        return await _contactDal.DeleteAsync(id);
    }
   

    public async Task<ResponseDto<List<Contact>>> GetAllAsync()
    {
        return await _contactDal.GetAllAsync();
    }

    public async Task<ResponseDto<Contact>> GetByIdAsync(int id)
    {
        return await _contactDal.GetByIdAsync(id);
    }

    public async Task<ResponseDto<List<Contact>>> GetListByFilterAsync(Expression<Func<Contact, bool>> filter)
    {
        return await _contactDal.GetListByFilterAsync(filter);
    }
}