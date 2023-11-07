using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.ViewComponents;

public class TestimonialViewComponent : ViewComponent
{
    public ITestimonialService _testimonialService;
    
    public TestimonialViewComponent(ITestimonialService testimonialService)
    {
        _testimonialService = testimonialService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var testimonials = await _testimonialService.GetAllAsync();
        return View(testimonials.Data);
    }
    
}