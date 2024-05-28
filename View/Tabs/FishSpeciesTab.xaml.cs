using System.Windows;
using System.Windows.Controls;
using View.Windows;
using ViewModel;
namespace View.Tabs
{
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
    }
}
