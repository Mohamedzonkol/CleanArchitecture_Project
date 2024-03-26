using CleanArchitecture.Core.Featuers.Authentication.Query.Models;
using CleanArchitecture.Core.SheardResourses;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authentication.Query.Validators
{
    public class ConfirmEmailVaalidators : AbstractValidator<ConfirmEmailQuery>
    {
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer;

        public ConfirmEmailVaalidators(IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRule();
            ApplyCustomValidationRule();
        }

        private void ApplyValidationRule()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);
        }
        private void ApplyCustomValidationRule()
        {

        }

    }
}
