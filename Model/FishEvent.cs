using Model.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public partial class FishEvent : EventBase
{
    public long? FishId { get; set; }

    [ForeignKey(nameof(FishId))]
    public Fish? Fish { get; set; }
}
