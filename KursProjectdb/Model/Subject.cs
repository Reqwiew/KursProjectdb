using KursProjectdb.Utills;

namespace KursProjectdb.Model;

public class Subject:ViewModelBase
{
    
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(nameof(Id)); }
    }
    
    private string subjectTitle;
    public string SubjectTitle
    {
        get { return subjectTitle; }
        set { subjectTitle = value; OnPropertyChanged(nameof(SubjectTitle)); }
    }
}