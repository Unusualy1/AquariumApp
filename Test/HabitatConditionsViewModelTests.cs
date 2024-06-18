using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using Moq;
using NUnit.Framework;
using ViewModel;
using ViewModel.UseCases;

namespace Test
{
    [TestFixture]
    public class HabitatConditionsViewModelTests
    {
        private HabitatConditionsViewModel viewModel;
        private Mock<IHabitatConditionRepository> mockHabitatConditionRepository;

        [SetUp]
        public void Setup()
        {
            mockHabitatConditionRepository = new Mock<IHabitatConditionRepository>();

            var initialConditions = new HabitatConditions
            {
                WaterTemperature = 24,
                DegreeOfAcidity = 7,
                Lighting = 6000,
                Substrate = "Sand",
                OxygenLevel = 6,
                Salinity = 1.025
            };

            mockHabitatConditionRepository.Setup(r => r.Get())
                                          .Returns(initialConditions);

            viewModel = new HabitatConditionsViewModel(mockHabitatConditionRepository.Object);


            viewModel.LoadData();
        }

        [Test]
        public void CheckConditions_WaterTemperatureOutOfRange_SetsVisibleText()
        {
            // Arrange
            viewModel.HabitatCondtitions.WaterTemperature = 20; // Below MIN_WATER_TEMPERATURE

            // Act
            viewModel.CheckConditions();

            // Assert
            Assert.IsTrue(viewModel.IsVisiblePrefactoryText);
            Assert.AreEqual("Внимание! Неблагоприятная температура аквариума!", viewModel.PrefactoryText);
        }

        [Test]
        public void CheckConditions_OxygenLevelBelowMinimum_SetsVisibleText()
        {
            // Arrange
            viewModel.HabitatCondtitions.OxygenLevel = 3; // Below MIN_OXYGEN_LEVEL

            // Act
            viewModel.CheckConditions();

            // Assert
            Assert.IsTrue(viewModel.IsVisiblePrefactoryText);
            Assert.AreEqual("Внимание! Низкий уровень кислорода в аквариуме!", viewModel.PrefactoryText);
        }

        [Test]
        public void CheckConditions_LightingOutOfRange_SetsVisibleText()
        {
            // Arrange
            viewModel.HabitatCondtitions.Lighting = 15000; // Above MAX_LIGHTING_LEVEL

            // Act
            viewModel.CheckConditions();

            // Assert
            Assert.IsTrue(viewModel.IsVisiblePrefactoryText);
            Assert.AreEqual("Внимание! Недостаточный или избыточный уровень освещенности в аквариуме!", viewModel.PrefactoryText);
        }

        [Test]
        public void SaveChanges_ValidConditions_CopiesAndUpdates()
        {
            // Arrange
            viewModel.EditHabitatConditions = new HabitatConditions
            {
                WaterTemperature = 25,
                DegreeOfAcidity = 7,
                Lighting = 8000,
                Substrate = "Gravel",
                OxygenLevel = 6,
                Salinity = 1.025
            };

            // Act
            viewModel.SaveChanges();

            // Assert
            mockHabitatConditionRepository.Verify(r => r.Update(viewModel.HabitatCondtitions), Times.Once);
        }
    }
}
