// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Readables.ViewController.List
{
	[Register ("ReadableListViewController")]
	partial class ReadableListViewController
	{
		[Outlet]
		AppKit.NSTableView readablesTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (readablesTableView != null) {
				readablesTableView.Dispose ();
				readablesTableView = null;
			}
		}
	}
}
