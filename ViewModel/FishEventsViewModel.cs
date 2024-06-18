using Model;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.DataAccess.Repositories;
using ViewModel.UseCases;
using Model.Enums;
using Model.Factories;
using Model.Abstactions;

namespace ViewModel;

public partial class FishEventsViewModel : BaseViewModel
{
    private readonly IFishEventRepository _fishEventRepository;

    private void SwapState()
    {
        IsEnabledDataGrid = !IsEnabledDataGrid;
        IsEnabledFishEventInfo = !IsEnabledFishEventInfo;
    }

    [ObservableProperty]
    private Fish _createdWindowFish;

    [ObservableProperty]
    private ObservableCollection<FishEvent> _fishEvents;

    [ObservableProperty]
    private FishEvent? currentFishEvent = null;

    [ObservableProperty]
    private string? newFishEventDescription = null;

    [ObservableProperty]
    private bool _isEnabledFishEventInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    partial void OnCurrentFishEventChanged(FishEvent? value)
    {
        if (CurrentFishEvent == null)
        {
            DeleteFishEventCommand.NotifyCanExecuteChanged();
            return;
        }

        DeleteFishEventCommand.NotifyCanExecuteChanged();
    }

    partial void OnNewFishEventDescriptionChanged(string? value)
    {
        CreateFishEventCommand.NotifyCanExecuteChanged();
    }

    private bool FishEventNotNull()
    {
        return CurrentFishEvent != null;
    }

    private bool NewFishEventDescriptionNotNull()
    {
        return !String.IsNullOrEmpty(NewFishEventDescription);
    }

    [RelayCommand]
    public void RefreshFishEvents()
    {
        FishEvents = new ObservableCollection<FishEvent>(_fishEventRepository.GetAllByFishId(CreatedWindowFish!.Id));
    }
    [RelayCommand(CanExecute = nameof(FishEventNotNull))]
    public async Task DeleteFishEvent()
    {
        if (CurrentFishEvent == null)
        {
            await _fishEventRepository.Delete(FishEvents.Last().Id);
            FishEvents.Remove(FishEvents.Last());
            return;
        }
        await _fishEventRepository.Delete(CurrentFishEvent.Id);
        FishEvents.Remove(CurrentFishEvent);

        DeleteFishEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void AddFishEvent()
    {
        SwapState();

        CreateFishEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(NewFishEventDescriptionNotNull))]
    public async Task CreateFishEvent()
    {
        SwapState();
        var @event = EventFactory.CreateCustomFishEvent(
                                           EventType.Кастомный,
                                           NewFishEventDescription!,
                                           CreatedWindowFish.Id);
        await _fishEventRepository.Add(@event);
        FishEvents.Add(@event);

        NewFishEventDescription = null;
    }

    [RelayCommand]
    public void CancelFishEvent()
    {
        SwapState();
        NewFishEventDescription = null;

        CreateFishEventCommand.NotifyCanExecuteChanged();
    }

    public FishEventsViewModel(Fish fish)
    {
        _fishEventRepository =  new FishEventRepository();
        FishEvents = new ObservableCollection<FishEvent>(_fishEventRepository.GetAllByFishId(fish.Id));
        CreatedWindowFish = fish;
    }

    public FishEventsViewModel(Fish fish, IFishEventRepository fishEventRepository)
    {
        _fishEventRepository = fishEventRepository;
        FishEvents = new ObservableCollection<FishEvent>(_fishEventRepository.GetAllByFishId(fish.Id));
        CreatedWindowFish = fish;
    }
}
