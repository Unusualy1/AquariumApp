using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
    [MaxLength(100,
        ErrorMessage = "Имя рыбы не можеть превышать {1} символов.")]
    [ObservableProperty]
    private string _name = string.Empty;

    /// <summary>
    /// Ширина рыбы (в см)
    /// </summary>
    [AllowNull]
    [Range(0, int.MaxValue,
        ErrorMessage = "Значения для длины ширины быть между {1} и {2}.")]
    [ObservableProperty]
    private int _width;

    /// <summary>
    /// Длина рыбы (в см)
    /// </summary>
    [AllowNull]
    [Range(0, int.MaxValue,
        ErrorMessage = "Значения для длины должно быть между {1} и {2}.")]
    [ObservableProperty]
    private int _length;

    /// <summary>
    /// Цвет рыбы
    /// </summary>
    [AllowNull]
    [ObservableProperty]
    [MaxLength(100,
        ErrorMessage = "Длина цвета не может превышать {1} символов." ) ]
    private string _color = string.Empty;

    /// <summary>
    /// Диета рыбы (например, планктон, растения)
    /// </summary>
    [AllowNull]
    [MaxLength(200,
        ErrorMessage = "Длина диеты не может превышать {1} символов.")]
    [ObservableProperty]
    private string _diet = string.Empty;

    /// <summary>
    /// Естественная среда обитания рыбы
    /// </summary>
    [AllowNull]
    [MaxLength(200,
        ErrorMessage = "Длина среды обитания не может превышать {1} символов.")]
    [ObservableProperty]
    private string _habitat = string.Empty;

    /// <summary>
    /// Вид рыбы
    /// </summary>
    [ObservableProperty]
    [AllowNull]
    private FishSpecies _species;
}
