using Business.Abstract;
using Business.Dtos.Contact;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Instructor.Controllers;
[Area("Instructor")]
public class TestimonialsController : Controller
{
    private readonly ITestimonialService _testimonialService;
    
    public TestimonialsController(ITestimonialService testimonialService)
    {
        _testimonialService = testimonialService;
    }
    
    public IActionResult Index()
    {
        
        return View();
    }
    
    // GET
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(TestimonialDto testimonialDto)
    {
        testimonialDto.CreatedDate = DateTime.UtcNow;
        var response = await _testimonialService.AddAsync(testimonialDto);
        if (response)
        {
            return RedirectToAction("Index", "Profile", new {area = "Instructor"});
        }
        
        return View(testimonialDto);
    }
   
}