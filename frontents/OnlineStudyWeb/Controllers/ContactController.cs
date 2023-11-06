using Business.Abstract;
using Business.Dtos.Contact;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Controllers;

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
        var contactV = await _contactService.AddAsync(contact);
        if (contactV == null)
        {
            return NotFound();
        }
        return RedirectToAction("Index","Home");
    }
    
}