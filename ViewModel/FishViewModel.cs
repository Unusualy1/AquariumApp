using ViewModel.Abstrations;

namespace ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using Model.Enums;
using Model.Factories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ViewModel.Enums;
using ViewModel.UseCases;

public partial class FishViewModel : BaseViewModel
{
    private readonly IFishRepository _fishRepository;
    private readonly IFishEventRepository _fishEventRepository;
    private readonly IFishSpeciesRepository _fishSpeciesRepository;

    private State _state = State.OnDefault;

    [ObservableProperty]
    private bool _isEnabledFishInfo = false;

    [ObservableProperty]
    private bool _isEnabledDataGrid = true;

    [ObservableProperty]
    private ObservableCollection<Fish> _fishes;

    [ObservableProperty]
    private ObservableCollection<FishSpecies> _fishesSpecies;

    [ObservableProperty]
    private FishSpecies? _selectedFishSpecies = null;

    partial void OnSelectedFishSpeciesChanged(FishSpecies? value)
    {
        ApplyFishCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    private Fish? _currentFish = null;

    partial void OnCurrentFishChanged(Fish? value)
    {
        if (CurrentFish == null)
        {
            EditFishCommand.NotifyCanExecuteChanged();
            FeedFishCommand.NotifyCanExecuteChanged();
            ShowFishEventsCommand.NotifyCanExecuteChanged();
            return;
        }

        EditFishCommand.NotifyCanExecuteChanged();
        FeedFishCommand.NotifyCanExecuteChanged();
        ShowFishEventsCommand.NotifyCanExecuteChanged();

        CurrentFish.ErrorsChanged += CurrentFish_ErrorsChanged;
        CurrentFish.PropertyChanged += CurrentFish_PropertyChanged;

    }
    private void CurrentFish_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CurrentFish.Name))
        {
            ApplyFishCommand.NotifyCanExecuteChanged();
        }
    }

    private void CurrentFish_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
        ApplyFishCommand.NotifyCanExecuteChanged();
    }

    private bool ValidateCurrentFish()
    {
        if (CurrentFish == null || CurrentFish.Name.Length < 1) { return false; }

        return (!CurrentFish.HasErrors && SelectedFishSpecies != null) ;
    }

    private bool FishNotNull()
    {
        return CurrentFish != null;
    }
    
    private bool FishesIsExist()
    {
        return Fishes.Count != 0;
    }

    private void SwapState (State state)
    {
        IsEnabledFishInfo = !IsEnabledFishInfo;
        IsEnabledDataGrid = !IsEnabledDataGrid;
        _state = state;
    }

    public FishEventsViewModel CreateFishEventsViewModel()
    {
        return new FishEventsViewModel(CurrentFish!);
    }

    [RelayCommand]
    public void AddFish()
    {
        SwapState(State.OnAdd);
        CurrentFish = new()
        {
            Id = Fishes.Count + 1,
            FishSpecies = SelectedFishSpecies
            
        };
        ApplyFishCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(FishNotNull))]
    public void EditFish()
    {
        SwapState(State.OnEdit);
    }

    [RelayCommand(CanExecute = nameof(FishNotNull))]
    public async void FeedFish()
    {
        if (CurrentFish != null)
        {
            CurrentFish.FeedTime = DateTime.UtcNow;
            await _fishRepository.Update(CurrentFish);

            await _fishEventRepository.Add(EventFactory.CreateStandartFishEvent(EventType.Кормление, CurrentFish.Id));
        }
    }

    [RelayCommand(CanExecute = nameof(FishNotNull))]
    public void ShowFishEvents()
    {
        
    }

    [RelayCommand(CanExecute = nameof(FishesIsExist))]
    public async Task DeleteFish()
    {
        if (CurrentFish == null)
        {
            await _fishRepository.Delete(Fishes.Last().Id);
            Fishes.Remove(Fishes.Last());
            return;
        }
        await _fishRepository.Delete(CurrentFish.Id);
        Fishes.Remove(CurrentFish);

        DeleteFishCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(ValidateCurrentFish))]
    public async Task ApplyFish()
    {
        if (CurrentFish == null) return;

        if (_state == State.OnAdd)
        {
            CurrentFish.FishSpeciesId = SelectedFishSpecies?.Id;
            CurrentFish.FishSpecies = SelectedFishSpecies;
            await _fishRepository.Add(CurrentFish);
            Fishes.Add(CurrentFish);

            await _fishEventRepository.Add(EventFactory.CreateStandartFishEvent(EventType.Создание, CurrentFish.Id));
        }

        if (_state == State.OnEdit)
        {
            await _fishRepository.Update(CurrentFish);
            await _fishEventRepository.Add(EventFactory.CreateStandartFishEvent(EventType.Редактирование, CurrentFish.Id));
        }

        SwapState(State.OnDefault);
        CurrentFish = null;

        DeleteFishCommand.NotifyCanExecuteChanged();
        //RefreshFishesCommand.Execute(null);
    }

    [RelayCommand]
    public async Task CancelFish()
    {
        if (_state == State.OnAdd)
        {
            CurrentFish = null;
            SwapState(State.OnDefault);
            return;
        }

        if (_state == State.OnEdit)
        {
            if (CurrentFish == null) return;

            Fish? fishCopy = await _fishRepository.GetById(CurrentFish.Id);

            int index = Fishes.IndexOf(CurrentFish);

            if (index == -1 || fishCopy == null) return;

            CurrentFish = null;
            Fishes[index] = fishCopy;
            SwapState(State.OnDefault);
        }
    }

    [RelayCommand]
    public void RefreshFishes()
    {
        Fishes = new ObservableCollection<Fish>(_fishRepository.GetAll());
        FishesSpecies = new ObservableCollection<FishSpecies>(_fishSpeciesRepository.GetAll());
    }

    public FishViewModel()
    {
        _fishRepository = new FishRepository();
        _fishEventRepository = new FishEventRepository();
        _fishSpeciesRepository = new FishSpeciesRepository();
        Fishes = new ObservableCollection<Fish>(_fishRepository.GetAll());
        FishesSpecies = new ObservableCollection<FishSpecies>(_fishSpeciesRepository.GetAll());
    }

    public FishViewModel(IFishRepository fishRepository, IFishEventRepository fishEventRepository, IFishSpeciesRepository fishSpeciesRepository)
    {
        _fishRepository = fishRepository;
        _fishEventRepository = fishEventRepository;
        _fishSpeciesRepository = fishSpeciesRepository;
        Fishes = new ObservableCollection<Fish>(_fishRepository.GetAll());
        FishesSpecies = new ObservableCollection<FishSpecies>(_fishSpeciesRepository.GetAll());
    }

}
