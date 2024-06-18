using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Model.DataAccess.Repositories;
using Model;
using Moq;
using NUnit.Framework;
using ViewModel;
using ViewModel.Enums;
using ViewModel.UseCases;

namespace Test
{
    [TestFixture]
    public class PlantViewModelTests
    {
        private PlantsViewModel viewModel;
        private Mock<IPlantRepository> mockPlantRepository;
        private Mock<IPlantEventRepository> mockPlantEventRepository;
        private Mock<IPlantSpeciesRepository> mockPlantSpeciesRepository;

        [SetUp]
        public void Setup()
        {
            mockPlantRepository = new Mock<IPlantRepository>();
            mockPlantEventRepository = new Mock<IPlantEventRepository>();
            mockPlantSpeciesRepository = new Mock<IPlantSpeciesRepository>();

            var fishes = new List<Plant>
            {
                new Plant { Id = 1, Name = "Plant 1", PlantSpeciesId = 1 },
                new Plant { Id = 2, Name = "Plant 2", PlantSpeciesId = 2 },
                new Plant { Id = 3, Name = "Plant 3", PlantSpeciesId = 1 }
            };
            var fishSpecies = new List<PlantSpecies>
            {
                new PlantSpecies { Id = 1, Name = "Species 1" },
                new PlantSpecies { Id = 2, Name = "Species 2" }
            };

            mockPlantRepository.Setup(r => r.GetAll()).Returns(fishes);
            mockPlantSpeciesRepository.Setup(r => r.GetAll()).Returns(fishSpecies);

            viewModel = new PlantsViewModel(mockPlantRepository.Object, mockPlantEventRepository.Object, mockPlantSpeciesRepository.Object);
            viewModel.RefreshPlants();
        }

        [Test]
        public void EditPlant_CommandExecution_SetsState()
        {
            // Arrange
            var initialPlant = viewModel.Plants.FirstOrDefault();

            // Act
            viewModel.CurrentPlant = initialPlant;
            viewModel.EditPlant();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid);
            Assert.IsTrue(viewModel.IsEnabledPlantInfo);
        }


        [Test]
        public async Task ApplyPlant_AddCommand_AddsPlantAndEvents()
        {
            // Arrange
            var newPlant = new Plant
            {
                Name = "New Plant",
                PlantSpecies = viewModel.PlantSpecies.FirstOrDefault()
            };

            // Act
            viewModel.AddPlant();
            viewModel.CurrentPlant = newPlant;
            await viewModel.ApplyPlant();

            // Assert 
            Assert.IsTrue(viewModel.Plants.Contains(newPlant));
            Assert.IsNull(viewModel.CurrentPlant);
        }

        [Test]
        public async Task CancelPlant_AddCommand_CancelsAddition()
        {
            // Arrange
            viewModel.AddPlant(); // Enter add mode

            // Act
            await viewModel.CancelPlant();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid);
            Assert.IsFalse(viewModel.IsEnabledPlantInfo);
            Assert.IsNull(viewModel.CurrentPlant);
        }

        [Test]
        public async Task CancelPlant_EditCommand_RestoresOriginalPlant()
        {
            // Arrange
            var initialPlant = viewModel.Plants.FirstOrDefault();
            viewModel.EditPlant();

            var editedPlant = new Plant
            {
                Id = initialPlant.Id,
                Name = "Edited Plant",
                PlantSpecies = initialPlant.PlantSpecies
            };

            // Act
            viewModel.CurrentPlant = editedPlant;
            await viewModel.CancelPlant();

            // Assert
            Assert.AreEqual(initialPlant, viewModel.Plants.FirstOrDefault(f => f.Id == initialPlant.Id));
        }

        [Test]
        public async Task DeletePlant_CommandExecution_DeletesPlant()
        {
            // Arrange
            var fishToDelete = viewModel.Plants.FirstOrDefault();

            // Act
            viewModel.CurrentPlant = fishToDelete;
            await viewModel.DeletePlant();

            // Assert
            mockPlantRepository.Verify(r => r.Delete(fishToDelete.Id), Times.Once);
            Assert.IsFalse(viewModel.Plants.Contains(fishToDelete));
        }

        [Test]
        public void RefreshPlants_CommandExecution_LoadsPlants()
        {
            // Act
            viewModel.RefreshPlants();

            // Assert
            Assert.AreEqual(3, viewModel.Plants.Count);
        }
    }
}
