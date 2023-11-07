using Business.Abstract;
using Business.Dtos.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Controllers;
[Authorize]
public class ContactController : Controller
{
    public readonly IContactService _contactService;
    
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var contacts = await _contactService.GetAllAsync();
        return View();
    }
    
    [HttpPost]
    
    public async Task<IActionResult> Create(ContactDto contact)
    {
        contact.CreatedDate = DateTime.UtcNow;
        contact.Status = true;
        var contactV = await _contactService.AddAsync(contact);
        if (contactV == null)
        {
            return NotFound();
        }
        return RedirectToAction("Index","Home");
    }
    
}