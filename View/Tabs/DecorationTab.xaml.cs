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
    }
}
