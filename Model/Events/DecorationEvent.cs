using CommunityToolkit.Mvvm.ComponentModel;
using Model.Abstactions;

namespace Model.Events;

public partial class DecorationEvent : Event
{
    [ObservableProperty]
    private long decorationId;
}
