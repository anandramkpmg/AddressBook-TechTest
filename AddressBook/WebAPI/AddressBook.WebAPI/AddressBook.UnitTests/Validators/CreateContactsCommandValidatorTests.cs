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
        public void ValidContacts_PassValidation()
        {

            var createCommand = new CreateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "Alpha",
                Email = "test@tes.com",
                DateOfBirth = DateTime.Today.AddDays(-1)
            };

            Assert.True(Validator.Validate(createCommand).IsValid);

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

            Assert.False(validated.IsValid);
            Assert.Single(validated.Errors);
            Assert.Equal("First Name is required.", validated.Errors[0].ErrorMessage);
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

            Assert.False(validated.IsValid);
            Assert.Single(validated.Errors);
            Assert.Equal("The First Name must be 100 characters or less.", validated.Errors[0].ErrorMessage);

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

            Assert.False(validated.IsValid);
            Assert.Single(validated.Errors);
            Assert.Equal("Sur Name is required.", validated.Errors[0].ErrorMessage);
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

            Assert.False(validated.IsValid);
            Assert.Single(validated.Errors);
            Assert.Equal("The Sur Name must be 100 characters or less.", validated.Errors[0].ErrorMessage);
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

            Assert.False(validated.IsValid);
            Assert.Single(validated.Errors);
            Assert.Equal("Date of birth should be in the past.", validated.Errors[0].ErrorMessage);
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

            Assert.False(validated.IsValid);
            Assert.Single(validated.Errors);
            Assert.Equal("Email address should be in valid format.", validated.Errors[0].ErrorMessage);
        }
    }
}
