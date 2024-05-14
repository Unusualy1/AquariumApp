using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel;

using System.Threading.Tasks;

public partial class HabitatConditionsViewModel : BaseViewModel
{
    private readonly IHabitatConditionRepository _habitatConditionRepository = new HabitatConditionsRepository();

    private State _state = State.OnDefault;

    [ObservableProperty]
    private HabitatConditions? _habitatCondtitions = null;

    public HabitatConditionsViewModel()
    {
        _habitatCondtitions =  _habitatConditionRepository.Get();
    }
}
 