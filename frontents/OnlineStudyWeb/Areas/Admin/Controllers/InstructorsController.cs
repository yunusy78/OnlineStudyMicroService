using Business.Abstract;
using Business.Dtos.Contact;
using Frontents.Business.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class InstructorsController : Controller
{
    private readonly IInstructorService _instructorService;
    
    public InstructorsController(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }
    
    
    // GET
    public async Task<IActionResult> Index()
    {
        var instructors = await _instructorService.GetAllAsync();
        return View(instructors.Data);
    }
    
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(InstructorDto instructorDto)
    {
        instructorDto.CreatedDate = DateTime.UtcNow;
        var response = await _instructorService.AddAsync(instructorDto);
        if (response)
        {
            return RedirectToAction("Index", "Instructors", new {area = "Admin"});
        }
        
        return View(instructorDto);
    }
    
    public async Task<IActionResult> Update(int id)
    {
        var instructor = await _instructorService.GetByIdAsync(id);
        if (!instructor.IsSuccess)
        {
            return NotFound();
        }

        return View(instructor.Data);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> Update(InstructorDto instructorDto)
    {
        if (!ModelState.IsValid)
        {
            return View(instructorDto);
        }

        var response = await _instructorService.UpdateAsync(instructorDto);
        if (response)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return View(instructorDto);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var instructor = await _instructorService.DeleteAsync(id);
        if (!instructor)
        {
            return NotFound();
        }

        return RedirectToAction("Index", "Instructors", new {area = "Admin"});
    }
    
    
}