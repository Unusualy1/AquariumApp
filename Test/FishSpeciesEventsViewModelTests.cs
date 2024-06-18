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
    public class FishSpeciesEventsViewModelTests
    {
        private FishSpeciesEventsViewModel viewModel;
        private Mock<IFishSpeciesEventRepository> mockRepository;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IFishSpeciesEventRepository>();

            var fishSpeciesId = 1;
            var mockEvents = new List<FishSpeciesEvent>
            {
                new FishSpeciesEvent { Id = 1, Description = "Event 1", FishSpeciesId = fishSpeciesId },
                new FishSpeciesEvent { Id = 2, Description = "Event 2", FishSpeciesId = fishSpeciesId },
                new FishSpeciesEvent { Id = 3, Description = "Event 3", FishSpeciesId = fishSpeciesId }
            };

            mockRepository.Setup(r => r.GetAllByFishSpeciesId(fishSpeciesId))
                          .Returns(mockEvents);

            var fishSpecies = new FishSpecies { Id = fishSpeciesId };
            viewModel = new FishSpeciesEventsViewModel(fishSpecies, mockRepository.Object);
        }

        [Test]
        public void RefreshFishSpeciesEvents_CommandExecution_LoadsEvents()
        {
            // Act
            viewModel.RefreshFishSpeciesEvents();

            // Assert
            Assert.AreEqual(3, viewModel.FishSpeciesEvents.Count);
        }

        [Test]
        public async Task DeleteFishSpeciesEvent_CommandExecution_DeletesEvent()
        {
            // Arrange
            viewModel.RefreshFishSpeciesEvents();

            var eventToDelete = viewModel.FishSpeciesEvents.Last();
            mockRepository.Setup(r => r.Delete(eventToDelete.Id)).Returns(Task.CompletedTask);

            // Act
            await viewModel.DeleteFishSpeciesEvent();

            // Assert
            Assert.IsFalse(viewModel.FishSpeciesEvents.Contains(eventToDelete));
        }

        [Test]
        public void AddFishSpeciesEvent_CommandExecution_SwapsState()
        {
            // Act
            viewModel.AddFishSpeciesEvent();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledFishSpeciesEventInfo);
        }

        [Test]
        public async Task CreateFishSpeciesEvent_CommandExecution_AddsEvent()
        {
            // Arrange
            var newEventDescription = "New Event";

            // Act
            viewModel.NewFishSpeciesEventDescription = newEventDescription;
            await viewModel.CreateFishSpeciesEvent();

            // Assert
            Assert.IsTrue(viewModel.FishSpeciesEvents.Any(e => e.Description == newEventDescription));
            Assert.IsNull(viewModel.NewFishSpeciesEventDescription);
        }

        [Test]
        public void CancelFishSpeciesEvent_CommandExecution_SwapsState()
        {
            // Arrange
            viewModel.AddFishSpeciesEvent();

            // Act
            viewModel.CancelFishSpeciesEvent();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledFishSpeciesEventInfo);
        }

    }
}
