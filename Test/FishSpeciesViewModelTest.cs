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
    public class FishSpeciesViewModelTests
    {
        private FishSpeciesViewModel viewModel;
        private Mock<IFishSpeciesRepository> mockFishSpeciesRepository;
        private Mock<IFishSpeciesEventRepository> mockFishSpeciesEventRepository;
        private ObservableCollection<FishSpecies> fishSpecies;

        [SetUp]
        public void Setup()
        {
            mockFishSpeciesRepository = new Mock<IFishSpeciesRepository>();
            mockFishSpeciesEventRepository = new Mock<IFishSpeciesEventRepository>();

            var mockFishSpecies = new List<FishSpecies>
            {
                new FishSpecies { Id = 1, Name = "Fish Species 1" },
                new FishSpecies { Id = 2, Name = "Fish Species 2" },
                new FishSpecies { Id = 3, Name = "Fish Species 3" }
            };
            fishSpecies = new ObservableCollection<FishSpecies>(mockFishSpecies);

            mockFishSpeciesRepository.Setup(r => r.GetAll())
                                     .Returns(mockFishSpecies);

            mockFishSpeciesRepository.Setup(r => r.Add(It.IsAny<FishSpecies>()))
                                     .Callback((FishSpecies fish) =>
                                     {
                                         fishSpecies.Add(fish);
                                     })
                                     .Returns(Task.CompletedTask);

            mockFishSpeciesRepository.Setup(r => r.Delete(It.IsAny<long>()))
                                     .Callback((long id) =>
                                     {
                                         var fishToRemove = fishSpecies.FirstOrDefault(f => f.Id == id);
                                         if (fishToRemove != null)
                                             fishSpecies.Remove(fishToRemove);
                                     })
                                     .Returns(Task.CompletedTask);

            viewModel = new FishSpeciesViewModel(mockFishSpeciesEventRepository.Object, mockFishSpeciesRepository.Object);
            viewModel.FishSpecies = fishSpecies;
        }

        [Test]
        public void Constructor_InitializesProperties()
        {
            // Assert
            Assert.IsNotNull(viewModel.FishSpecies);
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledFishSpeciesInfo);
        }

        [Test]
        public void AddFishSpecies_CommandExecution_SwapsState()
        {
            // Act
            viewModel.AddFishSpecies();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledFishSpeciesInfo);
        }

        [Test]
        public void EditFishSpecies_CommandExecution_SwapsState()
        {
            // Arrange
            viewModel.CurrentFishSpecies = viewModel.FishSpecies.First();

            // Act
            viewModel.EditFishSpecies();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledFishSpeciesInfo);
        }

        [Test]
        public void ShowFishSpeciesEvents_CommandExecution_CreatesViewModel()
        {
            // Arrange
            viewModel.CurrentFishSpecies = viewModel.FishSpecies.First();

            // Act
            var eventsViewModel = viewModel.CreateFishSpeciesEventsViewModel();

            // Assert
            Assert.IsNotNull(eventsViewModel);
            Assert.AreEqual(viewModel.CurrentFishSpecies, eventsViewModel.CreatedWindowFishSpecies);
        }

        [Test]
        public async Task ApplyFishSpecies_AddCommand_AddsFishSpecies()
        {
            // Arrange
            var newFishSpecies = new FishSpecies { Id = 4, Name = "New Fish Species" };

            viewModel.AddFishSpecies();
            viewModel.CurrentFishSpecies = newFishSpecies;

            await viewModel.ApplyFishSpecies();

            // Assert
            Assert.IsTrue(viewModel.FishSpecies.Contains(newFishSpecies));
        }

        [Test]
        public async Task CancelFishSpecies_AddCommand_CancelsAddition()
        {
            // Arrange
            viewModel.AddFishSpecies(); // Enter add mode

            // Act
            viewModel.CancelFishSpecies();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledFishSpeciesInfo);
            Assert.IsNull(viewModel.CurrentFishSpecies);
        }

        [Test]
        public async Task CancelFishSpecies_EditCommand_RestoresOriginalFishSpecies()
        {
            // Arrange
            var originalFishSpecies = viewModel.FishSpecies.First();
            viewModel.EditFishSpecies();

            var editedFishSpecies = new FishSpecies { Id = originalFishSpecies.Id, Name = "Edited Fish Species" };

            // Act
            viewModel.CurrentFishSpecies = editedFishSpecies;
            await viewModel.CancelFishSpecies();

            // Assert
            Assert.AreEqual(originalFishSpecies, viewModel.FishSpecies.First(f => f.Id == originalFishSpecies.Id));
        }

        [Test]
        public async Task DeleteFishSpecies_CommandExecution_DeletesFishSpecies()
        {
            // Arrange
            var fishSpeciesToDelete = viewModel.FishSpecies.First(f => f.Id == 2);

            // Act
            viewModel.CurrentFishSpecies = fishSpeciesToDelete;
            await viewModel.DeleteFishSpecies();

            // Assert
            Assert.IsFalse(viewModel.FishSpecies.Contains(fishSpeciesToDelete));
        }

        [Test]
        public void RefreshFishSpecies_CommandExecution_LoadsFishSpecies()
        {
            // Act
            viewModel.RefreshFishSpecies();

            // Assert
            Assert.AreEqual(3, viewModel.FishSpecies.Count);
        }
    }
}
