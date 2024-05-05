using CommunityToolkit.Mvvm.ComponentModel;
using Model.Abstactions;

namespace Model.Events;

public partial class PlantEvent : Event
{
    [ObservableProperty]
    private long plantId;
}
