using System.Windows.Controls;
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
    }
}
