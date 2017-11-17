// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Readables.ViewControllers.Outline.Cells
{
    [Register(nameof(ReadableOutlineDataCellView))]
	partial class ReadableOutlineDataCellView
	{
		[Outlet]
		AppKit.NSView badgeContainerView { get; set; }

		[Outlet]
		AppKit.NSTextField badgeText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (badgeText != null) {
				badgeText.Dispose ();
				badgeText = null;
			}

			if (badgeContainerView != null) {
				badgeContainerView.Dispose ();
				badgeContainerView = null;
			}
		}
	}
}
