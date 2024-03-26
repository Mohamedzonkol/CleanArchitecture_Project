using CleanArchitecture.Core.Featuers.Authentication.Query.Models;
using CleanArchitecture.Core.SheardResourses;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authentication.Query.Validators
{
    public class ResetPasswordValidators : AbstractValidator<ResetPasswordQuery>
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
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);

        }
        private void ApplyCustomValidationRule()
        {

        }

    }
}
