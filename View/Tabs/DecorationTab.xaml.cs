using System.Windows;
using System.Windows.Controls;
using View.Windows;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для DecorationTab.xaml
    /// </summary>
    public partial class DecorationTab : UserControl
    {
        private readonly DecorationsViewModel _viewModel;

        public DecorationTab()
        {
            InitializeComponent();

            _viewModel = new DecorationsViewModel();

            DataContext = _viewModel;
        }

        public void OpenNewDecorationEventsWindow_Click(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateDecorationEventsViewModel();

            DecorationEventsWindow decorationSpeciesEventsWindow = new()
            {
                DataContext = windowViewModel,
                Owner = Application.Current.MainWindow
            };

            decorationSpeciesEventsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            decorationSpeciesEventsWindow.ShowDialog();
        }
    }
}
