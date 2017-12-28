using NLog;
using Readables.AggregatedEvents;
using Readables.Common;
using Readables.ViewModel;
using System.Windows;
using System;
using System.Linq;
using Microsoft.Win32;
using Readables.Import.AggregatedEvents;
using Readables.Import;
using Readables.Import.FileFormat;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Readables
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IListenTo<ImportFolderRequested>, IListenTo<ImportFileRequested>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IEventAggregator eventAggregator;

        public MainWindow()
        {
            InitializeComponent();
            this.eventAggregator = IOC.Resolve<IEventAggregator>();
            this.eventAggregator.AddListener(this);

            logger.Info($"Initialized {this.GetType().ToString()}");
        }

        public void HandleMessage(ImportFolderRequested message)
        {
            var dlg = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Multiselect = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.eventAggregator.SendMessage(new FoldersImportRequestEvent
                {
                    Folders = dlg.FileNames.Select(fn => new Uri(fn)).ToArray()
                });
            }
        }

        public void HandleMessage(ImportFileRequested message)
        {
            var importServices = IOC.ResolveAll<IReadableImportService>();
            var formatFilter = String.Join("|",
                new[] { $"All format|{String.Join(";", importServices.SelectMany(importSrv => importSrv.SupportedExtensions.Select(ext => $"*{ext}")))}" }
                .Union(
                importServices.Select(importSrv => $"{importSrv.FormatName}|{String.Join(";", importSrv.SupportedExtensions.Select(ext => $"*{ext}"))}")));

            var openDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = formatFilter
            };

            var dlgResult = openDialog.ShowDialog();
            if (dlgResult.HasValue && dlgResult.Value)
            {
                this.eventAggregator.SendMessage(new FilesImportRequestEvent
                {
                    Files = openDialog.FileNames.Select(fileName => new Uri(fileName)).ToArray()
                });
            }
        }
    }
}
