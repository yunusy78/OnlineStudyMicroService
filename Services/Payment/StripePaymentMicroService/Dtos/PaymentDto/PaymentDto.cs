﻿using StripePaymentMicroService.Dtos.OrderDto;

namespace StripePaymentMicroService.Dtos.PaymentDto;

public class PaymentDto
{
    
    
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpirationMonth { get; set; }
    public string ExpirationYear { get; set; }
    public string Cvv { get; set; }
    public decimal TotalPrice { get; set; }
    
    public OrderPaymentDto Order { get; set; }
    
}