using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using Model.Enums;
using Model.Factories;
using Moq;
using NUnit.Framework;
using ViewModel;
using ViewModel.Enums;

namespace Test
{
    [TestFixture]
    public class DecorationsViewModelTests
    {
        private DecorationsViewModel viewModel;
        private Mock<IDecorationRepository> mockDecorationRepository;
        private Mock<IDecorationEventRepository> mockDecorationEventRepository;
        private ObservableCollection<Decoration> decorations;

        [SetUp]
        public void Setup()
        {
            mockDecorationRepository = new Mock<IDecorationRepository>();
            mockDecorationEventRepository = new Mock<IDecorationEventRepository>();

            var mockDecorations = new List<Decoration>
            {
                new Decoration { Id = 1, Name = "Decoration 1" },
                new Decoration { Id = 2, Name = "Decoration 2" },
                new Decoration { Id = 3, Name = "Decoration 3" }
            };
            decorations = new ObservableCollection<Decoration>(mockDecorations);

            mockDecorationRepository.Setup(r => r.GetAll())
                                    .Returns(mockDecorations);

            mockDecorationRepository.Setup(r => r.Add(It.IsAny<Decoration>()))
                                    .Callback((Decoration decoration) =>
                                    {
                                        decorations.Add(decoration);
                                    })
                                    .Returns(Task.CompletedTask);

            mockDecorationRepository.Setup(r => r.Delete(It.IsAny<long>()))
                                    .Callback((long id) =>
                                    {
                                        var decorationToRemove = decorations.FirstOrDefault(d => d.Id == id);
                                        if (decorationToRemove != null)
                                            decorations.Remove(decorationToRemove);
                                    })
                                    .Returns(Task.CompletedTask);

            viewModel = new DecorationsViewModel(mockDecorationEventRepository.Object, mockDecorationRepository.Object);
            viewModel.Decorations = decorations;
        }

        [Test]
        public void Constructor_InitializesProperties()
        {
            // Assert
            Assert.IsNotNull(viewModel.Decorations);
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledDecorationInfo);
        }

        [Test]
        public void AddDecoration_CommandExecution_SwapsState()
        {
            // Act
            viewModel.AddDecoration();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid); 
            Assert.IsTrue(viewModel.IsEnabledDecorationInfo); 
        }

        [Test]
        public void EditDecoration_CommandExecution_SwapsState()
        {
            // Arrange
            viewModel.CurrentDecoration = viewModel.Decorations.First(); 

            // Act
            viewModel.EditDecoration();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid); 
            Assert.IsTrue(viewModel.IsEnabledDecorationInfo); 
        }

        [Test]
        public void ShowDecorationEvents_CommandExecution_CreatesViewModel()
        {
            // Arrange
            viewModel.CurrentDecoration = viewModel.Decorations.First(); 

            // Act
            var eventsViewModel = viewModel.CreateDecorationEventsViewModel();

            // Assert
            Assert.IsNotNull(eventsViewModel); 
            Assert.AreEqual(viewModel.CurrentDecoration, eventsViewModel.CreatedWindowDecoration); 
        }

        [Test]
        public async Task ApplyDecoration_AddCommand_AddsDecoration()
        {
            // Arrange
            var newDecoration = new Decoration { Id = 4, Name = "New Decoration" };


            viewModel.CurrentDecoration = newDecoration;
            viewModel.SwapState(State.OnAdd);

            await viewModel.ApplyDecoration(); 
            // Assert
            Assert.IsTrue(viewModel.Decorations.Contains(newDecoration)); 
        }

        [Test]
        public async Task CancelDecoration_AddCommand_CancelsAddition()
        {
            // Arrange
            viewModel.AddDecoration(); // Enter add mode

            // Act
            viewModel.CancelDecoration();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid); 
            Assert.IsFalse(viewModel.IsEnabledDecorationInfo); 
            Assert.IsNull(viewModel.CurrentDecoration); 
        }

        [Test]
        public async Task CancelDecoration_EditCommand_RestoresOriginalDecoration()
        {
            // Arrange
            var originalDecoration = viewModel.Decorations.First(); 
            viewModel.EditDecoration(); 

            var editedDecoration = new Decoration { Id = originalDecoration.Id, Name = "Edited Decoration" };

            // Act
            viewModel.CurrentDecoration = editedDecoration;
            await viewModel.CancelDecoration();

            await Task.Delay(1000);

            // Assert
            Assert.AreEqual(originalDecoration, viewModel.Decorations.First(d => d.Id == originalDecoration.Id)); 
        }

        [Test]
        public async Task DeleteDecoration_CommandExecution_DeletesDecoration()
        {
            // Arrange
            var decorationToDelete = viewModel.Decorations.First(d => d.Id == 2); 

            // Act
            viewModel.CurrentDecoration = decorationToDelete; 
            await viewModel.DeleteDecoration();

            // Assert
            Assert.IsFalse(viewModel.Decorations.Contains(decorationToDelete)); 
        }

        [Test]
        public void RefreshDecorations_CommandExecution_LoadsDecorations()
        {
            // Act
            viewModel.RefreshDecorations();

            // Assert
            Assert.AreEqual(3, viewModel.Decorations.Count); 
        }
    }
}
