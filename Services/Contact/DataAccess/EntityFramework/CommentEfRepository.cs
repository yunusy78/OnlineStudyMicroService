using DataAccess.Abstract;
using DataAccess.Concrete;
using EntityLayer.Concrete;

namespace DataAccess.EntityFramework;

public class CommentEfRepository : GenericEfRepository<Comment> , ICommentDal
{
    public CommentEfRepository(ContactDbContext context) : base(context)
    {
    }
}
