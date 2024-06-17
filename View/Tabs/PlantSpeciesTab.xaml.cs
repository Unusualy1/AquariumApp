using System.Windows;
using System.Windows.Controls;
using View.Windows;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для PlantSpeciesTab.xaml
    /// </summary>
    public partial class PlantSpeciesTab : UserControl
    {
        private readonly PlantSpeciesViewModel _viewModel;

        public PlantSpeciesTab()
        {
            InitializeComponent();

            _viewModel = new PlantSpeciesViewModel();

            DataContext = _viewModel;
        }

        public void OpenNewPlantSpeciesEventsWindow_Click(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreatePlantSpeciesEventsViewModel();

            PlantSpeciesEventsWindow plantSpeciesEventsWindow = new()
            {
                DataContext = windowViewModel,
                Owner = Application.Current.MainWindow
            };

            plantSpeciesEventsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            plantSpeciesEventsWindow.ShowDialog();
        }
    }
}
