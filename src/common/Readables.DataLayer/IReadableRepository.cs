using System.Collections.Generic;
using Readables.Domain;

namespace Readables.DataLayer
{
    public interface IReadableRepository
    {
        IList<Readable> GetAllReadables();

        void UpsertReadable(Readable readable);
    }
}
