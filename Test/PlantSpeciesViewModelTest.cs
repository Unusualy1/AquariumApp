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
    public class PlantSpeciesViewModelTests
    {
        private PlantSpeciesViewModel viewModel;
        private Mock<IPlantSpeciesRepository> mockPlantSpeciesRepository;
        private Mock<IPlantSpeciesEventRepository> mockPlantSpeciesEventRepository;
        private ObservableCollection<PlantSpecies> plantSpecies;

        [SetUp]
        public void Setup()
        {
            mockPlantSpeciesRepository = new Mock<IPlantSpeciesRepository>();
            mockPlantSpeciesEventRepository = new Mock<IPlantSpeciesEventRepository>();

            var mockPlantSpecies = new List<PlantSpecies>
            {
                new PlantSpecies { Id = 1, Name = "Plant Species 1" },
                new PlantSpecies { Id = 2, Name = "Plant Species 2" },
                new PlantSpecies { Id = 3, Name = "Plant Species 3" }
            };
            plantSpecies = new ObservableCollection<PlantSpecies>(mockPlantSpecies);

            mockPlantSpeciesRepository.Setup(r => r.GetAll())
                                     .Returns(mockPlantSpecies);

            mockPlantSpeciesRepository.Setup(r => r.Add(It.IsAny<PlantSpecies>()))
                                     .Callback((PlantSpecies plant) =>
                                     {
                                         plantSpecies.Add(plant);
                                     })
                                     .Returns(Task.CompletedTask);

            mockPlantSpeciesRepository.Setup(r => r.Delete(It.IsAny<long>()))
                                     .Callback((long id) =>
                                     {
                                         var plantToRemove = plantSpecies.FirstOrDefault(f => f.Id == id);
                                         if (plantToRemove != null)
                                             plantSpecies.Remove(plantToRemove);
                                     })
                                     .Returns(Task.CompletedTask);

            viewModel = new PlantSpeciesViewModel(mockPlantSpeciesEventRepository.Object, mockPlantSpeciesRepository.Object);
            viewModel.PlantSpecies = plantSpecies;
        }

        [Test]
        public void Constructor_InitializesProperties()
        {
            // Assert
            Assert.IsNotNull(viewModel.PlantSpecies);
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledPlantSpeciesInfo);
        }

        [Test]
        public void AddPlantSpecies_CommandExecution_SwapsState()
        {
            // Act
            viewModel.AddPlantSpecies();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledPlantSpeciesInfo);
        }

        [Test]
        public void EditPlantSpecies_CommandExecution_SwapsState()
        {
            // Arrange
            viewModel.CurrentPlantSpecies = viewModel.PlantSpecies.First();

            // Act
            viewModel.EditPlantSpecies();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledPlantSpeciesInfo);
        }

        [Test]
        public void ShowPlantSpeciesEvents_CommandExecution_CreatesViewModel()
        {
            // Arrange
            viewModel.CurrentPlantSpecies = viewModel.PlantSpecies.First();

            // Act
            var eventsViewModel = viewModel.CreatePlantSpeciesEventsViewModel();

            // Assert
            Assert.IsNotNull(eventsViewModel);
            Assert.AreEqual(viewModel.CurrentPlantSpecies, eventsViewModel.CreatedWindowPlantSpecies);
        }

        [Test]
        public async Task ApplyPlantSpecies_AddCommand_AddsPlantSpecies()
        {
            // Arrange
            var newPlantSpecies = new PlantSpecies { Id = 4, Name = "New Plant Species" };

            viewModel.AddPlantSpecies();
            viewModel.CurrentPlantSpecies = newPlantSpecies;

            await viewModel.ApplyPlantSpecies();

            // Assert
            Assert.IsTrue(viewModel.PlantSpecies.Contains(newPlantSpecies));
        }

        [Test]
        public async Task CancelPlantSpecies_AddCommand_CancelsAddition()
        {
            // Arrange
            viewModel.AddPlantSpecies(); // Enter add mode

            // Act
            viewModel.CancelPlantSpecies();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledPlantSpeciesInfo);
            Assert.IsNull(viewModel.CurrentPlantSpecies);
        }

        [Test]
        public async Task CancelPlantSpecies_EditCommand_RestoresOriginalPlantSpecies()
        {
            // Arrange
            var originalPlantSpecies = viewModel.PlantSpecies.First();
            viewModel.EditPlantSpecies();

            var editedPlantSpecies = new PlantSpecies { Id = originalPlantSpecies.Id, Name = "Edited Plant Species" };

            // Act
            viewModel.CurrentPlantSpecies = editedPlantSpecies;
            await viewModel.CancelPlantSpecies();

            // Assert
            Assert.AreEqual(originalPlantSpecies, viewModel.PlantSpecies.First(f => f.Id == originalPlantSpecies.Id));
        }

        [Test]
        public async Task DeletePlantSpecies_CommandExecution_DeletesPlantSpecies()
        {
            // Arrange
            var plantSpeciesToDelete = viewModel.PlantSpecies.First(f => f.Id == 2);

            // Act
            viewModel.CurrentPlantSpecies = plantSpeciesToDelete;
            await viewModel.DeletePlantSpecies();

            // Assert
            Assert.IsFalse(viewModel.PlantSpecies.Contains(plantSpeciesToDelete));
        }

        [Test]
        public void RefreshPlantSpecies_CommandExecution_LoadsPlantSpecies()
        {
            // Act
            viewModel.RefreshPlantSpecies();

            // Assert
            Assert.AreEqual(3, viewModel.PlantSpecies.Count);
        }
    }
}
