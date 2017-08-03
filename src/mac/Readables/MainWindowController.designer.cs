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
    [Register ("MainWindowController")]
    partial class MainWindowController
    {
        [Outlet]
        AppKit.NSSegmentedControl mainViewSelector { get; set; }

        [Action ("mainViewSelectorChanged:")]
        partial void mainViewSelectorChanged (AppKit.NSSegmentedControl sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (mainViewSelector != null) {
                mainViewSelector.Dispose ();
                mainViewSelector = null;
            }
        }
    }
}
