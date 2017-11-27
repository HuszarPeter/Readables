using Readables.ViewModel.Outline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Readables.View.Outline
{
    /// <summary>
    /// Interaction logic for ReadableOutlineView.xaml
    /// </summary>
    public partial class ReadableOutlineView : UserControl
    {
        private OutlineViewModel vm;

        public ReadableOutlineView()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                this.vm = new OutlineViewModel();
                this.DataContext = this.vm;
            };
        }
    }
}
