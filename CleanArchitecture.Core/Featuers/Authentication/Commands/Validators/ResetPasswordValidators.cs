using CleanArchitecture.Core.Featuers.Authentication.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authentication.Commands.Validators
{
    public class ResetPasswordValidators : AbstractValidator<ResetPasswordCommand>
    {
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer;

        public ResetPasswordValidators(IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRule();
            ApplyCustomValidationRule();
        }

        private void ApplyValidationRule()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);

        }
        private void ApplyCustomValidationRule()
        {

        }

    }
}
