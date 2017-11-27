using Castle.Core;
using NLog;
using Readables.Common;
using Readables.Common.Extensions;
using Readables.Import.AggregatedEvents;
using System;

namespace Readables.Import
{
    public class StartableImportComponent : 
        IStartable,
        IListenTo<FilesImportRequestEvent>,
        IListenTo<FoldersImportRequestEvent>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        readonly IEventAggregator eventAggregator;
        readonly IImportService importService;

        public StartableImportComponent(IEventAggregator eventAggregator, IImportService importService)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.importService = importService ?? throw new ArgumentNullException(nameof(importService));
        }

        public void Start()
        {
            this.eventAggregator.AddListener(this);
        }

        public void Stop()
        {
            // remove this from eventaggregator
        }

        #region Aggregated message handlers
        public void HandleMessage(FilesImportRequestEvent message)
        {
            message.Files
                   .ForEach(fileUri =>
                   {
                       logger.Info($"File import request: {fileUri}");
                       this.importService.ImportFile(fileUri);
                   });
        }

        public void HandleMessage(FoldersImportRequestEvent message)
        {
            message.Folders
                   .ForEach(folderUri =>
                   {
                       logger.Info($"Folder import request: {folderUri}");
                       this.importService.ImportFolder(folderUri);
                   });
        }
        #endregion
    }
}
