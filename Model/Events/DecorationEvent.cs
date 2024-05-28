using Model.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Events;

public partial class DecorationEvent : Event
{
    public long DecorationId { get; set; }

    [ForeignKey(nameof(DecorationId))]
    public Decoration? Decoration { get; set; }
}
