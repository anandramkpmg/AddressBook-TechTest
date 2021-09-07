using AddressBook.WebAPI.Features.Contacts.Commands;
using FluentValidation;
using System;

namespace AddressBook.WebAPI.Validators
{
    public class CreateContactsCommandValidator : AbstractValidator<CreateContactsCommand>
    {
        public CreateContactsCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(c => c.FirstName).MaximumLength(100).WithMessage("The First Name must be 100 characters or less.");
            RuleFor(c => c.SurName).NotEmpty().WithMessage("Sur Name is required.");
            RuleFor(c => c.SurName).MaximumLength(100).WithMessage("The Sur Name must be 100 characters or less.");
            RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required.");
            RuleFor(c => c.DateOfBirth).Must(IsDateInPast).WithMessage("Date of birth should be in the past.");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email address should be in valid format.");
        }

        private bool IsDateInPast(DateTime date)
        {
            return date < DateTime.Today;
        }
    }
}

