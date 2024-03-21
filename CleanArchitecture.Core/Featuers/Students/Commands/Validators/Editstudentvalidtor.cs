using CleanArchitecture.Core.Featuers.Students.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Services.Abstract;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Students.Commands.Validators
{
    public class Editstudentvalidtor : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentServices _studentServices;
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer;

        public Editstudentvalidtor(IStudentServices studentServices, IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        {
            _studentServices = studentServices;
            _stringLocalizer = stringLocalizer;
            ApplyValidationRule();
            ApplyCustomValidationRule();
        }

        private void ApplyValidationRule()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull])
                .MaximumLength(10).WithMessage("Max Length Is 10");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull])
                .MaximumLength(10).WithMessage("Max Length Is 10");
        }

        private void ApplyCustomValidationRule()
        {
            RuleFor(x => x.NameAr).MustAsync(async (model, key, CancellationToken) =>
                !await _studentServices.IsNameArExistExcludeSelf(key, model.Id)).WithMessage("Name Is Exist");
            RuleFor(x => x.NameEn).MustAsync(async (model, key, CancellationToken) =>
                !await _studentServices.IsNameEnExistExcludeSelf(key, model.Id)).WithMessage("Name Is Exist");
        }

    }
}
