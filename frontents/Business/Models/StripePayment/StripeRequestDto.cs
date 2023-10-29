﻿namespace Business.Models.StripePayment;

public class StripeRequestDto
{
    public string StripeSessionUrl { get; set; }
    public string StripeSessionId { get; set; }
    public string ApproveUrl { get; set; }
    public string CancelUrl { get; set; }
    //public OrderHeaderDto OrderHeaderDto { get; set; }
    
}