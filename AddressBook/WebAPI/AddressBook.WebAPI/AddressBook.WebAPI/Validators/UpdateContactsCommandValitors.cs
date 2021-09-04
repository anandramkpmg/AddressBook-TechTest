using AddressBook.WebAPI.Features.Contacts.Commands;
using FluentValidation;
using System;

namespace AddressBook.WebAPI.Validators
{
    public class UpdateContactsCommandValitor : AbstractValidator<UpdateContactsCommand>
    {
        public UpdateContactsCommandValitor()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.FirstName).MaximumLength(100);
            RuleFor(c => c.SurName).NotEmpty();
            RuleFor(c => c.SurName).MaximumLength(100);
            RuleFor(c => c.DateOfBirth).NotEmpty();
            RuleFor(c => c.DateOfBirth).Must(IsDateInPast).WithMessage("Date of birth should be in the past.");
            RuleFor(c => c.Email).EmailAddress();
        }

        private bool IsDateInPast(DateTime date)
        {
            return DateTime.Today > date;
        }
    }
}

