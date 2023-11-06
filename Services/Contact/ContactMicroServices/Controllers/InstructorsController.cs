using Business.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Controller;
using OnlineStudyShared.Services;


namespace ContactMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : CustomBaseController
    {
        
        private readonly ISharedIdentity _sharedIdentity;
        private readonly IInstructorService _instructorService;
        
        public InstructorsController(ISharedIdentity sharedIdentity, IInstructorService instructorService)
        {
            _sharedIdentity = sharedIdentity;
            _instructorService = instructorService;
        }
        
        [HttpPost]
        
        public async Task<IActionResult> CreateOrder(Instructor instructor)
        {
            var response = await _instructorService.AddAsync(instructor);
            return CreateActionResultInstance(response);
            
        }
        
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _instructorService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        
        
        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var response = await _instructorService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        
        
        [HttpPut]
        
        public async Task<IActionResult> Update(Instructor instructor)
        {
            var response = await _instructorService.UpdateAsync(instructor);
            return CreateActionResultInstance(response);
        }
        
        
        
        
        
    }
}
