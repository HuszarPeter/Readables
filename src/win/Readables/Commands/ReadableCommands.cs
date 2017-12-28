using Readables.AggregatedEvents;
using Readables.Common;
using Readables.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Readables.Commands
{
    public static class ReadableCommands
    {
        private static ActionCommand exitCommand = new ActionCommand(null, Exit);
        private static ActionCommand importFileCommand = new ActionCommand(null, ImportFile);
        private static ActionCommand importFolderCommand = new ActionCommand(null, ImportFolder);

        public static ICommand ExitCommand
        {
            get
            {
                return exitCommand;
            }
        }

        public static ICommand ImportFileCommand
        {
            get
            {
                return importFileCommand;
            }
        }

        public static ICommand ImportFolderCommand
        {
            get
            {
                return importFolderCommand;
            }
        }

        private static void Exit(object obj)
        {
            Application.Current.Shutdown(0);
        }

        private static void ImportFolder(object obj)
        {
            IOC.Resolve<IEventAggregator>().SendMessage(new ImportFolderRequested());
        }

        private static void ImportFile(object obj)
        {
            IOC.Resolve<IEventAggregator>().SendMessage(new ImportFileRequested());
        }
    }
}
