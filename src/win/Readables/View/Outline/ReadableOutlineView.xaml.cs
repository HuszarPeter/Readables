using Readables.ViewModel.Outline;
using System.Windows.Controls;

namespace Readables.View.Outline
{
    /// <summary>
    /// Interaction logic for ReadableOutlineView.xaml
    /// </summary>
    public partial class ReadableOutlineView : UserControl
    {
        public ReadableOutlineView()
        {
            InitializeComponent();
        }

        private void selectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if(this.DataContext is OutlineViewModel vm)
            {
                vm.SelectedOutlineItem = e.NewValue as OutlineItemBase;
            }
        }
    }
}
