using KursProjectdb.Utills;

namespace KursProjectdb.Model;

public class Teacher:ViewModelBase
{
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(nameof(Id)); }
    }
    
    private string fullname;
    public string Fullname
    {
        get { return fullname; }
        set { fullname = value; OnPropertyChanged(nameof(Fullname)); }
    }
}