using CommunityToolkit.Mvvm.ComponentModel;
using Model.Abstactions;

namespace Model.Events;

public partial class FishEvent : Event
{
    [ObservableProperty]
    private long fishId;
}
