using KursProjectdb.Utills;

namespace KursProjectdb.Model;

public class Facultye: ViewModelBase
{
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(nameof(Id)); }
    }
    
    private string facultyName;
    public string FacultyName
    {
        get { return facultyName; }
        set { facultyName = value; OnPropertyChanged(nameof(FacultyName)); }
    }
}