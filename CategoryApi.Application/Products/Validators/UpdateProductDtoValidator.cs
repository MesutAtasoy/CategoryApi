using CategoryApi.Application.Products.Dto.Request;
using FluentValidation;

namespace CategoryApi.Application.Products.Validators;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        
        RuleFor(x => x.Code)
            .MaximumLength(50)
            .NotEmpty();
    }
}