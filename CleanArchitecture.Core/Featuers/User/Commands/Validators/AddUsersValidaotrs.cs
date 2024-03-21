using CleanArchitecture.Core.Featuers.User.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.User.Commands.Validators
{
    public class AddUsersValidaotrs : AbstractValidator<AddUsersCommand>
    {
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer;

        public AddUsersValidaotrs(IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRule();
            ApplyCustomValidationRule();
        }

        private void ApplyValidationRule()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull])
                .MaximumLength(50).WithMessage("Max Length Is 50");
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull])
                .MaximumLength(20).WithMessage("Max Length Is 20");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull])
                .Matches(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull])
                .Equal(x => x.Password).WithMessage(_stringLocalizer[SheardResoursesKeys.PasswordNotMatched]);

        }
        private void ApplyCustomValidationRule()
        {

        }
    }
}
