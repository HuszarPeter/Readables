// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Readables.ViewController
{
	[Register ("ReadableItemView")]
	partial class ReadableItemView
	{
		[Outlet]
		AppKit.NSTextField authorLabel { get; set; }

		[Outlet]
		AppKit.NSImageView coverImage { get; set; }

		[Outlet]
		AppKit.NSTextField titleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (coverImage != null) {
				coverImage.Dispose ();
				coverImage = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (authorLabel != null) {
				authorLabel.Dispose ();
				authorLabel = null;
			}
		}
	}
}
