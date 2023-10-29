using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Models.Cart;

public class CartViewModel
{
    public CartViewModel()
    {
        _cartItems = new List<CartItemViewModel>();
    }
    
    public string UserId { get; set; }
    public string? DiscountCode { get; set; }
    
    public int? DiscountRate { get; set; }
    
    private List<CartItemViewModel> _cartItems;
    
    public List<CartItemViewModel> CartItems 
    {
        get{
            if(HasDiscount)
            {
                _cartItems.ForEach
                    (x =>
                        {
                            var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                            x.ApplyDiscount(Math.Round(x.Price - discountPrice, 2));
                            
                        });
            }
            return _cartItems;
        }    
            set
            {
                _cartItems = value;
                
            }
        
    }
    

    public decimal TotalPrice
    {
        get => CartItems.Sum(x => x.GetCurrentPrice);
    }
    
    public bool HasDiscount
    {
        get => !string.IsNullOrEmpty(DiscountCode)&& DiscountRate.HasValue;
    }
    
    public void CancelDiscount()
    {
        DiscountCode = null;
        DiscountRate = null;
    }
    
    public void ApplyDiscount(string discountCode, int discountRate)
    {
        DiscountCode = discountCode;
        DiscountRate = discountRate;
    }
}