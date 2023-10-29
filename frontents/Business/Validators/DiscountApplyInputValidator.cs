using System.Data;
using Business.Models.DiscountViewModel;
using FluentValidation;

namespace Business.Validators;

public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
{
    public DiscountApplyInputValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
    }
    
}