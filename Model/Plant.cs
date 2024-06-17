using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Model;

/// <summary>
/// Модель растения
/// </summary>
public partial class Plant : ObservableValidator
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [ObservableProperty]
    private long _id;

    /// <summary>
    /// Название растения
    /// </summary>
    [Required]
    [MaxLength(16, ErrorMessage = "Названия растения не может превышать {1} символов.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _name = String.Empty;

    /// <summary>
    /// Вид растения
    /// </summary>
    public long? PlantSpeciesId { get; set; }
    [ForeignKey("PlantSpeciesId")]
    public PlantSpecies? PlantSpecies { get; set; }

    /// <summary>
    /// Количество растений такого вида в аквариуме
    /// </summary>
    [Range(0, int.MaxValue,
        ErrorMessage = "Значения для количества растений должно быть между {1} и {2}")]
    [ObservableProperty]
    private int _count;

    /// <summary>
    /// Эвенты растения
    /// </summary>
    [AllowNull]
    [ObservableProperty]
    private ICollection<PlantEvent> _plantEvents;
}
