using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.DataAccess.Repositories;
using Model.Enums;
using Model.Factories;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;
using ViewModel.Enums;

namespace ViewModel;

public partial class PlantsViewModel : BaseViewModel
{
    private readonly IPlantRepository _plantRepository;
    private readonly IPlantEventRepository _plantEventRepository;
    private readonly IPlantSpeciesRepository _plantSpeciesRepository;

    private State _state = State.OnDefault;

    [ObservableProperty]
    private bool _isEnabledPlantInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    [ObservableProperty]
    private ObservableCollection<Plant> _plants;

    [ObservableProperty]
    private ObservableCollection<PlantSpecies> _plantSpecies;

    [ObservableProperty]
    private PlantSpecies? _selectedPlantSpecies = null;

    partial void OnSelectedPlantSpeciesChanged(PlantSpecies? value)
    {
        ApplyPlantCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    private Plant? _currentPlant = null;

    partial void OnCurrentPlantChanged(Plant? value)
    {
        if (CurrentPlant == null)
        {
            EditPlantCommand.NotifyCanExecuteChanged();
            ShowPlantEventsCommand.NotifyCanExecuteChanged();
            return;
        }

        EditPlantCommand.NotifyCanExecuteChanged();
        ShowPlantEventsCommand.NotifyCanExecuteChanged();

        CurrentPlant.ErrorsChanged += CurrentPlant_ErrorsChanged;
        CurrentPlant.PropertyChanged += CurrentPlant_PropertyChanged;

    }
    private void CurrentPlant_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CurrentPlant.Name))
        {
            ApplyPlantCommand.NotifyCanExecuteChanged();
        }
    }

    private void CurrentPlant_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
        ApplyPlantCommand.NotifyCanExecuteChanged();
    }

    private bool ValidateCurrentPlant()
    {
        if (CurrentPlant == null || CurrentPlant.Name.Length < 1) { return false; }

        return (!CurrentPlant.HasErrors && SelectedPlantSpecies != null);
    }

    private bool PlantNotNull()
    {
        return CurrentPlant != null;
    }

    private bool PlantsIsExist()
    {
        return Plants.Count != 0;
    }

    private void SwapState(State state)
    {
        IsEnabledPlantInfo = !IsEnabledPlantInfo;
        IsEnabledDataGrid = !IsEnabledDataGrid;
        _state = state;
    }

    public PlantEventsViewModel CreatePlantEventsViewModel()
    {
        return new PlantEventsViewModel(CurrentPlant!);
    }

    [RelayCommand]
    public void AddPlant()
    {
        SwapState(State.OnAdd);
        CurrentPlant = new()
        {
            Id = Plants.Count + 1,
            PlantSpecies = SelectedPlantSpecies

        };
        ApplyPlantCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(PlantNotNull))]
    public void EditPlant()
    {
        SwapState(State.OnEdit);
    }


    [RelayCommand(CanExecute = nameof(PlantNotNull))]
    public void ShowPlantEvents()
    {
    }

    [RelayCommand(CanExecute = nameof(PlantsIsExist))]
    public async Task DeletePlant()
    {
        if (CurrentPlant == null)
        {
            await _plantRepository.Delete(Plants.Last().Id);
            Plants.Remove(Plants.Last());
            return;
        }
        await _plantRepository.Delete(CurrentPlant.Id);
        Plants.Remove(CurrentPlant);

        DeletePlantCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(ValidateCurrentPlant))]
    public async Task ApplyPlant()
    {
        if (CurrentPlant == null) return;

        if (_state == State.OnAdd)
        {
            CurrentPlant.PlantSpeciesId = SelectedPlantSpecies?.Id;
            CurrentPlant.PlantSpecies = SelectedPlantSpecies;
            await _plantRepository.Add(CurrentPlant);
            Plants.Add(CurrentPlant);

            await _plantEventRepository.Add(EventFactory.CreateStandartPlantEvent(EventType.Создание, CurrentPlant.Id));
        }

        if (_state == State.OnEdit)
        {
            await _plantRepository.Update(CurrentPlant);
            await _plantEventRepository.Add(EventFactory.CreateStandartPlantEvent(EventType.Редактирование, CurrentPlant.Id));
        }

        SwapState(State.OnDefault);
        CurrentPlant = null;

        DeletePlantCommand.NotifyCanExecuteChanged();
        //RefreshPlantsCommand.Execute(null);
    }

    [RelayCommand]
    public async Task CancelPlant()
    {
        if (_state == State.OnAdd)
        {
            CurrentPlant = null;
            SwapState(State.OnDefault);
            return;
        }

        if (_state == State.OnEdit)
        {
            if (CurrentPlant == null) return;

            Plant? plantCopy = await _plantRepository.GetById(CurrentPlant.Id);

            int index = Plants.IndexOf(CurrentPlant);

            if (index == -1 || plantCopy == null) return;

            CurrentPlant = null;
            Plants[index] = plantCopy;
            SwapState(State.OnDefault);
        }
    }

    [RelayCommand]
    public void RefreshPlants()
    {
        Plants = new ObservableCollection<Plant>(_plantRepository.GetAll());
        PlantSpecies = new ObservableCollection<PlantSpecies>(_plantSpeciesRepository.GetAll());
    }

    public PlantsViewModel()
    {
        _plantRepository = new PlantRepository();
        _plantEventRepository = new PlantEventRepository();
        _plantSpeciesRepository = new PlantSpeciesRepository();
        Plants = new ObservableCollection<Plant>(_plantRepository.GetAll());
        PlantSpecies = new ObservableCollection<PlantSpecies>(_plantSpeciesRepository.GetAll());
    }

    public PlantsViewModel(IPlantRepository plantRepository,IPlantEventRepository plantEventRepository, IPlantSpeciesRepository plantSpeciesRepository)
    {
        _plantRepository = plantRepository;
        _plantEventRepository = plantEventRepository;
        _plantSpeciesRepository = plantSpeciesRepository;
        Plants = new ObservableCollection<Plant>(_plantRepository.GetAll());
        PlantSpecies = new ObservableCollection<PlantSpecies>(_plantSpeciesRepository.GetAll());
    }
}
