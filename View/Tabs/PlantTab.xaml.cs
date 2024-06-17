using System.Windows;
using System.Windows.Controls;
using View.Windows;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для PlantTab.xaml
    /// </summary>
    public partial class PlantTab : UserControl
    {
        private readonly PlantsViewModel _viewModel;

        public PlantTab()
        {
            InitializeComponent();

            _viewModel = new PlantsViewModel();

            DataContext = _viewModel;
        }

        public void OpenNewPlantEventsWindow_Click(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreatePlantEventsViewModel();

            PlantEventsWindow plantEventsWindow = new()
            {
                DataContext = windowViewModel,
                Owner = Application.Current.MainWindow
            };

            plantEventsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            plantEventsWindow.ShowDialog();
        }
    }
}
