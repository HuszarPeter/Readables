using System;
using AppKit;
using Foundation;
using Readables.AggregatedEvents;
using Readables.Common;
using Readables.Extensions;
using Readables.UI.Model;
using Readables.ViewControllers.TableView;

namespace Readables.ViewControllers
{
    [Register(nameof(ReadableMainEmbedViewController))]
    public partial class ReadableMainEmbedViewController: NSViewController, IListenTo<ViewChangeRequest>
    {
        private IEventAggregator eventAggregator;

        // Called when created from unmanaged code
        public ReadableMainEmbedViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableMainEmbedViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            this.eventAggregator = IOC.Resolve<IEventAggregator>();
            this.eventAggregator.AddListener(this);
        }

		public void HandleMessage(ViewChangeRequest message)
		{
            switch (message.ViewMode)
            {
                case ViewMode.List:
                    {
                        ShowList();
                        break;
                    }

                case ViewMode.Cover:
                    {
                        ShowCoverList();
                        break;
                    }
            }
        }

        private NSViewController currentViewController;

        private void SwitchViewController(NSViewController viewController)
        {
            if (currentViewController != null && currentViewController is IEventAggregatorSubscriber oldSubscriber) {
                oldSubscriber.UnSubscribeFromAggregatedEvents();
            }

            foreach (var view in this.containerView.Subviews)
            {
                view.RemoveFromSuperview();
                // unsubscribe from event aggregator ?
            }

            this.containerView.AddSubview(viewController.View);

            viewController.View.TranslatesAutoresizingMaskIntoConstraints = false;

            NSLayoutConstraint.FromVisualFormat("H:|-0-[view]-0-|", NSLayoutFormatOptions.None, "view", viewController.View)
                              .ActivateAll();
            NSLayoutConstraint.FromVisualFormat("V:|-0-[view]-0-|", NSLayoutFormatOptions.None, "view", viewController.View)
                              .ActivateAll();

            currentViewController = viewController;

            if(viewController is IEventAggregatorSubscriber newSubscriber)
            {
                newSubscriber.SubscribeToAggregatedEvents();
            }
        }

        private void ShowList()
        {
            // readableTableList
            // readableTableListWithDetail
            // readableCoverList

            var storyBoard = NSStoryboard.FromName("Main", null);
            var vc = storyBoard.InstantiateControllerWithIdentifier("readableTableList") as NSViewController;
            SwitchViewController(vc);
        }

        private void ShowCoverList()
        {
            // remove all from subviews?
            // create a new VC
            // add vc.view to subviews?

            // this.containerView.Subviews
            var storyboard = NSStoryboard.FromName("Main", null);
            var viewController = storyboard.InstantiateControllerWithIdentifier("readableCoverList") as NSViewController;
            this.SwitchViewController(viewController);

        }
    }
}
