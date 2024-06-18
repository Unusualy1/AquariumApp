using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using Moq;
using NUnit.Framework;
using ViewModel;

namespace Test
{
    [TestFixture]
    public class PlantEventsViewModelTests
    {
        private PlantEventsViewModel viewModel;
        private Mock<IPlantEventRepository> mockRepository;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IPlantEventRepository>();

            var plantId = 1;
            var mockEvents = new List<PlantEvent>
            {
                new PlantEvent { Id = 1, Description = "Event 1", PlantId = plantId },
                new PlantEvent { Id = 2, Description = "Event 2", PlantId = plantId },
                new PlantEvent { Id = 3, Description = "Event 3", PlantId = plantId }
            };

            mockRepository.Setup(r => r.GetAllByPlantId(plantId))
                          .Returns(mockEvents);

            var plant = new Plant { Id = plantId };
            viewModel = new PlantEventsViewModel(plant, mockRepository.Object);
        }

        [Test]
        public void RefreshPlantEvents_CommandExecution_LoadsEvents()
        {
            // Act
            viewModel.RefreshPlantEvents();

            // Assert
            Assert.AreEqual(3, viewModel.PlantEvents.Count);
        }

        [Test]
        public async Task DeletePlantEvent_CommandExecution_DeletesEvent()
        {
            // Arrange
            viewModel.RefreshPlantEvents();

            var eventToDelete = viewModel.PlantEvents.Last();
            mockRepository.Setup(r => r.Delete(eventToDelete.Id)).Returns(Task.CompletedTask);

            // Act
            await viewModel.DeletePlantEvent();

            // Assert
            Assert.IsFalse(viewModel.PlantEvents.Contains(eventToDelete));
        }

        [Test]
        public void AddPlantEvent_CommandExecution_SwapsState()
        {
            // Act
            viewModel.AddPlantEvent();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledPlantEventInfo);
        }

        [Test]
        public async Task CreatePlantEvent_CommandExecution_AddsEvent()
        {
            // Arrange
            var newEventDescription = "New Event";

            // Act
            viewModel.NewPlantEventDescription = newEventDescription;
            await viewModel.CreatePlantEvent();

            // Assert
            Assert.IsTrue(viewModel.PlantEvents.Any(e => e.Description == newEventDescription));
            Assert.IsNull(viewModel.NewPlantEventDescription);
        }

        [Test]
        public void CancelPlantEvent_CommandExecution_SwapsState()
        {
            // Arrange
            viewModel.AddPlantEvent();

            // Act
            viewModel.CancelPlantEvent();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledPlantEventInfo);
        }

    }
}
