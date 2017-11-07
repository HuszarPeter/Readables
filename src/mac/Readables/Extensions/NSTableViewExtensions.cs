using System;
using AppKit;
using Foundation;

namespace Readables.Extensions
{
    public static class NSTableViewExtensions
    {
        public static void RegisterViewWithNib<T>(this NSTableView tableView) {
            Type type = typeof(T);
            var nib = new NSNib(type.Name, NSBundle.MainBundle);
            tableView.RegisterNib(nib, type.Name);
        }

        public static T ResolveRegisteredView<T>(this NSTableView tableView) where T: NSView {
            Type type = typeof(T);
            var result = (T)tableView.MakeView(type.Name, tableView);
            return result;
        }
    }
}
