using Readables.ViewModel.List;
using System.Windows.Controls;

namespace Readables.View.List
{
    /// <summary>
    /// Interaction logic for ReadableListView.xaml
    /// </summary>
    public partial class ReadableListView : UserControl
    {

        private ListViewModel viewModel;
        public ReadableListView()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                viewModel = new ListViewModel();
                DataContext = viewModel;
            };
        }
    }
}
