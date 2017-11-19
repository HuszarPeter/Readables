using System;

namespace Readables.Domain
{
    public class Readable
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
       
        string[] subjects = { };
        public string[] Subjects
        {
            get
            {
                return subjects;
            }

            set
            {
                subjects = value;
            }
        }

        ReadableFile[] files = { };
        public ReadableFile[] Files
        {
            get
            {
                return files;
            }

            set
            {
                files = value;
            }
        }

        public string Series { get; set; }

		public string SeriesIndex { get; set; }
  
        public int Rating { get; set; }

        public string Publisher { get; set; }

        public int PublishedYear { get; set; }

        ReadableMetadata[] metadata = { };
        public ReadableMetadata[] Metadata
        {
            get
            {
                return metadata;
            }

            set
            {
                metadata = value;
            }
        }

        public DateTime DateAdded { get; set; }

        public byte[] CoverImageBytes { get; set; }

    }
}