using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Readables.DataLayer;
using Readables.Domain;
using Readables.Common;
using Readables.Import.AggregatedEvents;

namespace Readables.Import
{
    public class ImportService : IImportService
    {
        readonly IEnumerable<IReadableImportService> importServices;

        readonly IDataContext dataContext;

        readonly IEventAggregator eventAggregator;

        public ImportService(IDataContext dataContext, IEventAggregator eventAggregator, IEnumerable<IReadableImportService> importServices)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
			this.importServices = importServices ?? throw new ArgumentNullException(nameof(importServices));
			this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public void ImportFolder(string path)
        {
            if(string.IsNullOrEmpty(path)) 
            {
                throw new ArgumentNullException(path);
            }

			int success = 0;
			int failed = 0;
			
            foreach (var fileName in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    this.ImportFileInternal(fileName);
                    success++;
                }
                catch (Exception) 
                {
                    // do nothing, just count the errors    
                    failed++;
                }
            }

            this.eventAggregator.SendMessage(new PathImportedEvent { 
                NumberOfFailedImport = failed, 
                NumberOfSuccessfullyImported = success});

        }

        public void ImportFile(string fileName) 
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(fileName);
            }

            this.ImportFileInternal(fileName);
            this.eventAggregator.SendMessage(new FileImportedEvent(fileName));
		}

        private void ImportFileInternal(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            var importService = this.FindReadableImportServiceByExtension(fileInfo.Extension);
            if (importService == null)
            {
                throw new Exception("Unknown file type"); // TODO: Create custom exception
            }

            var readable = this.ReadFile(importService, fileName);
            if (readable == null)
            {
                throw new Exception("Cannot import file"); // TODO: Create custom exception    
            }

            this.StoreReadable(readable);
        }

        private IReadableImportService FindReadableImportServiceByExtension(string extension)
        {
            return this.importServices.FirstOrDefault(importer => importer.SupportedExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase));
        }

        private Readable ReadFile(IReadableImportService importService, string fileName)
        {
            return importService.Import(fileName);
        }

        private void StoreReadable(Readable readable)
        {
            this.dataContext.Upsert(readable);
        }
    }
}
