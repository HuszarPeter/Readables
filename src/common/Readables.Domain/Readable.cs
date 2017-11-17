using System;

namespace Readables.Domain
{
    public class Readable
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string[] Subjects { get; set; }

        public ReadableFile[] Files { get; set; }

        public string Series { get; set; }

		public string SeriesIndex { get; set; }
  
        public int Rating { get; set; }

        public string Publisher { get; set; }

        public int PublishedYear { get; set; }

        public ReadableMetadata[] Metadata { get; set; }

        public DateTime DateAdded { get; set; }

        public byte[] CoverImageBytes { get; set; }

    }
}