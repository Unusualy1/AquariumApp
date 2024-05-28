using System.Windows;
using System.Windows.Controls;
using View.Windows;
using ViewModel;

namespace View.Tabs;

/// <summary>
/// Логика взаимодействия для FishTab.xaml
/// </summary>
public partial class FishTab : UserControl
{
    private readonly FishViewModel _viewModel;

    public FishTab()
    {
        InitializeComponent();

        _viewModel = new FishViewModel();

        DataContext = _viewModel;
    }
    public void OpenNewFishEventsWindow_Click(object sender, RoutedEventArgs e)
    {
        var windowViewModel = _viewModel.CreateFishEventsViewModel();

        FishEventsWindow fishEventsWindow = new()
        {
            DataContext = windowViewModel,
            Owner = Application.Current.MainWindow
        };

        fishEventsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        fishEventsWindow.ShowDialog();
    }
}
