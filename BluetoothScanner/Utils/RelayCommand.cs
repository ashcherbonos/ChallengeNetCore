using System;
using System.Windows.Input;

namespace BluetoothScanner.Utils
{
    /// <summary>
    /// Custom implementation for ICommand interface
    /// <see cref="https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.input.icommand?view=winrt-19041"/>
    /// </summary>
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
