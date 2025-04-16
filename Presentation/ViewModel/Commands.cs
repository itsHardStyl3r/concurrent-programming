using System.Windows.Input;

namespace Presentation.Commands
{
    public class Command : ICommand
    {
        private Action<object> exec;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public Command(Action<object> execute)
        {
            exec = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            exec(parameter);
        }
    }
}