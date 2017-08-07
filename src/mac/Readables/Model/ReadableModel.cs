using Foundation;

namespace Readables.Model
{
    [Register(nameof(ReadableModel))]
    public class ReadableModel: NSObject
    {
        string title;

        [Export(nameof(Title))]
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                WillChangeValue(nameof(Title));
                title = value;
                DidChangeValue(nameof(Title));
            }
        }
        string author;

        [Export(nameof(Author))]
        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                WillChangeValue(nameof(Author));
                author = value;
                DidChangeValue(nameof(Author));
            }
        }
    }
}
