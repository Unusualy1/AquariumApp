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
    public class PlantSpeciesEventsViewModelTests
    {
        private PlantSpeciesEventsViewModel viewModel;
        private Mock<IPlantSpeciesEventRepository> mockRepository;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IPlantSpeciesEventRepository>();

            var fishId = 1;
            var mockEvents = new List<PlantSpeciesEvent>
            {
                new PlantSpeciesEvent { Id = 1, Description = "Event 1", PlantSpeciesId = fishId },
                new PlantSpeciesEvent { Id = 2, Description = "Event 2", PlantSpeciesId = fishId },
                new PlantSpeciesEvent { Id = 3, Description = "Event 3", PlantSpeciesId = fishId }
            };

            mockRepository.Setup(r => r.GetAllByPlantSpeciesId(fishId))
                          .Returns(mockEvents);

            var fish = new PlantSpecies { Id = fishId };
            viewModel = new PlantSpeciesEventsViewModel(fish, mockRepository.Object);
        }

        [Test]
        public void RefreshPlantSpeciesEvents_CommandExecution_LoadsEvents()
        {
            // Act
            viewModel.RefreshPlantSpeciesEvents();

            // Assert
            Assert.AreEqual(3, viewModel.PlantSpeciesEvents.Count);
        }

        [Test]
        public async Task DeletePlantSpeciesEvent_CommandExecution_DeletesEvent()
        {
            // Arrange
            viewModel.RefreshPlantSpeciesEvents();

            var eventToDelete = viewModel.PlantSpeciesEvents.Last();
            mockRepository.Setup(r => r.Delete(eventToDelete.Id)).Returns(Task.CompletedTask);

            // Act
            await viewModel.DeletePlantSpeciesEvent();

            // Assert
            Assert.IsFalse(viewModel.PlantSpeciesEvents.Contains(eventToDelete));
        }

        [Test]
        public void AddPlantSpeciesEvent_CommandExecution_SwapsState()
        {
            // Act
            viewModel.AddPlantSpeciesEvent();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledPlantSpeciesEventInfo);
        }

        [Test]
        public async Task CreatePlantSpeciesEvent_CommandExecution_AddsEvent()
        {
            // Arrange
            var newEventDescription = "New Event";

            // Act
            viewModel.NewPlantSpeciesEventDescription = newEventDescription;
            await viewModel.CreatePlantSpeciesEvent();

            // Assert
            Assert.IsTrue(viewModel.PlantSpeciesEvents.Any(e => e.Description == newEventDescription));
            Assert.IsNull(viewModel.NewPlantSpeciesEventDescription);
        }

        [Test]
        public void CancelPlantSpeciesEvent_CommandExecution_SwapsState()
        {
            // Arrange
            viewModel.AddPlantSpeciesEvent();

            // Act
            viewModel.CancelPlantSpeciesEvent();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledPlantSpeciesEventInfo);
        }

    }
}
