// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Readables
{
	partial class AppDelegate
	{
		[Outlet]
		AppKit.NSMenuItem directoryImportMenuItem { get; set; }

		[Outlet]
		AppKit.NSMenuItem fileImportMenuItem { get; set; }

		[Action ("fileImportMenuAction:")]
		partial void fileImportMenuAction (AppKit.NSMenuItem sender);

		[Action ("folderImportMenuAction:")]
		partial void folderImportMenuAction (AppKit.NSMenuItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (fileImportMenuItem != null) {
				fileImportMenuItem.Dispose ();
				fileImportMenuItem = null;
			}

			if (directoryImportMenuItem != null) {
				directoryImportMenuItem.Dispose ();
				directoryImportMenuItem = null;
			}
		}
	}
}
