using CleanArchitecture.Core.Featuers.Authorization.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Services.Abstract;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authorization.Commands.Validtors
{
    public class AddRoleValidtors : AbstractValidator<AddRoleCommand>
    {
        private readonly IAuthorizationServices _authorizationServices;
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer;

        public AddRoleValidtors(IAuthorizationServices authorizationServices,
            IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        {
            _authorizationServices = authorizationServices;
            _stringLocalizer = stringLocalizer;
            ApplyValidationRule();
            ApplyCustomValidationRule();
        }

        private void ApplyValidationRule()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull]);

        }

        private void ApplyCustomValidationRule()
        {
            RuleFor(x => x.RoleName).MustAsync(async (key, CancellationToken) =>
                !await _authorizationServices.IsRoleNameExist(key)).WithMessage("This Role  Is Exist");
        }
    }
}
