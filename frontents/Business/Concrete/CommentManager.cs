using System.Linq.Expressions;
using Business.Abstract;
using Business.Dtos.Contact;
using OnlineStudyShared;

namespace Business.Concrete;

public class CommentManager : ICommentService
{
    public Task<ResponseDto<CommentDto>> AddAsync(CommentDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<CommentDto>> UpdateAsync(CommentDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<CommentDto>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<CommentDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<CommentDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<CommentDto>>> GetListByFilterAsync(Expression<Func<CommentDto, bool>> filter)
    {
        throw new NotImplementedException();
    }
}