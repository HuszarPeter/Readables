using System;
namespace Readables.Import.Metadata
{
    public class ReadableMetadata
    {
        public string Title;
        public string[] Authors;
        public string Description;
        public string[] Subjects;

        public override string ToString()
        {
            return string.Format($"Title: {Title}, Description: {Description}, Subjects: {String.Join(", ", Subjects)}");
        }
    }
}
