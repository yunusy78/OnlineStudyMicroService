
using AutoMapper;
using ContactMicroServices.Dtos;
using EntityLayer.Concrete;
using Services.Contact.ContactMicroServices.Dtos;

namespace ContactMicroServices.Mapping;

public class CustomMapper : Profile
{
    public CustomMapper()
    {
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Instructor, InstructorDto>().ReverseMap();
        CreateMap<Testimonial, TestimonialDto>().ReverseMap();
        
    }
    
}