using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using Model.Enums;
using Model.Factories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel;

public partial class DecorationEventsViewModel : BaseViewModel
{
    private readonly IDecorationEventRepository _decorationEventRepository;

    private void SwapState()
    {
        IsEnabledDataGrid = !IsEnabledDataGrid;
        IsEnabledDecorationEventInfo = !IsEnabledDecorationEventInfo;
    }

    [ObservableProperty]
    private Decoration _createdWindowDecoration;

    [ObservableProperty]
    private ObservableCollection<DecorationEvent> _decorationEvents;

    [ObservableProperty]
    private DecorationEvent? currentDecorationEvent = null;

    [ObservableProperty]
    private string? newDecorationEventDescription = null;

    [ObservableProperty]
    private bool _isEnabledDecorationEventInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    partial void OnCurrentDecorationEventChanged(DecorationEvent? value)
    {
        if (CurrentDecorationEvent == null)
        {
            DeleteDecorationEventCommand.NotifyCanExecuteChanged();
            return;
        }

        DeleteDecorationEventCommand.NotifyCanExecuteChanged();
    }

    partial void OnNewDecorationEventDescriptionChanged(string? value)
    {
        CreateDecorationEventCommand.NotifyCanExecuteChanged();
    }

    private bool DecorationEventNotNull()
    {
        return CurrentDecorationEvent != null;
    }

    private bool NewDecorationEventDescriptionNotNull()
    {
        return !String.IsNullOrEmpty(NewDecorationEventDescription);
    }

    [RelayCommand]
    public void RefreshDecorationEvents()
    {
        DecorationEvents = new ObservableCollection<DecorationEvent>(_decorationEventRepository.GetAllByDecorationId(CreatedWindowDecoration!.Id));
    }
    [RelayCommand(CanExecute = nameof(DecorationEventNotNull))]
    public async Task DeleteDecorationEvent()
    {
        if (CurrentDecorationEvent == null)
        {
            await _decorationEventRepository.Delete(DecorationEvents.Last().Id);
            DecorationEvents.Remove(DecorationEvents.Last());
            return;
        }
        await _decorationEventRepository.Delete(CurrentDecorationEvent.Id);
        DecorationEvents.Remove(CurrentDecorationEvent);

        DeleteDecorationEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void AddDecorationEvent()
    {
        SwapState();

        CreateDecorationEventCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(NewDecorationEventDescriptionNotNull))]
    public async Task CreateDecorationEvent()
    {
        SwapState();
        var @event = EventFactory.CreateCustomDecorationEvent(
                                           EventType.Кастомный,
                                           NewDecorationEventDescription!,
                                           CreatedWindowDecoration.Id);
        await _decorationEventRepository.Add(@event);
        DecorationEvents.Add(@event);

        NewDecorationEventDescription = null;
    }

    [RelayCommand]
    public void CancelDecorationEvent()
    {
        SwapState();
        NewDecorationEventDescription = null;

        CreateDecorationEventCommand.NotifyCanExecuteChanged();
    }

    public DecorationEventsViewModel(Decoration decoration)
    {
        _decorationEventRepository = new DecorationEventRepository();
        DecorationEvents = new ObservableCollection<DecorationEvent>(_decorationEventRepository.GetAllByDecorationId(decoration.Id));
        CreatedWindowDecoration = decoration;
    }

    public DecorationEventsViewModel(Decoration decoration, IDecorationEventRepository decorationEventRepository)
    {
        _decorationEventRepository = decorationEventRepository;
        DecorationEvents = new ObservableCollection<DecorationEvent>(_decorationEventRepository.GetAllByDecorationId(decoration.Id));
        CreatedWindowDecoration = decoration;
    }
}
