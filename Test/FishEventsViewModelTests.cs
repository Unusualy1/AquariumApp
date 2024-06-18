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
    public class FishEventsViewModelTests
    {
        private FishEventsViewModel viewModel;
        private Mock<IFishEventRepository> mockRepository;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IFishEventRepository>();

            var fishId = 1;
            var mockEvents = new List<FishEvent>
            {
                new FishEvent { Id = 1, Description = "Event 1", FishId = fishId },
                new FishEvent { Id = 2, Description = "Event 2", FishId = fishId },
                new FishEvent { Id = 3, Description = "Event 3", FishId = fishId }
            };

            mockRepository.Setup(r => r.GetAllByFishId(fishId))
                          .Returns(mockEvents);

            var fish = new Fish { Id = fishId };
            viewModel = new FishEventsViewModel(fish, mockRepository.Object);
        }

        [Test]
        public void RefreshFishEvents_CommandExecution_LoadsEvents()
        {
            // Act
            viewModel.RefreshFishEvents();

            // Assert
            Assert.AreEqual(3, viewModel.FishEvents.Count);
        }

        [Test]
        public async Task DeleteFishEvent_CommandExecution_DeletesEvent()
        {
            // Arrange
            viewModel.RefreshFishEvents();

            var eventToDelete = viewModel.FishEvents.Last();
            mockRepository.Setup(r => r.Delete(eventToDelete.Id)).Returns(Task.CompletedTask);

            // Act
            await viewModel.DeleteFishEvent();

            // Assert
            Assert.IsFalse(viewModel.FishEvents.Contains(eventToDelete));
        }

        [Test]
        public void AddFishEvent_CommandExecution_SwapsState()
        {
            // Act
            viewModel.AddFishEvent();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledFishEventInfo);
        }

        [Test]
        public async Task CreateFishEvent_CommandExecution_AddsEvent()
        {
            // Arrange
            var newEventDescription = "New Event";

            // Act
            viewModel.NewFishEventDescription = newEventDescription;
            await viewModel.CreateFishEvent();

            // Assert
            Assert.IsTrue(viewModel.FishEvents.Any(e => e.Description == newEventDescription));
            Assert.IsNull(viewModel.NewFishEventDescription);
        }

        [Test]
        public void CancelFishEvent_CommandExecution_SwapsState()
        {
            // Arrange
            viewModel.AddFishEvent();

            // Act
            viewModel.CancelFishEvent();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledFishEventInfo);
        }

    }
}
