using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Readables.Utils
{
    public class ActionCommand : ICommand
    {
        private readonly INotifyPropertyChanged viewModel;
        private readonly Action<Object> execute;
        private readonly Func<Object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public ActionCommand(INotifyPropertyChanged viewModel, Action<Object> execute, Func<Object, bool> canExecute = null)
        {
            this.viewModel = viewModel;
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;

            if (this.viewModel != null)
            {
                this.viewModel.PropertyChanged += (s, e) =>
                {
                    this.CanExecuteChanged?.Invoke(this, new EventArgs());
                };
            }
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null)
            {
                return this.canExecute(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
