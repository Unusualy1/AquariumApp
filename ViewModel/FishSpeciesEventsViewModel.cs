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

public partial class FishSpeciesEventsViewModel : BaseViewModel
{
    private readonly IFishSpeciesEventRepository _fishSpeciesEventRepository;

    private void SwapState()
    {
        IsEnabledDataGrid = !IsEnabledDataGrid;
        IsEnabledFishSpeciesEventInfo = !IsEnabledFishSpeciesEventInfo;
    }

    [ObservableProperty]
    private FishSpecies _createdWindowFishSpecies;

    [ObservableProperty]
    private ObservableCollection<FishSpeciesEvent> _fishSpeciesEvents;

    [ObservableProperty]
    private FishSpeciesEvent? currentFishSpeciesEvent = null;

    [ObservableProperty]
    private string? newFishSpeciesEventDescription = null;

    [ObservableProperty]
    private bool _isEnabledFishSpeciesEventInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    partial void OnCurrentFishSpeciesEventChanged(FishSpeciesEvent? value)
    {
        if (CurrentFishSpeciesEvent == null)
        {
            DeleteFishSpeciesEventCommand.NotifyCanExecuteChanged();
            return;
        }

        DeleteFishSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    partial void OnNewFishSpeciesEventDescriptionChanged(string? value)
    {
        CreateFishSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    private bool FishSpeciesEventNotNull()
    {
        return CurrentFishSpeciesEvent != null;
    }

    private bool NewFishSpeciesEventDescriptionNotNull()
    {
        return !String.IsNullOrEmpty(NewFishSpeciesEventDescription);
    }

    [RelayCommand]
    public void RefreshFishSpeciesEvents()
    {
        FishSpeciesEvents = new ObservableCollection<FishSpeciesEvent>(_fishSpeciesEventRepository.GetAllByFishSpeciesId(CreatedWindowFishSpecies!.Id));
    }
    [RelayCommand(CanExecute = nameof(FishSpeciesEventNotNull))]
    public async Task DeleteFishSpeciesEvent()
    {
        if (CurrentFishSpeciesEvent == null)
        {
            await _fishSpeciesEventRepository.Delete(FishSpeciesEvents.Last().Id);
            FishSpeciesEvents.Remove(FishSpeciesEvents.Last());
            return;
        }
        await _fishSpeciesEventRepository.Delete(CurrentFishSpeciesEvent.Id);
        FishSpeciesEvents.Remove(CurrentFishSpeciesEvent);

        DeleteFishSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void AddFishSpeciesEvent()
    {
        SwapState();

        CreateFishSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(NewFishSpeciesEventDescriptionNotNull))]
    public async Task CreateFishSpeciesEvent()
    {
        SwapState();
        var @event = EventFactory.CreateCustomFishSpeciesEvent(
                                           EventType.Кастомный,
                                           NewFishSpeciesEventDescription!,
                                           CreatedWindowFishSpecies.Id);
        await _fishSpeciesEventRepository.Add(@event);
        FishSpeciesEvents.Add(@event);

        NewFishSpeciesEventDescription = null;
    }

    [RelayCommand]
    public void CancelFishSpeciesEvent()
    {
        SwapState();
        NewFishSpeciesEventDescription = null;

        CreateFishSpeciesEventCommand.NotifyCanExecuteChanged();
    }

    public FishSpeciesEventsViewModel(FishSpecies fishSpecies)
    {
        _fishSpeciesEventRepository = new FishSpeciesEventRepository();
        FishSpeciesEvents = new ObservableCollection<FishSpeciesEvent>(_fishSpeciesEventRepository.GetAllByFishSpeciesId(fishSpecies.Id));
        CreatedWindowFishSpecies = fishSpecies;
    }

    public FishSpeciesEventsViewModel(FishSpecies fishSpecies, IFishSpeciesEventRepository fishSpeciesEventRepository)
    {
        _fishSpeciesEventRepository = fishSpeciesEventRepository;
        FishSpeciesEvents = new ObservableCollection<FishSpeciesEvent>(_fishSpeciesEventRepository.GetAllByFishSpeciesId(fishSpecies.Id));
        CreatedWindowFishSpecies = fishSpecies;
    }
}
