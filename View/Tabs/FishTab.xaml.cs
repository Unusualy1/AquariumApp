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

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
