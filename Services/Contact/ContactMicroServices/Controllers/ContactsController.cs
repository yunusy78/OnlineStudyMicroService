using Business.Abstract;
using ContactMicroServices.Dtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Controller;
using OnlineStudyShared.Services;


namespace ContactMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : CustomBaseController
    {
        
       
        private readonly IContactService _contactService;
        
        public ContactsController(IContactService contactService)
        {
            
            _contactService = contactService;
        }
        
       
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Contact contact)
        {
            var response = await _contactService.AddAsync(contact);
            return CreateActionResultInstance(response);
            
        }
        
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _contactService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        
        
        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var response = await _contactService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        
        
        [HttpPut]
        
        public async Task<IActionResult> Update(Contact contact)
        {
           
            var response = await _contactService.UpdateAsync(contact);
            return CreateActionResultInstance(response);
        }
        
        
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _contactService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
        
        
        
        
        
        
    }
}
