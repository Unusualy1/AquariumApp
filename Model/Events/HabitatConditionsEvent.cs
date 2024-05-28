using Model.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Events;

public partial class HabitatConditionsEvent : Event
{
    public long HabitatConditionsId { get; set; }

    [ForeignKey("HabitatConditionsId")]
    public HabitatConditions? HabitatConditions { get; set;}
}
