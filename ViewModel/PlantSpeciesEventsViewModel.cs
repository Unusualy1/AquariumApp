using Model;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.DataAccess.Repositories;
using ViewModel.UseCases;
using Model.Enums;
using Model.Factories;

namespace ViewModel;

public partial class PlantSpeciesEventsViewModel : BaseViewModel
{
    private readonly IPlantSpeciesEventRepository _plantSpeciesEventRepository;

    private void SwapState()
    {
        IsEnabledDataGrid = !IsEnabledDataGrid;
        IsEnabledPlantSpeciesEventInfo = !IsEnabledPlantSpeciesEventInfo;
    }

    [ObservableProperty]
    private PlantSpecies _createdWindowPlantSpecies;

    [ObservableProperty]
    private ObservableCollection<PlantSpeciesEvent> _plantSpeciesEvents;

    [ObservableProperty]
    private PlantSpeciesEvent? currentPlantSpeciesEvent = null;

    [ObservableProperty]
    private string? newPlantSpeciesEventDescription = null;

    [ObservableProperty]
    private bool _isEnabledPlantSpeciesEventInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    partial void OnCurrentPlantSpeciesEventChanged(PlantSpeciesEvent? value)
    {
        if (CurrentPlantSpeciesEvent == null)
        {
            DeletePlantSpeciesEventCommand.NotifyCanExecuteChanged();
            return;
        }

        DeletePlantSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    partial void OnNewPlantSpeciesEventDescriptionChanged(string? value)
    {
        CreatePlantSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    private bool PlantSpeciesEventNotNull()
    {
        return CurrentPlantSpeciesEvent != null;
    }

    private bool NewPlantSpeciesEventDescriptionNotNull()
    {
        return !String.IsNullOrEmpty(NewPlantSpeciesEventDescription);
    }

    [RelayCommand]
    public void RefreshPlantSpeciesEvents()
    {
        PlantSpeciesEvents = new ObservableCollection<PlantSpeciesEvent>(_plantSpeciesEventRepository.GetAllByPlantSpeciesId(CreatedWindowPlantSpecies!.Id));
    }
    [RelayCommand(CanExecute = nameof(PlantSpeciesEventNotNull))]
    public async Task DeletePlantSpeciesEvent()
    {
        if (CurrentPlantSpeciesEvent == null)
        {
            await _plantSpeciesEventRepository.Delete(PlantSpeciesEvents.Last().Id);
            PlantSpeciesEvents.Remove(PlantSpeciesEvents.Last());
            return;
        }
        await _plantSpeciesEventRepository.Delete(CurrentPlantSpeciesEvent.Id);
        PlantSpeciesEvents.Remove(CurrentPlantSpeciesEvent);

        DeletePlantSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void AddPlantSpeciesEvent()
    {
        SwapState();

        CreatePlantSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(NewPlantSpeciesEventDescriptionNotNull))]
    public async Task CreatePlantSpeciesEvent()
    {
        SwapState();
        var @event = EventFactory.CreateCustomPlantSpeciesEvent(
                                           EventType.Кастомный,
                                           NewPlantSpeciesEventDescription!,
                                           CreatedWindowPlantSpecies.Id);
        await _plantSpeciesEventRepository.Add(@event);
        PlantSpeciesEvents.Add(@event);

        NewPlantSpeciesEventDescription = null;
    }

    [RelayCommand]
    public void CancelPlantSpeciesEvent()
    {
        SwapState();
        NewPlantSpeciesEventDescription = null;

        CreatePlantSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    public PlantSpeciesEventsViewModel(PlantSpecies plantSpecies)
    {
        _plantSpeciesEventRepository = new PlantSpeciesEventRepository();
        PlantSpeciesEvents = new ObservableCollection<PlantSpeciesEvent>(_plantSpeciesEventRepository.GetAllByPlantSpeciesId(plantSpecies.Id));
        CreatedWindowPlantSpecies = plantSpecies;
    }

    public PlantSpeciesEventsViewModel(PlantSpecies plantSpecies,IPlantSpeciesEventRepository plantSpeciesEventRepository)
    {
        _plantSpeciesEventRepository = plantSpeciesEventRepository;
        PlantSpeciesEvents = new ObservableCollection<PlantSpeciesEvent>(_plantSpeciesEventRepository.GetAllByPlantSpeciesId(plantSpecies.Id));
        CreatedWindowPlantSpecies = plantSpecies;
    }
}
