using System;
using System.Linq;
using Castle.Core;
using Readables.Common;
using Readables.Common.Extensions;
using Readables.Import.AggregatedEvents;

namespace Readables.Import
{
    public class StartableImportComponent : IStartable,
    IListenTo<FilesImportRequestEvent>,
    IListenTo<FoldersImportRequestEvent>
    {
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
                       this.importService.ImportFile(fileUri);
                   });
        }

        public void HandleMessage(FoldersImportRequestEvent message)
        {
            message.Folders
                   .ForEach(folderUri =>
                   {
                       this.importService.ImportFolder(folderUri);
                   });
        }
        #endregion
    }
}
