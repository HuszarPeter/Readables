using System;
using AppKit;
using Foundation;
using Readables.Domain;

namespace Readables.Extensions
{
    public static class ReadableExtensions
    {
        public static NSImage CoverImage(this Readable readable)
        {
            if (readable == null)
            {
                throw new ArgumentNullException(nameof(readable));
            }

            if (readable.CoverImageBytes != null)
            {
                return new NSImage(NSData.FromArray(readable.CoverImageBytes));
            }
            else
            {
                return NSImage.ImageNamed("cover");
            }
        }
    }
}
