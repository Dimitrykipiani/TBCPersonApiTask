using Domain.Enums;
using Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Models.PersonAggregate.Validations
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            // Firstname validations

            RuleFor(x => x.FistName).NotEmpty().WithMessage(AppStrings.Required);
            RuleFor(x => x.FistName).MinimumLength(2).WithMessage(AppStrings.MinLengthError);
            RuleFor(x => x.FistName).MaximumLength(50).WithMessage(AppStrings.MinLengthError);
            RuleFor(x => x.FistName).Must(firstName => ValidationHelperMethods.ShouldContainOnlyLatinOrOnlyGeorgianCharacters(firstName)).WithMessage(AppStrings.WrongFormat);

            // Lastname Validations

            RuleFor(x => x.LastName).NotEmpty().WithMessage(AppStrings.Required);
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage(AppStrings.MinLengthError);
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage(AppStrings.MinLengthError);
            RuleFor(x => x.LastName).Must(lastName => ValidationHelperMethods.ShouldContainOnlyLatinOrOnlyGeorgianCharacters(lastName)).WithMessage(AppStrings.WrongFormat);

            // Sex validations

            RuleFor(x => x.Sex).Must(sex => ValidationHelperMethods.MustBeFemaleOrMale(sex)).WithMessage(AppStrings.SexError);

            //Private number validations

            RuleFor(x => x.PrivateNumber).NotEmpty().WithMessage(AppStrings.Required);
            RuleFor(x => x.PrivateNumber).Must(pn => pn.Length == 11).WithMessage(AppStrings.PrivateNumberLengthError);

            // Birthdate validations

            RuleFor(x => x.BirthDate).NotEmpty().WithMessage(AppStrings.Required);
            RuleFor(x => x.BirthDate).Must(bd => DateTime.Now.AddYears(-18) > bd).WithMessage(AppStrings.EighteenYearsError);

            // PhoneNumber validations

            RuleFor(x => x.PhoneNumbers).Must(pns => ValidationHelperMethods.PhoneNumbersAreValid(pns)).WithMessage(AppStrings.PhoneNumberError);

            // Related people validations

            RuleFor(x => x.RelatedPeople).Must(rp => ValidationHelperMethods.RelatedPeopleAreValid(rp)).WithMessage(AppStrings.RelatedPersonValidation);
        }
    }
}
