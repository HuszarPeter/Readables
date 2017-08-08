using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Readables.DataLayer;
using Readables.Domain;

namespace Readables.Import
{
    public class ImportService : IImportService
    {
        readonly IEnumerable<IReadableImportService> importServices;

        readonly IDataContext dataContext;

        public ImportService(IDataContext dataContext, IEnumerable<IReadableImportService> importServices)
        {
			// Readables.Common.IOC.Container.ResolveAll<IReadableImportService>()
			this.importServices = importServices ?? throw new ArgumentNullException(nameof(importServices));
			this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public void ImportFolder(string path)
        {
        }

        public void ImportFile(string fileName)
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
            // send notification using event aggregate
        }
    }
}
