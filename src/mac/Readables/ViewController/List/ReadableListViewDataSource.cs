using System;
using AppKit;
using Foundation;
using Readables.DataLayer;
using System.Linq;
using Readables.Common;

namespace Readables.ViewController.List
{
	public class ReadableListViewDataSource : NSTableViewDataSource
	{
        readonly IDataContext dataContext;

        public ReadableListViewDataSource(IDataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

		public override nint GetRowCount(NSTableView tableView)
		{
			var result = this.dataContext.Readables.Count();
			return result;
		}

		public override NSObject GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, nint row)
		{
            var readable = this.dataContext.Readables.ElementAt((int)row);
            switch(tableColumn.Identifier)
            {
                case "titleColumn":
                    return new NSString(readable.Title);
                case "authorColumn":
                    return new NSString(readable.Author ?? "");
                default:
                    return new NSString("??");
            }
		}
	}
}
