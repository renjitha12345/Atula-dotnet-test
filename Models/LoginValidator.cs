using AtulaHomeFurniture.Areas.Identity.Pages.Account;
using FluentValidation;

namespace AtulaHomeFurniture.Models
{
    public class LoginValidator : AbstractValidator<LoginModel.InputModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }

}
