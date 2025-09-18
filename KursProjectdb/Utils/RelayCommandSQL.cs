using System.Windows.Input;

namespace KursProjectdb.Styles;

public class RelayCommandSQL:ICommand
{
    public event EventHandler CanExecuteChanged;

    private Action DoWork;
    public RelayCommandSQL(Action work)
    {
        DoWork = work;
    }
    public bool CanExecute(object parameter)
    {
        return true;
    }
    public void Execute(object parameter)
    {
        DoWork();
    }
}