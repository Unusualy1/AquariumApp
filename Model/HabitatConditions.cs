using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Model;

/// <summary>
/// Модель условий обитания
/// </summary>
public partial class HabitatConditions : ObservableValidator
{
    [ObservableProperty]
    private long _id;

    /// <summary>
    /// Температура воды (в цельсиях)
    /// </summary>
    [Range(-273.15, double.MaxValue,
         ErrorMessage = "Значения для темпераутры воды быть между {1} и {2}.")]
    [ObservableProperty]
    private double _waterTemperature;

    /// <summary>
    /// Уровень кислотности воды (в pH)
    /// </summary>
    [Range(0, 14,
        ErrorMessage = "Значения для уровня кислотности быть между {1} и {2}.")]
    [ObservableProperty]
    private int _degreeOfAcidity;

    /// <summary>
    /// Уровень освещенности воды (в люмпенах)
    /// </summary>
    [Range(0.0, double.MaxValue,
        ErrorMessage = "Значения для уровня освещенности быть между {1} и {2}.")]
    [ObservableProperty]
    private double _lighting;

    /// <summary>
    /// Почва на дне аквариума
    /// </summary>
    [MaxLength(100,
        ErrorMessage = "Почва не может превышать {1} символов.")]
    [ObservableProperty]
    private string _substrate = string.Empty;

    /// <summary>
    /// Уровень кислорода в воде (в ppm)
    /// </summary>
    [Range(0.0, double.MaxValue,
        ErrorMessage = "Значения для уровня кислорода быть между {1} и {2}.")]
    [ObservableProperty]
    private double _oxygenLevel;

    /// <summary>
    /// Уровень соленности воды (в ppt)
    /// </summary>
    [Range(0.0, double.MaxValue,
        ErrorMessage = "Значения для уровня солености быть между {1} и {2}.")]
    [ObservableProperty]
    private double _salinity;
}