using System;
using AppKit;
using Readables.Domain;
using Readables.ViewControllers.TableView.Cells;

namespace Readables.ViewControllers.TableView
{
    public class ReadableTableViewPresenter
    {
        private const string TextCellIdentifier = "textColumnCell";
        private const string ImageCellIdentifier = "imageColumnCell";

        private ReadableListInteractor interactor;

        public ReadableTableViewPresenter()
        {
            this.interactor = new ReadableListInteractor();
        }

        internal nint ItemsCount()
        {
            return this.interactor.GetNumberofItems();
        }

        internal NSView ItemAt(int row, string columnIdentifier, NSTableView tableView)
        {
            var readable = this.interactor.ItemAt(row);

            switch (columnIdentifier)
            {
                case "titleColumn":
                    {
                        return MakeTextCellView(tableView, readable, ReadableFieldView.Title);
                    }
                case "formatsColumn":
                    {
                        var result = tableView.MakeView(ImageCellIdentifier, tableView) as ReadableTagsTableCellView;
                        if (result == null)
                        {
                            result = new ReadableTagsTableCellView()
                            {
                                Identifier = ImageCellIdentifier
                            };
                        }
                        result.Readable = readable;
                        return result;
                    }
                case "seriesColumn":
                    {
                        return MakeTextCellView(tableView, readable, ReadableFieldView.Series);
                    }
                case "dateAddedColumn":
                    {
                        return MakeTextCellView(tableView, readable, ReadableFieldView.DateAdded);
                    }
                case "authorColumn":
                    {
                        return MakeTextCellView(tableView, readable, ReadableFieldView.Author);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        private ReadableTextTableCellView MakeTextCellView(NSTableView tableView, Readable readable, ReadableFieldView field)
        {
            var result = tableView.MakeView(TextCellIdentifier, tableView) as ReadableTextTableCellView;
            if (result == null)
            {
                result = new ReadableTextTableCellView
                {
                    Identifier = TextCellIdentifier
                };
            }
            result.Readable = readable;
            result.Field = field;
            return result;
        }
    }
}
