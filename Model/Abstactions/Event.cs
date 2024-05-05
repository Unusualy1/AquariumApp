using CommunityToolkit.Mvvm.ComponentModel;

namespace Model.Abstactions;

/// <summary>
/// Модель события
/// </summary>
public partial class Event : ObservableValidator
{
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    [ObservableProperty]
    private long _id;

    /// <summary>
    /// Тип события
    /// </summary>
    [ObservableProperty]
    private string _type = string.Empty;

    /// <summary>
    /// Описания события
    /// </summary>
    [ObservableProperty]
    private string _description = string.Empty;
}