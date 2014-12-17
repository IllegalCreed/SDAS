using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SDAS.ViewModels
{
    public class BaseCommand : ICommand
    {
        #region Members
        private Action mCommand;
        private Action<Object> mParaCommand;
        private Func<Object, Boolean> mCanExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Ctor
        public BaseCommand(Action command, Func<Object, Boolean> canExecute = null)
        {
            mCommand = command;
            mCanExecute = canExecute;
        }

        public BaseCommand(Action<Object> paraCommand, Func<Object, Boolean> canExecute = null)
        {
            mParaCommand = paraCommand;
            mCanExecute = canExecute;
        }
        #endregion

        public bool CanExecute(object parameter)
        {
            if (null != mCanExecute)
            {
                return mCanExecute(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            if (null != mCommand)
            {
                mCommand();
            }

            if (null != mParaCommand)
            {
                mParaCommand(parameter);
            }
        }
    }
}
