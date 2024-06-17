using Model.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public partial class HabitatConditionsEvent : EventBase
{
    public long HabitatConditionsId { get; set; }

    [ForeignKey("HabitatConditionsId")]
    public HabitatConditions? HabitatConditions { get; set; }
}
