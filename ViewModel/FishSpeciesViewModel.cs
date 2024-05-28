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

public partial class FishSpeciesViewModel : BaseViewModel
{
    private readonly IFishSpeciesRepository _fishSpeciesRepository = new FishSpeciesRepository();
    private readonly IFishSpeciesEventRepository _fishSpeciesEventRepository = new FishSpeciesEventRepository();

    private State _state = State.OnDefault;

    private void SwapState(State state)
    {
        IsEnabledFishSpeciesInfo = !IsEnabledFishSpeciesInfo;
        IsEnabledDataGrid = !IsEnabledDataGrid;
        _state = state;
    }

    [ObservableProperty]
    private bool _isEnabledFishSpeciesInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    [ObservableProperty]
    private ObservableCollection<FishSpecies> _fishSpecies;

    [ObservableProperty]
    private FishSpecies? _currentFishSpecies = null;

    partial void OnCurrentFishSpeciesChanged(FishSpecies? value)
    {
        if (CurrentFishSpecies == null)
        {
            EditFishSpeciesCommand.NotifyCanExecuteChanged();
            ShowFishSpeciesEventsCommand.NotifyCanExecuteChanged();
            return;
        }

        EditFishSpeciesCommand.NotifyCanExecuteChanged();
        ShowFishSpeciesEventsCommand.NotifyCanExecuteChanged();

        CurrentFishSpecies.ErrorsChanged += CurrentFishSpecies_ErrorsChanged;
        CurrentFishSpecies.PropertyChanged += CurrentFishSpecies_PropertyChanged;

    }

    private void CurrentFishSpecies_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
        ApplyFishSpeciesCommand.NotifyCanExecuteChanged();
    }

    private void CurrentFishSpecies_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CurrentFishSpecies.Name))
        {
            ApplyFishSpeciesCommand.NotifyCanExecuteChanged();
        }
    }


    public FishSpeciesEventsViewModel CreateFishSpeciesEventsViewModel()
    {
        return new FishSpeciesEventsViewModel(CurrentFishSpecies!);
    }

    private bool ValidateCurrentFishSpecies()
    {
        if (CurrentFishSpecies == null || CurrentFishSpecies.Name.Length < 1) { return false; }

        return !CurrentFishSpecies.HasErrors;
    }

    private bool FishSpeciesNotNull()
    {
        return CurrentFishSpecies != null;
    }
    
    private bool FishSpeciesIsExist()
    {
        return FishSpecies.Count != 0;
    }

    [RelayCommand]
    public void AddFishSpecies()
    {
        SwapState(State.OnAdd);
        CurrentFishSpecies = new()
        {
            Id = FishSpecies.Count + 1,

        };
        ApplyFishSpeciesCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(FishSpeciesNotNull))]
    public void EditFishSpecies()
    {
        SwapState(State.OnEdit);
    }

    [RelayCommand(CanExecute = nameof(FishSpeciesNotNull))]
    public void ShowFishSpeciesEvents()
    {

    }

    [RelayCommand(CanExecute = nameof(FishSpeciesIsExist))]
    public async Task DeleteFishSpecies()
    {
        if (CurrentFishSpecies == null)
        {
            await _fishSpeciesRepository.Delete(FishSpecies.Last().Id);
            FishSpecies.Remove(FishSpecies.Last());
            return;
        }
        await _fishSpeciesRepository.Delete(CurrentFishSpecies.Id);
        FishSpecies.Remove(CurrentFishSpecies);

        DeleteFishSpeciesCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(ValidateCurrentFishSpecies))]
    public async Task ApplyFishSpecies()
    {
        if (CurrentFishSpecies == null) return;

        if (_state == State.OnAdd)
        {
            await _fishSpeciesRepository.Add(CurrentFishSpecies);
            FishSpecies.Add(CurrentFishSpecies);

            await _fishSpeciesEventRepository.Add(EventFactory.CreateStandartFishSpeciesEvent(EventType.Создание, CurrentFishSpecies.Id));
        }

        if (_state == State.OnEdit)
        {
            await _fishSpeciesRepository.Update(CurrentFishSpecies);
            await _fishSpeciesEventRepository.Add(EventFactory.CreateStandartFishSpeciesEvent(EventType.Редактирование, CurrentFishSpecies.Id));
        }

        SwapState(State.OnDefault);
        CurrentFishSpecies = null;

        DeleteFishSpeciesCommand.NotifyCanExecuteChanged();
        RefreshFishSpeciesCommand.Execute(null);
    }

    [RelayCommand]
    public async Task CancelFishSpecies()
    {
        if (_state == State.OnAdd)
        {
            CurrentFishSpecies = null;
            SwapState(State.OnDefault);
            return;
        }

        if (_state == State.OnEdit)
        {
            if (CurrentFishSpecies == null) return;

            FishSpecies? fishSpeciesCopy = await _fishSpeciesRepository.GetById(CurrentFishSpecies.Id!);

            int index = FishSpecies.IndexOf(CurrentFishSpecies);

            if (index == -1 || fishSpeciesCopy == null) return;

            CurrentFishSpecies = null;
            FishSpecies[index] = fishSpeciesCopy;
            SwapState(State.OnDefault);
        }
    }

    [RelayCommand]
    public void RefreshFishSpecies()
    {
        FishSpecies = new ObservableCollection<FishSpecies>(_fishSpeciesRepository.GetAll());
    }

    public FishSpeciesViewModel()
    {
        FishSpecies = new ObservableCollection<FishSpecies>(_fishSpeciesRepository.GetAll());
    }

}
