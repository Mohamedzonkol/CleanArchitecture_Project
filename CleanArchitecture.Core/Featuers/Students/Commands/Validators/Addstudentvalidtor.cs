using CleanArchitecture.Core.Featuers.Students.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Services.Abstract;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Students.Commands.Validators
{
    public class Addstudentvalidtor : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentServices _studentServices;
        private readonly IDepartmentServices _departmentServices;
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer;

        public Addstudentvalidtor(IStudentServices studentServices, IDepartmentServices departmentServices, IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        {
            _studentServices = studentServices;
            _departmentServices = departmentServices;
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
            //.Matches(@"[]");//regular Expression
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SheardResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SheardResoursesKeys.NotNull])
                .MaximumLength(10).WithMessage("Max Length Is 10");
            //.Matches(@"[]");//regular Expression
        }

        private void ApplyCustomValidationRule()
        {
            RuleFor(x => x.NameAr).MustAsync(async (key, CancellationToken) =>
                !await _studentServices.IsNameArExist(key)).WithMessage("Name Is Exist");
            RuleFor(x => x.NameEn).MustAsync(async (key, CancellationToken) =>
                !await _studentServices.IsNameEnExist(key)).WithMessage("Name Is Exist");
            RuleFor(x => x.DepartmentId).MustAsync(async (key, CancellationToken) =>
                await _departmentServices.IsDepartmentExist(key)).WithMessage("This Department Id Is Not Exist");
        }
    }
}
