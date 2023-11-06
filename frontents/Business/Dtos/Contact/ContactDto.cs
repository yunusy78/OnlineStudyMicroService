﻿

namespace Business.Dtos.Contact;

public class ContactDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public string Subject { get; set; }
    
    public string Message { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public bool Status { get; set; }


}