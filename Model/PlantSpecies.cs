using CommunityToolkit.Mvvm.ComponentModel;
using Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model;

/// <summary>
/// Модель вида растений
/// </summary>
public partial class PlantSpecies : ObservableValidator
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [ObservableProperty]
    private long _id;

    /// <summary>
    /// Название вида растения
    /// </summary>
    [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения.")]
    [MaxLength(16, ErrorMessage = "Название вида растения не может превышать {1} символов.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _name = string.Empty;

    /// <summary>
    /// Описание вида растения
    /// </summary>
    [AllowNull]
    [MaxLength(256, ErrorMessage = "Описание вида растения не может превышать {1} символов.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _description = string.Empty;

    /// <summary>
    /// Тип вида растения
    /// </summary>
    [Required(ErrorMessage = "Поле 'Тип' обязательно для заполнения.")]
    [MaxLength(64, ErrorMessage = "Тип вида растения не может превышать {1} символов.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _type = string.Empty;

    /// <summary>
    /// Место происхождения вида растения
    /// </summary>
    [AllowNull]
    [MaxLength(32, ErrorMessage = "Место происхождения вида растения не может превышать {1} символов.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _origin = string.Empty;

    /// <summary>
    /// Эвенты вида растения
    /// </summary>
    [AllowNull]
    [ObservableProperty]
    private ICollection<PlantSpeciesEvent> _plantSpeciesEvents;
}