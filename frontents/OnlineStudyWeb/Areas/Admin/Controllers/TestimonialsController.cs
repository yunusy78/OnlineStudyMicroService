using Business.Abstract;
using Business.Dtos.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class TestimonialsController : Controller
{
    private readonly ITestimonialService _testimonialService;
    
    public TestimonialsController(ITestimonialService testimonialService)
    {
        _testimonialService = testimonialService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var testimonials = await _testimonialService.GetAllAsync();
        return View(testimonials.Data);
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _testimonialService.DeleteAsync(id);
        if (!response)
        {
            return NotFound();
        }
        return RedirectToAction("Index", "Testimonials", new {area = "Admin"});
    }
    
    public async Task<IActionResult> Update(int id)
    {
        var testimonial = await _testimonialService.GetByIdAsync(id);
        if (!testimonial.IsSuccess)
        {
            return NotFound();
        }
        
        return View(testimonial.Data);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(TestimonialDto testimonialDto)
    {
        var response = await _testimonialService.UpdateAsync(testimonialDto);
        if (response)
        {
            return RedirectToAction("Index", "Testimonials", new {area = "Admin"});
        }
        
        return View(testimonialDto);
    }
}