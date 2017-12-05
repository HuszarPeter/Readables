using NLog;
using Readables.ViewModel;
using System.Windows;

namespace Readables
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            logger.Info($"Initialized {this.GetType().ToString()}");
        }
    }
}
