// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;

namespace Readables.ViewControllers
{
    [Register(nameof(ReadableOutlineViewController))]
    partial class ReadableOutlineViewController
	{
		[Outlet]
		AppKit.NSOutlineView outlineView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (outlineView != null) {
				outlineView.Dispose ();
				outlineView = null;
			}
		}
	}
}
