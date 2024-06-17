using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using Model.Enums;
using Model.Factories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModel.Abstrations;
using ViewModel.Enums;
using ViewModel.UseCases;

namespace ViewModel;

public partial class PlantSpeciesViewModel : BaseViewModel
{
    private readonly IPlantSpeciesRepository _plantSpeciesRepository = new PlantSpeciesRepository();
    private readonly IPlantSpeciesEventRepository _plantSpeciesEventRepository = new PlantSpeciesEventRepository();

    private State _state = State.OnDefault;

    private void SwapState(State state)
    {
        IsEnabledPlantSpeciesInfo = !IsEnabledPlantSpeciesInfo;
        IsEnabledDataGrid = !IsEnabledDataGrid;
        _state = state;
    }

    [ObservableProperty]
    private bool _isEnabledPlantSpeciesInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    [ObservableProperty]
    private ObservableCollection<PlantSpecies> _plantSpecies;

    [ObservableProperty]
    private PlantSpecies? _currentPlantSpecies = null;

    partial void OnCurrentPlantSpeciesChanged(PlantSpecies? value)
    {
        if (CurrentPlantSpecies == null)
        {
            EditPlantSpeciesCommand.NotifyCanExecuteChanged();
            ShowPlantSpeciesEventsCommand.NotifyCanExecuteChanged();
            return;
        }

        EditPlantSpeciesCommand.NotifyCanExecuteChanged();
        ShowPlantSpeciesEventsCommand.NotifyCanExecuteChanged();

        CurrentPlantSpecies.ErrorsChanged += CurrentPlantSpecies_ErrorsChanged;
        CurrentPlantSpecies.PropertyChanged += CurrentPlantSpecies_PropertyChanged;

    }

    private void CurrentPlantSpecies_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
        ApplyPlantSpeciesCommand.NotifyCanExecuteChanged();
    }

    private void CurrentPlantSpecies_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ApplyPlantSpeciesCommand.NotifyCanExecuteChanged();
    }


    public PlantSpeciesEventsViewModel CreatePlantSpeciesEventsViewModel()
    {
        return new PlantSpeciesEventsViewModel(CurrentPlantSpecies!);
    }

    private bool ValidateCurrentPlantSpecies()
    {
        if (CurrentPlantSpecies == null || CurrentPlantSpecies.Name.Length < 1) { return false; }

        return !CurrentPlantSpecies.HasErrors;
    }

    private bool PlantSpeciesNotNull()
    {
        return CurrentPlantSpecies != null;
    }

    private bool PlantSpeciesIsExist()
    {
        return PlantSpecies.Count != 0;
    }

    [RelayCommand]
    public void AddPlantSpecies()
    {
        SwapState(State.OnAdd);
        CurrentPlantSpecies = new()
        {
            Id = PlantSpecies.Count + 1,

        };
        ApplyPlantSpeciesCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(PlantSpeciesNotNull))]
    public void EditPlantSpecies()
    {
        SwapState(State.OnEdit);
    }

    [RelayCommand(CanExecute = nameof(PlantSpeciesNotNull))]
    public void ShowPlantSpeciesEvents()
    {

    }

    [RelayCommand(CanExecute = nameof(PlantSpeciesIsExist))]
    public async Task DeletePlantSpecies()
    {
        if (CurrentPlantSpecies == null)
        {
            await _plantSpeciesRepository.Delete(PlantSpecies.Last().Id);
            PlantSpecies.Remove(PlantSpecies.Last());
            return;
        }
        await _plantSpeciesRepository.Delete(CurrentPlantSpecies.Id);
        PlantSpecies.Remove(CurrentPlantSpecies);

        DeletePlantSpeciesCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(ValidateCurrentPlantSpecies))]
    public async Task ApplyPlantSpecies()
    {
        if (CurrentPlantSpecies == null) return;

        if (_state == State.OnAdd)
        {
            await _plantSpeciesRepository.Add(CurrentPlantSpecies);
            PlantSpecies.Add(CurrentPlantSpecies);

            await _plantSpeciesEventRepository.Add(EventFactory.CreateStandartPlantSpeciesEvent(EventType.Создание, CurrentPlantSpecies.Id));
        }

        if (_state == State.OnEdit)
        {
            await _plantSpeciesRepository.Update(CurrentPlantSpecies);
            await _plantSpeciesEventRepository.Add(EventFactory.CreateStandartPlantSpeciesEvent(EventType.Редактирование, CurrentPlantSpecies.Id));
        }

        SwapState(State.OnDefault);
        CurrentPlantSpecies = null;

        DeletePlantSpeciesCommand.NotifyCanExecuteChanged();
        RefreshPlantSpeciesCommand.Execute(null);
    }

    [RelayCommand]
    public async Task CancelPlantSpecies()
    {
        if (_state == State.OnAdd)
        {
            CurrentPlantSpecies = null;
            SwapState(State.OnDefault);
            return;
        }

        if (_state == State.OnEdit)
        {
            if (CurrentPlantSpecies == null) return;

            PlantSpecies? plantSpeciesCopy = await _plantSpeciesRepository.GetById(CurrentPlantSpecies.Id!);

            int index = PlantSpecies.IndexOf(CurrentPlantSpecies);

            if (index == -1 || plantSpeciesCopy == null) return;

            CurrentPlantSpecies = null;
            PlantSpecies[index] = plantSpeciesCopy;
            SwapState(State.OnDefault);
        }
    }

    [RelayCommand]
    public void RefreshPlantSpecies()
    {
        PlantSpecies = new ObservableCollection<PlantSpecies>(_plantSpeciesRepository.GetAll());
    }

    public PlantSpeciesViewModel()
    {
        PlantSpecies = new ObservableCollection<PlantSpecies>(_plantSpeciesRepository.GetAll());
    }

}
