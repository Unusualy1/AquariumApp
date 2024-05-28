using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model;

/// <summary>
/// Модель вида рыбы
/// </summary>
public partial class FishSpecies : ObservableValidator
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [ObservableProperty]
    private long _id;

    /// <summary>
    /// Название вида рыбы
    /// </summary>
    [Required]
    [MaxLength(16, ErrorMessage = "Название вида рыбы не может превышать {1} символов")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _name = string.Empty;

    /// <summary>
    /// Описание вида рыбы
    /// </summary>
    [AllowNull]
    [MaxLength(256, ErrorMessage = "Описание вида рыбы не может превышать {1} символов")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _description = string.Empty;

    /// <summary>
    /// Тип вида рыбы (например, хищник, травоядное)
    /// </summary>
    [AllowNull]
    [MaxLength(16, ErrorMessage = "Название типа рыбы не может превышать {1} символов")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _type = string.Empty;

    /// <summary>
    /// Место происхождения вида рыбы
    /// </summary>
    [AllowNull]
    [MaxLength(32, ErrorMessage = "Место происхождения вида рыбы не может превышать {1} символов")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _origin = string.Empty;

    /// <summary>
    /// Эвенты вида рыбы
    /// </summary>
    [AllowNull]
    [ObservableProperty]
    private ICollection<FishSpeciesEvent> _fishSpeciesEvents;
}
