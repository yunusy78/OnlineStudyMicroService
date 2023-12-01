using Business.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Controller;
using OnlineStudyShared.Services;


namespace ContactMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : CustomBaseController
    {
        
        private readonly ISharedIdentity _sharedIdentity;
        private readonly ITestimonialService  _testimonialService;
        
        public TestimonialsController(ISharedIdentity sharedIdentity, ITestimonialService testimonialService)
        {
            _sharedIdentity = sharedIdentity;
            _testimonialService = testimonialService;
        }
        
        [HttpPost]
        
        public async Task<IActionResult> CreateOrder(Testimonial testimonial)
        {
            var response = await _testimonialService.AddAsync(testimonial);
            return CreateActionResultInstance(response);
            
        }
        
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _testimonialService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _testimonialService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        
        
        [HttpPut]
        
        public async Task<IActionResult> Update(Testimonial testimonial)
        {
            var response = await _testimonialService.UpdateAsync(testimonial);
            return CreateActionResultInstance(response);
        }
        
        
        [HttpDelete("{id}")]
        
        
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _testimonialService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
        
        
        
        
        
        
    }
}
