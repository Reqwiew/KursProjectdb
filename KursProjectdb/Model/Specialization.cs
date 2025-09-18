using KursProjectdb.Utills;

namespace KursProjectdb.Model;

public class Specialization:ViewModelBase
{
    private int id;
    public int Id
    {
        get { return id; }
        
        set { id = value; OnPropertyChanged(nameof(Id)); }
    }
    
    private int facultyId;
    public int FacultyId
    {
        get { return facultyId; }
        set { facultyId = value; OnPropertyChanged(nameof(FacultyId)); }
    }
    private string specializationTitle;
    public string SpecializationTitle
    {
        get { return specializationTitle; }
        set { specializationTitle = value; OnPropertyChanged(nameof(SpecializationTitle)); }
    }
    private string facultyName;
    public string FacultyName
    {
        get { return facultyName; }
        set { facultyName = value; OnPropertyChanged(nameof(FacultyName)); }
    }
}