using System.Collections.Generic;
using Readables.Data.Model;
using Readables.Domain;

namespace Readables.Data
{
    public interface IDataRepository
    {
        IEnumerable<Readable> VisibleReadables { get; }

        IEnumerable<OutlineItemSubject> Subjects { get; }

        IEnumerable<OutlineItemLibrary> LibraryItems { get; }

        void SetLibraryItemFilter(OutlineItemLibrary item);

        void SetSubjectFilter(OutlineItemSubject subject);

        void SetFilterString(string filter);
    }
}
