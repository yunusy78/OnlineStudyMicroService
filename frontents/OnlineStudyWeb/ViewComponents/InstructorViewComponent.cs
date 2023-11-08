using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.ViewComponents;

public class InstructorViewComponent : ViewComponent
{
    
    private readonly IInstructorService _instructorService;
    
    public InstructorViewComponent(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var instructors = await _instructorService.GetAllAsync();
        return View(instructors.Data);
    }
    
    
}