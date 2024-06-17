using Model.Abstactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class PlantSpeciesEvent : EventBase
{
    public long PlantSpeciesId { get; set; }

    [ForeignKey(nameof(PlantSpeciesId))]
    public PlantSpecies? PlantSpecies { get; set; }
}
