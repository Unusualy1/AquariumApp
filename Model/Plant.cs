using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    /// Название вида растения
    /// </summary>
    [AllowNull]
    [MaxLength(100,
        ErrorMessage = "Название вида растения не может превышать {1} символов.")]
    [ObservableProperty]
    private string _name = string.Empty;

    /// <summary>
    /// Описание растения
    /// </summary>
    [AllowNull]
    [MaxLength(200,
        ErrorMessage = "Описание вида растения не может превышать {1} символов.")]
    [ObservableProperty]
    private string _description = string.Empty;

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
