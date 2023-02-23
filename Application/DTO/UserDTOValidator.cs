
using FluentValidation;

namespace Application.DTO
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email must be informed");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password must be informed");
        }
    }
}
