using AmazonECommerce.Application.DTOs.Identity;
using FluentValidation;

namespace AmazonECommerce.Application.Validators.Authentication;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.s")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("The password must have a minimum length of 8 characters.")
            .Matches(@"[A-Z]").WithMessage("The password must include at least one uppercase letter (e.g., A-Z).")
            .Matches(@"[a-z]").WithMessage("The password must include at least one lowercase letter (e.g., a-z).")
            .Matches(@"[\d]").WithMessage("The password must contain at least one numeric digit (e.g., 0-9).");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Password do not match.");
    }
}
