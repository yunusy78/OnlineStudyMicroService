using System.Linq.Expressions;
using Business.Abstract;
using DataAccess.Abstract;
using EntityLayer.Concrete;
using OnlineStudyShared;

namespace Business.Concrete;

public class CommentManager : ICommentService
{
    private readonly ICommentDal _commentDal;
    
    public CommentManager(ICommentDal commentDal)
    {
        _commentDal = commentDal;
    }

    public async Task<ResponseDto<Comment>> AddAsync(Comment entity)
    {
        return await _commentDal.AddAsync(entity);
    }

    public async Task<ResponseDto<Comment>> UpdateAsync(Comment entity)
    {
        return await _commentDal.UpdateAsync(entity);
    }

    public async Task<ResponseDto<Comment>> DeleteAsync(int id)
    {
        return await _commentDal.DeleteAsync(id);
    }
    

    public async Task<ResponseDto<List<Comment>>> GetAllAsync()
    {
        return await _commentDal.GetAllAsync();
    }

    public async Task<ResponseDto<Comment>> GetByIdAsync(int id)
    {
        return await _commentDal.GetByIdAsync(id);
    }

    public async Task<ResponseDto<List<Comment>>> GetListByFilterAsync(Expression<Func<Comment, bool>> filter)
    {
        return await _commentDal.GetListByFilterAsync(filter);
    }
}