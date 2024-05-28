using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Abstactions;
using Model.DataAccess.Repositories;
using Model.Factories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel;

public partial class DecorationsViewModel : BaseViewModel
{
    private readonly IDecorationRepository _decorationRepository = new DecorationRepository();
    private readonly IDecorationEventRepository _decorationEventRepository = new DecorationEventRepository();

    private State _state = State.OnDefault;

    private void SwapState(State state)
    {
        IsEnabledDecorationInfo = !IsEnabledDecorationInfo;
        IsEnabledDataGrid = !IsEnabledDataGrid;
        _state = state;
    }

    public DecorationEventsViewModel CreateDecorationEventsViewModel()
    {
        return new DecorationEventsViewModel(CurrentDecoration!);
    }

    [ObservableProperty]
    private bool _isEnabledDecorationInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    [ObservableProperty]
    private ObservableCollection<Decoration> _decorations;

    [ObservableProperty]
    private Decoration? _currentDecoration = null;

    partial void OnCurrentDecorationChanged(Decoration? value)
    {
        if (CurrentDecoration == null)
        {
            EditDecorationCommand.NotifyCanExecuteChanged();
            ShowDecorationEventsCommand.NotifyCanExecuteChanged();
            return;
        }

        EditDecorationCommand.NotifyCanExecuteChanged();
        ShowDecorationEventsCommand.NotifyCanExecuteChanged();

        CurrentDecoration.ErrorsChanged += CurrentDecoration_ErrorsChanged;
        CurrentDecoration.PropertyChanged += CurrentDecoration_PropertyChanged;
    }

    private void CurrentDecoration_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
        ApplyDecorationCommand.NotifyCanExecuteChanged();
    }

    private void CurrentDecoration_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CurrentDecoration.Name))
        {
            ApplyDecorationCommand.NotifyCanExecuteChanged();
        }
    }

    private bool ValidateCurrentDecoration()
    {
        if (CurrentDecoration == null || CurrentDecoration.Name.Length < 1) { return false; }

        return !CurrentDecoration.HasErrors;
    }

    private bool DecorationNotNull()
    {
        return CurrentDecoration != null;
    }

    private bool DecorationIsExist()
    {
        return Decorations.Count != 0;
    }

    [RelayCommand]
    public void AddDecoration()
    {
        SwapState(State.OnAdd);
        CurrentDecoration = new()
        {
            Id = Decorations.Count + 1,

        };
        ApplyDecorationCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(DecorationNotNull))]
    public void EditDecoration()
    {
        SwapState(State.OnEdit);
    }

    [RelayCommand(CanExecute = nameof(DecorationNotNull))]
    public void ShowDecorationEvents()
    {

    }

    [RelayCommand(CanExecute = nameof(DecorationIsExist))]
    public async Task DeleteDecoration()
    {
        if (CurrentDecoration == null)
        {
            await _decorationRepository.Delete(Decorations.Last().Id);
            Decorations.Remove(Decorations.Last());
            return;
        }
        await _decorationRepository.Delete(CurrentDecoration.Id);
        Decorations.Remove(CurrentDecoration);

        DeleteDecorationCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(ValidateCurrentDecoration))]
    public async Task ApplyDecoration()
    {
        if (CurrentDecoration == null) return;

        if (_state == State.OnAdd)
        {
            await _decorationRepository.Add(CurrentDecoration);
            Decorations.Add(CurrentDecoration);

            await _decorationEventRepository.Add(EventFactory.CreateStandartDecorationEvent(EventType.Создание, CurrentDecoration.Id));
        }

        if (_state == State.OnEdit)
        {
            await _decorationRepository.Update(CurrentDecoration);
            await _decorationEventRepository.Add(EventFactory.CreateStandartDecorationEvent(EventType.Редактирование, CurrentDecoration.Id));
        }

        SwapState(State.OnDefault);
        CurrentDecoration = null;

        DeleteDecorationCommand.NotifyCanExecuteChanged();
        RefreshDecorationsCommand.Execute(null);
    }

    [RelayCommand]
    public async Task CancelDecoration()
    {
        if (_state == State.OnAdd)
        {
            CurrentDecoration = null;
            SwapState(State.OnDefault);
            return;
        }

        if (_state == State.OnEdit)
        {
            if (CurrentDecoration == null) return;

            Decoration? fishSpeciesCopy = await _decorationRepository.GetById(CurrentDecoration.Id!);

            int index = Decorations.IndexOf(CurrentDecoration);

            if (index == -1 || fishSpeciesCopy == null) return;

            CurrentDecoration = null;
            Decorations[index] = fishSpeciesCopy;
            SwapState(State.OnDefault);
        }
    }

    [RelayCommand]
    public void RefreshDecorations()
    {
        Decorations = new ObservableCollection<Decoration>(_decorationRepository.GetAll());
    }

    public DecorationsViewModel()
    {
        Decorations = new ObservableCollection<Decoration>(_decorationRepository.GetAll());
    }


}
