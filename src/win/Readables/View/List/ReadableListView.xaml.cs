using Readables.Common;
using Readables.ViewModel.List;
using System.Windows.Controls;

namespace Readables.View.List
{
    /// <summary>
    /// Interaction logic for ReadableListView.xaml
    /// </summary>
    public partial class ReadableListView : UserControl
    {
        public ReadableListView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                if (this.DataContext is IEventAggregatorSubscriber sub)
                {
                    sub.SubscribeToAggregatedEvents();
                }
            };

            this.Unloaded += (s, e) =>
            {
                if (this.DataContext is IEventAggregatorSubscriber sub)
                {
                    sub.UnSubscribeFromAggregatedEvents();
                }
            };
        }
    }
}
