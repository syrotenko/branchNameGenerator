using System;
using System.Windows.Input;

namespace GenerateBranchName
{
    public class WpfCommand : ICommand
    {
        public WpfCommand(Action<object> action, Predicate<object> canExecuteAction) 
        {
            CustomAction = action;
            CanExecuteAction = canExecuteAction;
        }

        readonly Action<object> CustomAction;
        readonly Predicate<object> CanExecuteAction;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteAction != null && CanExecuteAction.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            CustomAction?.Invoke(parameter);
        }
    }
}
