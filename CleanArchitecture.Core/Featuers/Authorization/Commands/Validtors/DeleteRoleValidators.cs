using CleanArchitecture.Core.Featuers.Authorization.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authorization.Commands.Validtors
{
    public class DeleteRoleValidators : AbstractValidator<DeleteRoleCommand>
    {

        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer;

        public DeleteRoleValidators(
            IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRule();
        }

        private void ApplyValidationRule()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);

        }
    }
}
