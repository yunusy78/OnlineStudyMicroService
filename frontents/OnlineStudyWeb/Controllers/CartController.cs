﻿using Business.Abstract;
using Business.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Services;

namespace OnlineStudyWeb.Controllers;

public class CartController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ICartService _cartService;
    private readonly ISharedIdentity _sharedIdentity;
    
    public CartController(ICatalogService catalogService, ICartService cartService, ISharedIdentity sharedIdentity)
    {
        _catalogService = catalogService;
        _cartService = cartService;
        _sharedIdentity = sharedIdentity;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _cartService.GetCart();
        return View(result);
    }
    
    public async Task<IActionResult> AddToCart(string courseId)
    {
        
        var course = await _catalogService.GetCourseByIdAsync(courseId);
        if (course == null)
        {
            return NotFound();
        }
        
        var cartItemViewModel = new CartItemViewModel
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Price = course.CoursePrice,
            PictureUrl = course.StockImageUrl,
            
            Quantity = 1
        };
        await _cartService.AddItem(cartItemViewModel);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> RemoveCartItem(string courseId)
    {
        await _cartService.RemoveCartItem(courseId);
        return RedirectToAction("Index");
    }
    
    
    public async Task<IActionResult> ApplyDiscount(string discountCode)
    {
        await _cartService.ApplyDiscount(discountCode);
        return RedirectToAction("Index");
    }
    
    
    public async Task<IActionResult> CancelApplyDiscount()
    {
        await _cartService.CancelApplyDiscount();
        return RedirectToAction("Index");
    }
    
    
 
    
    
    
    
    
}