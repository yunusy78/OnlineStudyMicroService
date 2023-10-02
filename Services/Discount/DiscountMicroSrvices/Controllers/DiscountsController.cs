using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Controller;
using OnlineStudyShared.Services;

namespace DiscountMicroSrvices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentity _sharedIdentity;
        
        public DiscountsController(IDiscountService discountService, ISharedIdentity sharedIdentity)
        {
            _discountService = discountService;
            _sharedIdentity = sharedIdentity;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _discountService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _discountService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Save(Discount discountDto)
        {
            var response = await _discountService.AddAsync(discountDto);
            return CreateActionResultInstance(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(Discount discountDto)
        {
            var response = await _discountService.UpdateAsync(discountDto);
            return CreateActionResultInstance(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _discountService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
        
        [HttpGet("GetByUser/{code}")]
        public async Task<IActionResult> GetByCodeAndUserId(string code)
        {
            var response = await _discountService.GetByCodeAndUserAsync(code, _sharedIdentity.GetUserId);
            return CreateActionResultInstance(response);
        }
    }
}
