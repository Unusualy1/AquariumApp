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

    [ObservableProperty]
    public bool _isVisiblePrefactoryText = false;

    [ObservableProperty]
    public string _prefactoryText = string.Empty;

    private void CheckConditions()
    {
        if (HabitatCondtitions == null) return;

        if (HabitatCondtitions.WaterTemperature < 10 || HabitatCondtitions.WaterTemperature > 30)
        {
            IsVisiblePrefactoryText = true;
            PrefactoryText = "Внимание! Неблагоприятная температура аквариума!";
            return;
        }

        PrefactoryText = string.Empty;
    }

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
}

 