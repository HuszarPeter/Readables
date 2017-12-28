using Readables.Common;
using System.Windows.Controls;

namespace Readables.View.Cover
{
    /// <summary>
    /// Interaction logic for ReadableCoverView.xaml
    /// </summary>
    public partial class ReadableCoverView : UserControl
    {
        public ReadableCoverView()
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
