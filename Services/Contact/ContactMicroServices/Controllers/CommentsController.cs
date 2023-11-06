using Business.Abstract;
using ContactMicroServices.Dtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Controller;
using OnlineStudyShared.Services;


namespace ContactMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : CustomBaseController
    {
        
        private readonly ISharedIdentity _sharedIdentity;
        private readonly ICommentService _commentService;
        
        public CommentsController(ISharedIdentity sharedIdentity, ICommentService commentService)
        {
            _sharedIdentity = sharedIdentity;
            _commentService = commentService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Comment comment)
        {
            var response = await _commentService.AddAsync(comment);
            return CreateActionResultInstance(response);
            
        }
        
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _commentService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        
        
        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var response = await _commentService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        
        
        [HttpPut]
        
        public async Task<IActionResult> Update(Comment comment)
        {
            var response = await _commentService.UpdateAsync(comment);
            return CreateActionResultInstance(response);
        }
        
        
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _commentService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
        
        
        
        
        
        
        
    }
}
