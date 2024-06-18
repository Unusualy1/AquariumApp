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
    public class DecorationEventsViewModelTests
    {
        private DecorationEventsViewModel viewModel;
        private Mock<IDecorationEventRepository> mockRepository;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IDecorationEventRepository>();

            var decorationId = 1;
            var mockEvents = new List<DecorationEvent>
            {
                new DecorationEvent { Id = 1, Description = "Event 1", DecorationId = decorationId },
                new DecorationEvent { Id = 2, Description = "Event 2", DecorationId = decorationId },
                new DecorationEvent { Id = 3, Description = "Event 3", DecorationId = decorationId }
            };

            mockRepository.Setup(r => r.GetAllByDecorationId(decorationId))
                          .Returns(mockEvents);

            var decoration = new Decoration { Id = decorationId };
            viewModel = new DecorationEventsViewModel(decoration, mockRepository.Object);
        }

        [Test]
        public void RefreshDecorationEvents_CommandExecution_LoadsEvents()
        {
            // Act
            viewModel.RefreshDecorationEvents();

            // Assert
            Assert.AreEqual(3, viewModel.DecorationEvents.Count); 
        }

        [Test]
        public async Task DeleteDecorationEvent_CommandExecution_DeletesEvent()
        {
            // Arrange
            viewModel.RefreshDecorationEvents(); 

            var eventToDelete = viewModel.DecorationEvents.Last(); 
            mockRepository.Setup(r => r.Delete(eventToDelete.Id)).Returns(Task.CompletedTask);

            // Act
            await viewModel.DeleteDecorationEvent();

            // Assert
            Assert.IsFalse(viewModel.DecorationEvents.Contains(eventToDelete));
        }

        [Test]
        public void AddDecorationEvent_CommandExecution_SwapsState()
        {
            // Act
            viewModel.AddDecorationEvent();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledDecorationEventInfo);
        }

        [Test]
        public async Task CreateDecorationEvent_CommandExecution_AddsEvent()
        {
            // Arrange
            var newEventDescription = "New Event";

            // Act
            viewModel.NewDecorationEventDescription = newEventDescription;
            await viewModel.CreateDecorationEvent();

            // Assert
            Assert.IsTrue(viewModel.DecorationEvents.Any(e => e.Description == newEventDescription));
            Assert.IsNull(viewModel.NewDecorationEventDescription); 
        }

        [Test]
        public void CancelDecorationEvent_CommandExecution_SwapsState()
        {
            // Arrange
            viewModel.AddDecorationEvent();

            // Act
            viewModel.CancelDecorationEvent();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid); 
            Assert.IsFalse(viewModel.IsEnabledDecorationEventInfo); 
        }

    }
}
