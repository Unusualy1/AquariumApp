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
    public class FishViewModelTests
    {
        private FishViewModel viewModel;
        private Mock<IFishRepository> mockFishRepository;
        private Mock<IFishEventRepository> mockFishEventRepository;
        private Mock<IFishSpeciesRepository> mockFishSpeciesRepository;

        [SetUp]
        public void Setup()
        {
            mockFishRepository = new Mock<IFishRepository>();
            mockFishEventRepository = new Mock<IFishEventRepository>();
            mockFishSpeciesRepository = new Mock<IFishSpeciesRepository>();

            var fishes = new List<Fish>
            {
                new Fish { Id = 1, Name = "Fish 1", FishSpeciesId = 1 },
                new Fish { Id = 2, Name = "Fish 2", FishSpeciesId = 2 },
                new Fish { Id = 3, Name = "Fish 3", FishSpeciesId = 1 }
            };
            var fishSpecies = new List<FishSpecies>
            {
                new FishSpecies { Id = 1, Name = "Species 1" },
                new FishSpecies { Id = 2, Name = "Species 2" }
            };

            mockFishRepository.Setup(r => r.GetAll()).Returns(fishes);
            mockFishSpeciesRepository.Setup(r => r.GetAll()).Returns(fishSpecies);

            viewModel = new FishViewModel(mockFishRepository.Object, mockFishEventRepository.Object, mockFishSpeciesRepository.Object);
            viewModel.RefreshFishes();
        }

        [Test]
        public void EditFish_CommandExecution_SetsState()
        {
            // Arrange
            var initialFish = viewModel.Fishes.FirstOrDefault();

            // Act
            viewModel.CurrentFish = initialFish;
            viewModel.EditFish();

            // Assert
            Assert.IsFalse(viewModel.IsEnabledDataGrid); 
            Assert.IsTrue(viewModel.IsEnabledFishInfo); 
        }

        [Test]
        public async Task FeedFish_CommandExecution_UpdatesFeedTime()
        {
            // Arrange
            var initialFish = viewModel.Fishes.FirstOrDefault();

            // Act
            viewModel.CurrentFish = initialFish;
            viewModel.FeedFish();

            // Assert
            mockFishRepository.Verify(r => r.Update(viewModel.CurrentFish), Times.Once); 
            mockFishEventRepository.Verify(r => r.Add(It.IsAny<FishEvent>()), Times.Once); 
        }

        [Test]
        public async Task ApplyFish_AddCommand_AddsFishAndEvents()
        {
            // Arrange
            var newFish = new Fish
            {
                Name = "New Fish",
                FishSpecies = viewModel.FishesSpecies.FirstOrDefault()
            };

            // Act
            viewModel.AddFish();
            viewModel.CurrentFish = newFish;
            await viewModel.ApplyFish();

            // Assert 
            Assert.IsTrue(viewModel.Fishes.Contains(newFish)); 
            Assert.IsNull(viewModel.CurrentFish); 
        }

        [Test]
        public async Task CancelFish_AddCommand_CancelsAddition()
        {
            // Arrange
            viewModel.AddFish(); // Enter add mode

            // Act
            await viewModel.CancelFish();

            // Assert
            Assert.IsTrue(viewModel.IsEnabledDataGrid); 
            Assert.IsFalse(viewModel.IsEnabledFishInfo); 
            Assert.IsNull(viewModel.CurrentFish); 
        }

        [Test]
        public async Task CancelFish_EditCommand_RestoresOriginalFish()
        {
            // Arrange
            var initialFish = viewModel.Fishes.FirstOrDefault();
            viewModel.EditFish(); 

            var editedFish = new Fish
            {
                Id = initialFish.Id,
                Name = "Edited Fish",
                FishSpecies = initialFish.FishSpecies
            };

            // Act
            viewModel.CurrentFish = editedFish;
            await viewModel.CancelFish();

            // Assert
            Assert.AreEqual(initialFish, viewModel.Fishes.FirstOrDefault(f => f.Id == initialFish.Id)); 
        }

        [Test]
        public async Task DeleteFish_CommandExecution_DeletesFish()
        {
            // Arrange
            var fishToDelete = viewModel.Fishes.FirstOrDefault();

            // Act
            viewModel.CurrentFish = fishToDelete;
            await viewModel.DeleteFish();

            // Assert
            mockFishRepository.Verify(r => r.Delete(fishToDelete.Id), Times.Once); 
            Assert.IsFalse(viewModel.Fishes.Contains(fishToDelete)); 
        }

        [Test]
        public void RefreshFishes_CommandExecution_LoadsFishes()
        {
            // Act
            viewModel.RefreshFishes();

            // Assert
            Assert.AreEqual(3, viewModel.Fishes.Count); 
        }
    }
}
