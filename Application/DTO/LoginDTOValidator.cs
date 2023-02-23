using FluentValidation;

namespace Application.DTO
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email must be informed");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password must be informed");
        }
    }
}
