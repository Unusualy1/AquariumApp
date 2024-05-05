using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model;

/// <summary>
/// Модель декорации
/// </summary>
public partial class Decoration : ObservableValidator
{
    /// <summary>
    /// Уникальный идентификатор декорации
    /// </summary>
    [ObservableProperty]
    private long _id;

    /// <summary>
    /// Название декорации
    /// </summary>
    [MaxLength(100,
        ErrorMessage = "Название декорации не может превышать {1} символов!")]
    [ObservableProperty]
    private string _name = string.Empty;

    /// <summary>
    /// Описание декорации
    /// </summary>
    [MaxLength(200,
        ErrorMessage = "Описание декорации не может превышать {1} символов!")]
    [ObservableProperty]
    private string _description = string.Empty;

    /// <summary>
    ///  Количество декораций такого типа 
    /// </summary>
    [Range(0, int.MaxValue,
        ErrorMessage = "Значения для длины должно быть между {1} и {2}.")]
    [ObservableProperty]
    private int _count;
}
