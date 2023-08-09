using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPackage.Controllers;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Services;

namespace TestPackageTour
{
    public  class ItineraryTesting
    {
        [Fact]
        public async Task GetItinerary_ValidId_ReturnsItinerary()
        {
            // Arrange
            var mockService = new Mock<IItineraryService>();
            var controller = new ItineraryController(mockService.Object);
            var itineraryId = 1;
            var itinerary = new Itinerary { ItineraryId = itineraryId, DayandVisit = "Day 1", DestinationName = "Destination" };

            mockService.Setup(service => service.GetItineraryById(itineraryId)).ReturnsAsync(itinerary);

            // Act
            var actionResult = await controller.GetItinerary(itineraryId);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult?.Value);
            Assert.Equal(itinerary.ItineraryId, (okObjectResult.Value as Itinerary)?.ItineraryId);
        }
        [Fact]
        public async Task GetAllItineraries_ReturnsListOfItineraries()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Itinerary>>();
            var itineraryService = new ItineraryService(mockRepo.Object);
            var itineraries = new List<Itinerary>
            {
                new Itinerary { ItineraryId = 1, DayandVisit = "Day 1", DestinationName = "Destination 1" },
                new Itinerary { ItineraryId = 2, DayandVisit = "Day 2", DestinationName = "Destination 2" }
            };

            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(itineraries);

            // Act
            var result = await itineraryService.GetAllItineraries();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(itineraries.Count, result.Count);
        }
        [Fact]
        public async Task AddItinerary_ValidItinerary_ReturnsItinerary()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Itinerary>>();
            var itineraryService = new ItineraryService(mockRepo.Object);
            var itinerary = new Itinerary { ItineraryId = 1, DayandVisit = "Day 1", DestinationName = "Destination" };

            mockRepo.Setup(repo => repo.Add(It.IsAny<Itinerary>())).ReturnsAsync(itinerary);

            // Act
            var result = await itineraryService.AddItinerary(itinerary);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(itinerary.ItineraryId, result.ItineraryId);
        }
        [Fact]
        public async Task UpdateItinerary_ValidItinerary_ReturnsUpdatedItinerary()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Itinerary>>();
            var itineraryService = new ItineraryService(mockRepo.Object);
            var itineraryId = 1;
            var originalItinerary = new Itinerary { ItineraryId = itineraryId, DayandVisit = "Day 1", DestinationName = "Destination" };
            var updatedItinerary = new Itinerary { ItineraryId = itineraryId, DayandVisit = "Day 2", DestinationName = "Updated Destination" };

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(originalItinerary);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Itinerary>())).ReturnsAsync(updatedItinerary);

            // Act
            var result = await itineraryService.UpdateItinerary(updatedItinerary);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedItinerary.DayandVisit, result.DayandVisit);
        }
        [Fact]
        public async Task DeleteItinerary_ExistingId_ReturnsDeletedItinerary()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Itinerary>>();
            var itineraryService = new ItineraryService(mockRepo.Object);
            var itineraryId = 1;
            var itinerary = new Itinerary { ItineraryId = itineraryId, DayandVisit = "Day 1", DestinationName = "Destination" };

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(itinerary);
            mockRepo.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(itinerary);

            // Act
            var result = await itineraryService.DeleteItinerary(itineraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(itineraryId, result.ItineraryId);
        }
    }
}
