using CategoryApi.Application.Products.Dto.Request;
using FluentValidation;

namespace CategoryApi.Application.Products.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty();
        
        RuleFor(x => x.Code)
            .MaximumLength(50)
            .NotEmpty();
    }
}