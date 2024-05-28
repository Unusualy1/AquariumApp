using Model;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.DataAccess.Repositories;
using ViewModel.UseCases;

namespace ViewModel;

public partial class FishEventsViewModel : BaseViewModel
{
    private readonly IFishEventRepository _fishEventRepository = new FishEventRepository();

    [ObservableProperty]
    private Fish createdWindowFish;

    [ObservableProperty]
    private ObservableCollection<FishEvent> _fishEvents;

    [RelayCommand]
    public void RefreshFishEvents()
    {
        FishEvents = new ObservableCollection<FishEvent>(_fishEventRepository.GetAllByFishId(createdWindowFish!.Id));
    }

    public FishEventsViewModel(Fish fish)
    {
        FishEvents = new ObservableCollection<FishEvent>(_fishEventRepository.GetAllByFishId(fish.Id));
        createdWindowFish = fish;
    }
}
