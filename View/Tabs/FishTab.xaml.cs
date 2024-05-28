using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
