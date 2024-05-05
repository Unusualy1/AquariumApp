using CommunityToolkit.Mvvm.ComponentModel;
using Model.Abstactions;

namespace Model.Events;

public partial class FishSpeciesEvent : Event
{
    [ObservableProperty]
    private long fishSpeciesId;
}
