using Business.Dtos.Catalog.Course;
using FluentValidation;

namespace Business.Validators;

public class CourseCreateInputValidator : AbstractValidator<CreateCourseDto>
{

    public CourseCreateInputValidator()
    {
        RuleFor(x => x.CourseName).NotEmpty().WithMessage("Course name is required");
        RuleFor(x => x.CoursePrice).NotEmpty().WithMessage("Course price is required");
        RuleFor(x => x.CourseDescription).NotEmpty().WithMessage("Course description is required");
        RuleFor(x => x.CourseDescription).MinimumLength(10).WithMessage("Course description must be at least 10 characters");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.CourseName).MaximumLength(100).WithMessage("Course name cannot be longer than 100 characters");
        RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage("Course price must be greater than 0");
        RuleFor(x => x.CourseImage).NotEmpty().WithMessage("Course picture is required");
        
        
        
    }
    
}