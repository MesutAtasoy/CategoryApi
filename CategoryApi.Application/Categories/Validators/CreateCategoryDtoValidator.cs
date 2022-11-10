using CategoryApi.Application.Categories.Dto.Request;
using FluentValidation;

namespace CategoryApi.Application.Categories.Validators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}