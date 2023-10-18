using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Business.Models.Cart;

public class CartItemViewModel
{
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public decimal Price { get; set; }
    public string? PictureUrl { get; set; }
    public int Quantity { get; set; }

    private decimal? _discountAppliedPrice;

    public void ApplyDiscount(decimal discountPrice)
    {
        _discountAppliedPrice = discountPrice;
    }

    public decimal GetCurrentPrice
    {
        get => _discountAppliedPrice != null ? _discountAppliedPrice.Value : Price;
    }
}