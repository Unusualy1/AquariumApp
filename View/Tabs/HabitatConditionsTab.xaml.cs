using System.Windows;
using System.Windows.Controls;
using View.Tabs.HabitatConditions;
using View.Windows;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для HabitatConditionsTab.xaml
    /// </summary>
    public partial class HabitatConditionsTab : UserControl
    {
        private readonly HabitatConditionsViewModel _viewModel;

        public HabitatConditionsTab()
        {
            InitializeComponent();

            _viewModel = new HabitatConditionsViewModel();
            DataContext = _viewModel;
        }

        private void OpenHabitatConditionsEditWindowButton_Click(object sender, RoutedEventArgs e)
        {
            HabitatConditionsEditWindow editWindow = new()
            {
                DataContext = _viewModel,
                Owner = Application.Current.MainWindow
            };

            editWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            editWindow.ShowDialog();
        }
        private void OpenHabitatConditionsEventsButton_Click(object sender,  EventArgs e)
        {
            HabitatConditionsEventsWindow habitatConditionsEventsWindow = new()
            {
                DataContext = _viewModel,
                Owner = Application.Current.MainWindow
            };

            habitatConditionsEventsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            habitatConditionsEventsWindow.ShowDialog();
        }
    }
}
