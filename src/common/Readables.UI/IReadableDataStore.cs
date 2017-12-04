using System.Collections.Generic;
using Readables.Domain;
using Readables.UI.Model;

namespace Readables.UI
{
    public interface IReadableDataStore
    {
        IEnumerable<Readable> VisibleReadables { get; }

        IEnumerable<UtilityItemSubject> Subjects { get; }

        IEnumerable<UtilityItemLibrary> LibraryItems { get; }

        void SetLibraryItemFilter(UtilityItemLibrary item);

        void SetSubjectFilter(UtilityItemSubject subject);

        void SetFilterString(string filter);
    }
}
