using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Readables.Domain
{
    public class Readable
    {
        public string Id;

        public string Title;

        public IList<ReadableFile> Files;
    }
}