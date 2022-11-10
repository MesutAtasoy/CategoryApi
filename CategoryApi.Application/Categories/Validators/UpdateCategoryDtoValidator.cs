using CategoryApi.Application.Categories.Dto.Request;
using FluentValidation;

namespace CategoryApi.Application.Categories.Validators;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}