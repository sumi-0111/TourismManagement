using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Services;

namespace TestPackageTour
{
    public class TestingContactDetails
    {
        [Fact]
        public async Task AddContactDetails_ValidContactDetails_ReturnsContactDetails()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, ContactDetails>>();
            var mockLogger = new Mock<ILogger<ContactDetails>>();
            var contactDetailsService = new ContactDetailsService(mockRepo.Object, mockLogger.Object);
            var contactDetails = new ContactDetails { ContactId = 1, AgentName = "Agent", AgentPhoneNo = "123456789", AgentEmail = "agent@example.com" };

            mockRepo.Setup(repo => repo.Add(It.IsAny<ContactDetails>())).ReturnsAsync(contactDetails);

            // Act
            var result = await contactDetailsService.AddContactDetails(contactDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contactDetails.ContactId, result.ContactId);
        }
        [Fact]
        public async Task GetAllContactDetails_ReturnsAllContactDetails()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, ContactDetails>>();
            var mockLogger = new Mock<ILogger<ContactDetails>>();
            var contactDetailsService = new ContactDetailsService(mockRepo.Object, mockLogger.Object);

            var contactDetailsList = new List<ContactDetails>
            {
                new ContactDetails { ContactId = 1, AgentName = "Agent1" },
                new ContactDetails { ContactId = 2, AgentName = "Agent2" },
                // Add more ContactDetails as needed...
            };

            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(contactDetailsList);

            // Act
            var result = await contactDetailsService.GetAllContactDetails();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contactDetailsList.Count, result.Count);
        }
        [Fact]
        public async Task UpdateContactDetails_ValidContactDetails_ReturnsUpdatedContactDetails()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, ContactDetails>>();
            var mockLogger = new Mock<ILogger<ContactDetails>>();
            var contactDetailsService = new ContactDetailsService(mockRepo.Object, mockLogger.Object);

            var contactDetails = new ContactDetails { ContactId = 1, AgentName = "Agent1" };

            mockRepo.Setup(repo => repo.Update(It.IsAny<ContactDetails>())).ReturnsAsync(contactDetails);

            // Act
            var result = await contactDetailsService.UpdateContactDetails(contactDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contactDetails.AgentName, result.AgentName);
        }
        [Fact]
        public async Task GetContactDetails_ValidContactId_ReturnsContactDetails()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, ContactDetails>>();
            var mockLogger = new Mock<ILogger<ContactDetails>>();
            var contactDetailsService = new ContactDetailsService(mockRepo.Object, mockLogger.Object);

            var contactDetails = new ContactDetails { ContactId = 1, AgentName = "Agent1" };

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(contactDetails);

            // Act
            var result = await contactDetailsService.GetContactDetails(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contactDetails.ContactId, result.ContactId);
        }
        [Fact]
        public async Task DeleteContactDetails_ValidContactId_ReturnsDeletedContactDetails()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, ContactDetails>>();
            var mockLogger = new Mock<ILogger<ContactDetails>>();
            var contactDetailsService = new ContactDetailsService(mockRepo.Object, mockLogger.Object);

            var contactDetails = new ContactDetails { ContactId = 1, AgentName = "Agent1" };

            mockRepo.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(contactDetails);

            // Act
            var result = await contactDetailsService.DeleteContactDetails(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contactDetails.ContactId, result.ContactId);
        }
    }
}
