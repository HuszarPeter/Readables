﻿using System;
using System.Linq;
using Foundation;
using AppKit;
using Readables.Common;
using Readables.AggregatedEvents;
using AutoMapper;
using Readables.UI.Model;
using Readables.UI.AggregatedEvents;

namespace Readables
{
    public partial class MainWindowController : NSWindowController, 
    IListenTo<MenuImportFileRequest>,
    IListenTo<MenuImportDirectoryRequest>
    {
        private IEventAggregator eventAggregator;

        public MainWindowController(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public MainWindowController(NSCoder coder) : base(coder)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.eventAggregator = IOC.Resolve<IEventAggregator>();
            this.eventAggregator.AddListener(this);

            SetViewMode(ViewMode.Cover);
        }

        public new MainWindow Window
        {
            get { return (MainWindow)base.Window; }
        }

        #region Aggredated Event Handling
        public void HandleMessage(MenuImportFileRequest message)
        {
            var openPanel = new NSOpenPanel
            {
                CanChooseFiles = true,
                CanChooseDirectories = false,
                AllowsMultipleSelection = true,
                ResolvesAliases = true
            };
            openPanel.BeginSheet(Window, (result) =>
            {
                this.eventAggregator.SendMessage(new MenuImportFileRequestCompleted());
                if (result == 1)
                {
                    this.eventAggregator.SendMessage(
                        new Import.AggregatedEvents.FilesImportRequestEvent
                        {
                            Files = openPanel.Urls.Select(nsUrl => new Uri(nsUrl.AbsoluteString)).ToArray()
                        });
                }
            });
        }

        public void HandleMessage(MenuImportDirectoryRequest message)
        {
            var openPanel = new NSOpenPanel
            {
                CanChooseFiles = false,
                CanChooseDirectories = true,
                ResolvesAliases = true
            };
            openPanel.BeginSheet(Window, (result) =>
            {
                this.eventAggregator.SendMessage(new MenuImportDirectoryRequestCompleted ());
                if (result == 1) 
                {
                    this.eventAggregator.SendMessage(
                        new Import.AggregatedEvents.FoldersImportRequestEvent
                        {
                            Folders = openPanel.Urls.Select(u => new Uri(u.AbsoluteString)).ToArray()
                        }
                    );
                }
            });
        }
		#endregion

        private void SetViewMode(ViewMode viewMode) {
            this.changeView.SetSelected(true, Mapper.Map<nint>(viewMode));
            this.eventAggregator.SendMessage(new ViewChangeRequest { ViewMode = viewMode });
        }

        partial void onViewChanged(NSSegmentedControl sender)
        {
            var viewMode = Mapper.Map<UI.Model.ViewMode>(sender.SelectedSegment);
            this.SetViewMode(viewMode);
        }

        partial void onSearchValueChanged(NSSearchField sender)
        {
            this.eventAggregator.SendMessage(new FilterForStringRequest { FilterValue = sender.StringValue });
        }
    }
}
