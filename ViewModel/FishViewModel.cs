using ViewModel.Abstrations;

namespace ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ViewModel.UseCases;

public partial class FishViewModel : BaseViewModel
{
    private readonly IFishRepository _fishRepository = new FishRepository();
    private readonly IFishSpeciesRepository _fishSpeciesRepository = new FishSpeciesRepository();

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

    [ObservableProperty]
    private Fish? _currentFish = null;

    partial void OnCurrentFishChanged(Fish? value)
    {
        EditFishCommand.NotifyCanExecuteChanged();
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

    [RelayCommand]
    public void AddFish()
    {
        SwapState(State.OnAdd);
        CurrentFish = new()
        {
            Id = Fishes.Count + 1
            
        };
        ApplyFishCommand.NotifyCanExecuteChanged();
        

    }

    [RelayCommand(CanExecute = nameof(FishNotNull))]
    public void EditFish()
    {
        SwapState(State.OnEdit);
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

    [RelayCommand()]
    public async Task ApplyFish()
    {
        if (CurrentFish == null) return;

        if (_state == State.OnAdd)
        {
            await _fishRepository.Add(CurrentFish);
            Fishes.Add(CurrentFish);
        }

        if (_state == State.OnEdit)
        {
            await _fishRepository.Update(CurrentFish);
        }

        SwapState(State.OnDefault);
        CurrentFish = null;

        DeleteFishCommand.NotifyCanExecuteChanged();
    }
    [RelayCommand()]
    public void RefreshFishes()
    {
        Fishes = new ObservableCollection<Fish>(_fishRepository.GetAll());
        FishesSpecies = new ObservableCollection<FishSpecies>(_fishSpeciesRepository.GetAll());
    }

    public FishViewModel()
    {
        Fishes = new ObservableCollection<Fish>(_fishRepository.GetAll());
        FishesSpecies = new ObservableCollection<FishSpecies>(_fishSpeciesRepository.GetAll());
    }

}
