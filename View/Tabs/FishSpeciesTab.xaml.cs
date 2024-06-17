using System.Windows;
using System.Windows.Controls;
using View.Windows;
using ViewModel;
namespace View.Tabs;

/// <summary>
/// Логика взаимодействия для FishSpeciesTab.xaml
/// </summary>
public partial class FishSpeciesTab : UserControl
{
    private readonly FishSpeciesViewModel _viewModel;

    public FishSpeciesTab()
    {
        InitializeComponent();

        _viewModel = new FishSpeciesViewModel();

        DataContext = _viewModel;
    }

    public void OpenNewFishSpeciesEventsWindow_Click(object sender, RoutedEventArgs e) 
    {
        var windowViewModel = _viewModel.CreateFishSpeciesEventsViewModel();

        FishSpeciesEventsWindow fishSpeciesEventsWindow = new()
        {
            DataContext = windowViewModel,
            Owner = Application.Current.MainWindow
        };

        fishSpeciesEventsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        fishSpeciesEventsWindow.ShowDialog();
    }
}
