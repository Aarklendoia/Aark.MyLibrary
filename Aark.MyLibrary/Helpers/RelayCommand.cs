using Microsoft.UI.Xaml.Input;
using System;

namespace Aark.MyLibrary.Helpers
{
    public class RelayCommand : ICommand
    {
        readonly Predicate<object> canExecute = null;
        readonly Action<Object> executeAction = null;

        public RelayCommand(Action executeAction)
            : this(param => true, param => executeAction())
        {
        }

        public RelayCommand(Action<object> executeAction)
            : this(param => true, executeAction)
        {
        }

        public RelayCommand(Predicate<object> canExecute, Action<object> executeAction)
        {
            this.canExecute = canExecute;
            this.executeAction = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction?.Invoke(parameter);
            UpdateCanExecuteState();
        }

        public void UpdateCanExecuteState()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        event EventHandler<object> ICommand.CanExecuteChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }
    }


    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        private readonly Func<T, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        event EventHandler<object> ICommand.CanExecuteChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }
    }
}