using FluentValidation;

namespace AtulaHomeFurniture.Models
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.Sku).NotEmpty().WithMessage("SKU is required.");
        }
    }
}