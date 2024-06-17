using CommunityToolkit.Mvvm.ComponentModel;
using Model.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public partial class PlantEvent : EventBase
{
    public long PlantId { get; set; }

    [ForeignKey(nameof(PlantId))]
    public Plant? Plant { get; set; }
}
