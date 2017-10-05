using System;
using AppKit;
using Foundation;
using Readables.Import;

namespace Readables.Utils
{
    public class PathDropHandler
    {
        private readonly NSView view;
        private readonly IImportService importService;

        public PathDropHandler(NSView view, IImportService importService)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.importService = importService ?? throw new ArgumentNullException(nameof(importService));

            this.view.RegisterForDraggedTypes(new string[] { NSPasteboard.NSFilenamesType });
        }

        public NSDragOperation GetAvailableDraggingOperation(NSDraggingInfo info) {
            if (info.DraggingSourceOperationMask.HasFlag(NSDragOperation.Copy)) {
                return NSDragOperation.Copy;
            }
            return NSDragOperation.None;
        }

        public bool PerformDrop(NSDraggingInfo info) {
			var fileNames = (NSArray)info.DraggingPasteboard.GetPropertyListForType(NSPasteboard.NSFilenamesType);
			if (fileNames != null)
			{
				for (nuint i = 0; i < fileNames.Count; i++)
				{
					var item = fileNames.GetItem<NSString>(i);
					this.importService.ImportPath(item);
				}

				return true;
			}
			else
			{
                return false;
			}
        }
    }
}
