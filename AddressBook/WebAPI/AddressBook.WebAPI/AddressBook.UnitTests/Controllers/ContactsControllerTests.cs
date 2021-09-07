using AddressBook.WebAPI.Controllers;
using AddressBook.WebAPI.Features.Contacts.Commands;
using AddressBook.WebAPI.Features.Contacts.Queries;
using AddressBook.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AddressBook.UnitTests.Controllers
{
    public class ContactsControllerTests
    {
        [Fact]
        public async void GetContactById_ValidRequest_ReturnsValidContact()
        {
            var mockMediator = new Mock<IMediator>();

            var controller = new ContactsController(mockMediator.Object);

            var contact = new Contact
            {
                FirstName = "Alpha",
                SurName = "A",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Today,
                Id = 1
            };

            mockMediator
            .Setup(m => m.Send(It.IsAny<GetContactByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(contact) //<-- return Task to allow await to continue
            .Verifiable("Notification was not sent.");

            // act
            var result = await controller.GetById(1);
            var okResult = result as OkObjectResult;
            mockMediator.Verify(x => x.Send(It.IsAny<GetContactByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(((Contact)okResult.Value).Id, contact.Id);
        }


        [Fact]
        public async void GetAllContacts_ValidRequest_ReturnsAllContacts()
        {
            var mockMediator = new Mock<IMediator>();

            var controller = new ContactsController(mockMediator.Object);

            var contacts = new List<Contact>(1)
            {
                new Contact
                {
                    FirstName = "Alpha",
                    SurName = "A",
                    Email = "test@gmail.com",
                    DateOfBirth = DateTime.Today,
                    Id = 1
                }
            };

            mockMediator
            .Setup(m => m.Send(It.IsAny<GetAllContactsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(contacts) //<-- return Task to allow await to continue
            .Verifiable("Notification was not sent.");

            // act
            var result = await controller.GetAll();
            var okResult = result as OkObjectResult;
            mockMediator.Verify(x => x.Send(It.IsAny<GetAllContactsQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(1, ((ICollection<Contact>)okResult.Value).Count);
        }

        [Fact]
        public async void CreateContacts_ValidCommand_CreatesAndReturnsCandidate()
        {
            var mockMediator = new Mock<IMediator>();

            var controller = new ContactsController(mockMediator.Object);

            var contacts = new List<Contact>(1);

            var createContactsCommand = new CreateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "A",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Today,
                Id = 1
            };

            mockMediator
            .Setup(m => m.Send(It.IsAny<CreateContactsCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1) //<-- return Task to allow await to continue
            .Verifiable("Notification was not sent.");

            // act
            var result = await controller.Create(createContactsCommand);
            var okResult = result as OkObjectResult;
            mockMediator.Verify(x => x.Send(It.IsAny<CreateContactsCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(1, (int)okResult.Value);
        }

        [Fact]
        public async void UpdateContacts_MatchesContact_UpdatesContacts()
        {
            var mockMediator = new Mock<IMediator>();

            var controller = new ContactsController(mockMediator.Object);

            var updateContactsCommand = new UpdateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "A",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Today,
                Id = 1
            };

            mockMediator
            .Setup(m => m.Send(It.IsAny<UpdateContactsCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1) //<-- return Task to allow await to continue
            .Verifiable("Notification was not sent.");

            // act
            var result = await controller.Update(1, updateContactsCommand);
            var okResult = result as OkObjectResult;
            mockMediator.Verify(x => x.Send(It.IsAny<UpdateContactsCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(1, ((int)okResult.Value));
        }

        [Fact]
        public async void UpdateContactsCommand_IdMismatch_ReturnsBadRequest()
        {
            var mockMediator = new Mock<IMediator>();

            var controller = new ContactsController(mockMediator.Object);

            var updateContactsCommand = new UpdateContactsCommand
            {
                FirstName = "Alpha",
                SurName = "A",
                Email = "test@gmail.com",
                DateOfBirth = DateTime.Today,
                Id = 1
            };

            mockMediator
            .Setup(m => m.Send(It.IsAny<UpdateContactsCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1) //<-- return Task to allow await to continue
            .Verifiable("Notification was not sent.");

            // act
            var result = await controller.Update(2, updateContactsCommand);

            // assert
            var badRequest = result as BadRequestResult;
            var okResult = result as OkObjectResult;
            mockMediator.Verify(x => x.Send(It.IsAny<CreateContactsCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.Null(okResult);
            Assert.Equal(400, badRequest.StatusCode);
        }

    }
}
