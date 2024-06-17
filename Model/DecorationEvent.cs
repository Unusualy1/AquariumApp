using Model.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public partial class DecorationEvent : EventBase
{
    public long DecorationId { get; set; }

    [ForeignKey(nameof(DecorationId))]
    public Decoration? Decoration { get; set; }
}
