﻿

using Microsoft.AspNetCore.Http;

namespace Business.Dtos.Contact;

public class InstructorDto 
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Status { get; set; }
    
    public IFormFile ImageFormFile { get; set; }
    
}