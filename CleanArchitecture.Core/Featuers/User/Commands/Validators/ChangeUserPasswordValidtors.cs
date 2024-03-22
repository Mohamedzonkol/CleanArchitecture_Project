using CleanArchitecture.Core.Featuers.User.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.User.Commands.Validators
{
    public class ChangeUserPasswordValidtors : AbstractValidator<ChangeUserPasswordCommand>
    {
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer;

        public ChangeUserPasswordValidtors(IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRule();
            ApplyCustomValidationRule();
        }

        private void ApplyValidationRule()
        {

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);
            RuleFor(x => x.CuurentPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage(_stringLocalizer[SheardResoursesKeys.PasswordNotMatched]);
        }

        private void ApplyCustomValidationRule()
        {

        }
    }
}
