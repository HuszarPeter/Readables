using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Readables.Domain
{
    public class Readable
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public IList<String> Subjects { get; set; }

        public IList<ReadableFile> Files { get; set; }
    }
}