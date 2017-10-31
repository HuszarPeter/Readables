using System;
using AppKit;
using Foundation;
using Readables.DataLayer;
using System.Linq;
using Readables.Common;
using System.Collections.Generic;
using Readables.Domain;

namespace Readables.ViewController.List
{
	public class ReadableListViewDataSource : NSTableViewDataSource
	{
        readonly IReadableRepository readableRepository;

        private IList<Readable> allReadables;

        public ReadableListViewDataSource(IReadableRepository readableRepository)
        {
            this.readableRepository = readableRepository ?? throw new ArgumentNullException(nameof(readableRepository));
        }

		public override nint GetRowCount(NSTableView tableView)
		{
            this.allReadables = this.readableRepository.GetAllReadables();
            return allReadables.Count;
		}

		public override NSObject GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, nint row)
		{
            var readable = this.allReadables.ElementAt((int)row);
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
