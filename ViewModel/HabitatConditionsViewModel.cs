using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel;

using System.ComponentModel.DataAnnotations;

public partial class HabitatConditionsViewModel : BaseViewModel
{
    private readonly IHabitatConditionRepository _habitatConditionRepository = new HabitatConditionsRepository();

    [ObservableProperty]
    private HabitatConditions? _habitatCondtitions = null;

    [ObservableProperty]
    private HabitatConditions? _editHabitatConditions;


    public HabitatConditionsViewModel()
    {
        LoadData();
    }

    private void LoadData()
    {
        HabitatCondtitions = _habitatConditionRepository.Get();
        EditHabitatConditions = _habitatConditionRepository.Get();
        CheckConditions();
    }

    private void CopyHabitatConditions()
    {
        if (EditHabitatConditions != null && HabitatCondtitions != null)
        {
            HabitatCondtitions.WaterTemperature = EditHabitatConditions.WaterTemperature;
            HabitatCondtitions.DegreeOfAcidity = EditHabitatConditions.DegreeOfAcidity;
            HabitatCondtitions.Lighting = EditHabitatConditions.Lighting;
            HabitatCondtitions.Substrate = EditHabitatConditions.Substrate;
            HabitatCondtitions.OxygenLevel = EditHabitatConditions.OxygenLevel;
            HabitatCondtitions.Salinity = EditHabitatConditions.Salinity;
        }
        CheckConditions();
    }

    [RelayCommand]
    public void SaveChanges()
    {
        if (EditHabitatConditions != null && HabitatCondtitions != null)
        {
            var validationContext = new ValidationContext(EditHabitatConditions);
            var validationResults = new List<ValidationResult>();

            if (Validator.TryValidateObject(EditHabitatConditions, validationContext, validationResults, true))
            {
                CopyHabitatConditions();
                _habitatConditionRepository.Update(HabitatCondtitions);
            }
        }
    }

    [ObservableProperty]
    private bool _isVisiblePrefactoryText = false;

    [ObservableProperty]
    private string _prefactoryText = string.Empty;

    private const double MIN_WATER_TEMPERATURE = 22;
    private const double MAX_WATER_TEMPERATURE = 26;

    private const double MIN_OXYGEN_LEVEL = 5;

    private const double MIN_PH_LEVEL = 6.5;
    private const double MAX_PH_LEVEL = 7.5;

    private const double MIN_LIGHTING_LEVEL = 2000;
    private const double MAX_LIGHTING_LEVEL = 10000;

    private void CheckConditions()
    {
        if (HabitatCondtitions == null) return;

        bool isAnyConditionNotMet = false;

        if (HabitatCondtitions.WaterTemperature < MIN_WATER_TEMPERATURE || HabitatCondtitions.WaterTemperature > MAX_WATER_TEMPERATURE)
        {
            IsVisiblePrefactoryText = true;
            PrefactoryText = "Внимание! Неблагоприятная температура аквариума!";
            isAnyConditionNotMet = true;
        }

        if (HabitatCondtitions.OxygenLevel < MIN_OXYGEN_LEVEL)
        {
            IsVisiblePrefactoryText = true;
            PrefactoryText = "Внимание! Низкий уровень кислорода в аквариуме!";
            isAnyConditionNotMet = true;
        }

        if (HabitatCondtitions.DegreeOfAcidity < MIN_PH_LEVEL || HabitatCondtitions.DegreeOfAcidity > MAX_PH_LEVEL)
        {
            IsVisiblePrefactoryText = true;
            PrefactoryText = "Внимание! Несбалансированный уровень кислотности в аквариуме!";
            isAnyConditionNotMet = true;
        }

        if (HabitatCondtitions.Lighting < MIN_LIGHTING_LEVEL || HabitatCondtitions.Lighting > MAX_LIGHTING_LEVEL)
        {
            IsVisiblePrefactoryText = true;
            PrefactoryText = "Внимание! Недостаточный или избыточный уровень освещенности в аквариуме!";
            isAnyConditionNotMet = true;
        }

        if (!isAnyConditionNotMet)
        {
            PrefactoryText = string.Empty;
        }
    }
}

 