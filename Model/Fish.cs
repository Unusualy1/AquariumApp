using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Model;

/// <summary>
/// Модель рыбы
/// </summary>
public partial class Fish : ObservableValidator
{
    /// <summary>
    /// Уникальный идентификатор рыбы
    /// </summary>
    [ObservableProperty]
    private long _id;

    /// <summary>
    /// Имя рыбы
    /// </summary>
    [Required]
    [MaxLength(16, ErrorMessage = "Имя рыбы не можеть превышать {1} символов.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _name = string.Empty;

    /// <summary>
    /// Ширина рыбы (в см)
    /// </summary>
    [Range(0, 256, ErrorMessage = "Значения для длины ширины быть между {1} и {2}.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private int _width;

    /// <summary>
    /// Длина рыбы (в см)
    /// </summary>
    [Range(0, 256, ErrorMessage = "Значения для длины должно быть между {1} и {2}.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private int _length;

    /// <summary>
    /// Цвет рыбы
    /// </summary>
    [AllowNull]
    [NotifyDataErrorInfo]
    [MaxLength(32, ErrorMessage = "Длина цвета не может превышать {1} символов.")]
    [ObservableProperty]
    private string _color = string.Empty;

    /// <summary>
    /// Диета рыбы (например, планктон, растения)
    /// </summary>
    [AllowNull]
    [MaxLength(32, ErrorMessage = "Длина диеты не может превышать {1} символов.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _diet = string.Empty;

    /// <summary>
    /// Естественная среда обитания рыбы
    /// </summary>
    [AllowNull]
    [MaxLength(64, ErrorMessage = "Длина среды обитания не может превышать {1} символов.")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _habitat = string.Empty;

    /// <summary>
    /// Время кормежки рыбы 
    /// </summary>
    [AllowNull]
    [ObservableProperty]
    private DateTime _feedTime = DateTime.UtcNow;

    /// <summary>
    /// Вид рыбы
    /// </summary>
    public long? FishSpeciesId { get; set; }
    [ForeignKey("FishSpeciesId")]
    public FishSpecies? FishSpecies { get; set; }

    

    /// <summary>
    /// Эвенты рыбы
    /// </summary>
    [AllowNull]
    [ObservableProperty]
    private ICollection<FishEvent> _fishEvents;
}
