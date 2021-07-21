using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PasswordManagerApp.Command
{
    public class Command : ICommand
    {
        Action<object> executeMethod;
        Func<object, bool> canExecuteMethod;
        public event EventHandler CanExecuteChanged;

        public Command(Action<object> executeMethod, Func<object,bool> canExecuteMethod ) 
        {
            this.executeMethod = executeMethod; 
            this.canExecuteMethod = canExecuteMethod;
        }
        public bool CanExecute(object parameter)
        {
            return this.canExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            this.executeMethod(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
