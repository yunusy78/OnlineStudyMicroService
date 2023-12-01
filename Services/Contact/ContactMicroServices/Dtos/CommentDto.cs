using MediatR;
using OnlineStudyShared;

namespace  ContactMicroServices.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string CommentText { get; set; }
    public string CourseId { get; set; }
    public string UserId { get; set; }
    public string CourseName { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Status { get; set; }
    
}