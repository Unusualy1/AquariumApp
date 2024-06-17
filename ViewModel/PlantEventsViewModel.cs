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

public partial class PlantEventsViewModel : BaseViewModel
{
    private readonly IPlantEventRepository _plantEventRepository = new PlantEventRepository();

    private void SwapState()
    {
        IsEnabledDataGrid = !IsEnabledDataGrid;
        IsEnabledPlantEventInfo = !IsEnabledPlantEventInfo;
    }

    [ObservableProperty]
    private Plant _createdWindowPlant;

    [ObservableProperty]
    private ObservableCollection<PlantEvent> _plantEvents;

    [ObservableProperty]
    private PlantEvent? currentPlantEvent = null;

    [ObservableProperty]
    private string? newPlantEventDescription = null;

    [ObservableProperty]
    private bool _isEnabledPlantEventInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    partial void OnCurrentPlantEventChanged(PlantEvent? value)
    {
        if (CurrentPlantEvent == null)
        {
            DeletePlantEventCommand.NotifyCanExecuteChanged();
            return;
        }

        DeletePlantEventCommand.NotifyCanExecuteChanged();
    }

    partial void OnNewPlantEventDescriptionChanged(string? value)
    {
        CreatePlantEventCommand.NotifyCanExecuteChanged();
    }

    private bool PlantEventNotNull()
    {
        return CurrentPlantEvent != null;
    }

    private bool NewPlantEventDescriptionNotNull()
    {
        return !String.IsNullOrEmpty(NewPlantEventDescription);
    }

    [RelayCommand]
    public void RefreshPlantEvents()
    {
        PlantEvents = new ObservableCollection<PlantEvent>(_plantEventRepository.GetAllByPlantId(CreatedWindowPlant!.Id));
    }
    [RelayCommand(CanExecute = nameof(PlantEventNotNull))]
    public async Task DeletePlantEvent()
    {
        if (CurrentPlantEvent == null)
        {
            await _plantEventRepository.Delete(PlantEvents.Last().Id);
            PlantEvents.Remove(PlantEvents.Last());
            return;
        }
        await _plantEventRepository.Delete(CurrentPlantEvent.Id);
        PlantEvents.Remove(CurrentPlantEvent);

        DeletePlantEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void AddPlantEvent()
    {
        SwapState();

        CreatePlantEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(NewPlantEventDescriptionNotNull))]
    public async Task CreatePlantEvent()
    {
        SwapState();
        var @event = EventFactory.CreateCustomPlantEvent(
                                           EventType.Кастомный,
                                           NewPlantEventDescription!,
                                           CreatedWindowPlant.Id);
        await _plantEventRepository.Add(@event);
        PlantEvents.Add(@event);

        NewPlantEventDescription = null;
    }

    [RelayCommand]
    public void CancelPlantEvent()
    {
        SwapState();
        NewPlantEventDescription = null;

        CreatePlantEventCommand.NotifyCanExecuteChanged();
    }

    public PlantEventsViewModel(Plant plant)
    {
        PlantEvents = new ObservableCollection<PlantEvent>(_plantEventRepository.GetAllByPlantId(plant.Id));
        CreatedWindowPlant = plant;
    }
}
