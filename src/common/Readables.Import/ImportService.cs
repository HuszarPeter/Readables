using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Readables.DataLayer;
using Readables.Domain;
using Readables.Common;
using Readables.Import.AggregatedEvents;
using Readables.Import.Exceptions;
using Readables.Import.FileFormat;

namespace Readables.Import
{
    public class ImportService : IImportService
    {
        readonly IEnumerable<IReadableImportService> importServices;

        readonly IDataContext dataContext;
        readonly IReadableRepository readableRepository;

        readonly IEventAggregator eventAggregator;

        public ImportService(IDataContext dataContext, IEventAggregator eventAggregator, IEnumerable<IReadableImportService> importServices, IReadableRepository readableRepository)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
			this.importServices = importServices ?? throw new ArgumentNullException(nameof(importServices));
			this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.readableRepository = readableRepository ?? throw new ArgumentNullException(nameof(readableRepository));
        }

        public bool CanImportFile(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return this.FindReadableImportServiceByExtension(fileInfo.Extension) != null;
        }

        public void ImportPath(string folderOrFile) {
            var attributes = File.GetAttributes(folderOrFile);
            if (attributes.HasFlag(FileAttributes.Directory)) 
            {
                this.ImportFolder(folderOrFile);
            }
            else 
            {
                this.ImportFile(folderOrFile);
            }
        }

        public void ImportFolder(Uri folder) {
            ImportFolder(folder.LocalPath);
        }

        public void ImportFolder(string path)
        {
            if(string.IsNullOrEmpty(path)) 
            {
                throw new ArgumentNullException(path);
            }
            Console.WriteLine($"Import folder {path}");
            int success = 0;
			int failed = 0;
			
            foreach (var fileName in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    this.ImportFileInternal(fileName);
                    success++;
                }
                catch (Exception ex) when (ex is ReadableImportException || ex is UnknownFileTypeException)
                {
                    // do nothing, just count the errors    
                    failed++;
                }
            }
            Console.WriteLine($"Path imported");
            this.eventAggregator.SendMessage(new PathImportedEvent { 
                NumberOfFailedImport = failed, 
                NumberOfSuccessfullyImported = success});

        }

        public void ImportFile(Uri file) 
        {
            ImportFile(file.LocalPath);
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
            Console.WriteLine($"Import file: {fileName}");
            var fileInfo = new FileInfo(fileName);
            var importService = this.FindReadableImportServiceByExtension(fileInfo.Extension);
            if (importService == null)
            {
                throw new UnknownFileTypeException();
            }
            Console.WriteLine($"\tusing {importService}");

            var readable = this.ReadFile(importService, fileName);
            if (readable == null)
            {
                throw new ReadableImportException();
            }
            Console.WriteLine($"Store {readable.Title}");

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
            this.readableRepository.UpsertReadable(readable);
        }
    }
}
