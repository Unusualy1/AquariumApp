using Model.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Events;

public partial class FishEvent : Event
{
    public long? FishId { get; set; }

    [ForeignKey(nameof(FishId))]
    public Fish? Fish {  get; set; } 
}
