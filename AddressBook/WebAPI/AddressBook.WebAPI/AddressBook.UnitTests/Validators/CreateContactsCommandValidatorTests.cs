using AddressBook.WebAPI.Features.Contacts.Commands;
using AddressBook.WebAPI.Validators;
using System;
using Xunit;

namespace AddressBook.UnitTests.Validators
{
    public class CreateContactsCommandValidatorTests
    {
        public CreateContactsCommandValidatorTests()
        {
            Validator = new CreateContactsCommandValidator();
        }

        private CreateContactsCommandValidator Validator { get; }

        [Fact]
        public void ValidContacts_PassValidation(){

            var createCommand = new CreateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "Alpha",
                Email = "test@tes.com",
                DateOfBirth = DateTime.Today.AddDays(-1)
            };

            Assert.True(Validator.Validate(createCommand).IsValid == true);

        }

        [Fact]
        public void FirstNameNotFound_FailsValidation_WithMessage()
        {
            var createCommand = new CreateContactsCommand
            {
                FirstName = "",
                SurName = "Alpha",
                Email = "test@tes.com",
                DateOfBirth = DateTime.Today.AddDays(-1)
            };

            var validated = Validator.Validate(createCommand);

            Assert.True(validated.IsValid == false);
            Assert.True(validated.Errors.Count == 1);
            Assert.True(validated.Errors[0].ErrorMessage == "First Name is required.");
        }

        [Fact]
        public void FirstNameExceedsMaxLength_FailsValidation_WithMessage()
        {
            var createCommand = new CreateContactsCommand
            {
                FirstName = "12312312301231231230123123123012312312301231231230123123123012312312301231231230123123123012312312301",
                SurName = "Alpha",
                Email = "test@tes.com",
                DateOfBirth = DateTime.Today.AddDays(-1)
            };

            var validated = Validator.Validate(createCommand);

            Assert.True(validated.IsValid == false);
            Assert.True(validated.Errors.Count == 1);
            Assert.True(validated.Errors[0].ErrorMessage == "The First Name must be 100 characters or less.");

        }

        [Fact]
        public void SurNameNotFound_FailsValidation_WithMessage()
        {
            var createCommand = new CreateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "",
                Email = "test@tes.com",
                DateOfBirth = DateTime.Today.AddDays(-1)
            };

            var validated = Validator.Validate(createCommand);

            Assert.True(validated.IsValid == false);
            Assert.True(validated.Errors.Count == 1);
            Assert.True(validated.Errors[0].ErrorMessage == "Sur Name is required.");
        }

        [Fact]
        public void SurNameExceedsMaxLength_FailsValidation_WithMessage()
        {
            var createCommand = new CreateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "12312312301231231230123123123012312312301231231230123123123012312312301231231230123123123012312312301",
                Email = "test@tes.com",
                DateOfBirth = DateTime.Today.AddDays(-1)
            };

            var validated = Validator.Validate(createCommand);

            Assert.True(validated.IsValid == false);
            Assert.True(validated.Errors.Count == 1);
            Assert.True(validated.Errors[0].ErrorMessage == "The Sur Name must be 100 characters or less.");
        }

        [Fact]
        public void DateOfBirth_InPast_FailsValidationWithMessage()
        {
            var createCommand = new CreateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "A",
                Email = "test@tes.com",
                DateOfBirth = DateTime.Today.AddDays(1)
            };

            var validated = Validator.Validate(createCommand);

            Assert.True(validated.IsValid == false);
            Assert.True(validated.Errors.Count == 1);
            Assert.True(validated.Errors[0].ErrorMessage == "Date of birth should be in the past.");
        }

        [Fact]
        public void Email_InvalidFormat_FailsValidationWithMessage()
        {
            var createCommand = new CreateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "A",
                Email = "testtes",
                DateOfBirth = DateTime.Today.AddDays(-1)
            };

            var validated = Validator.Validate(createCommand);

            Assert.True(validated.IsValid == false);
            Assert.True(validated.Errors.Count == 1);
            Assert.True(validated.Errors[0].ErrorMessage == "Email address should be in valid format.");
        }
    }
}
