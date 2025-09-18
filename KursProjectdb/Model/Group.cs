using KursProjectdb.Utills;

namespace KursProjectdb.Model;

public class Group:ViewModelBase
{
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(nameof(Id)); }
    }
    
    private int course;
    public int Course
    {
        get { return course; }
        set { course = value; OnPropertyChanged(nameof(Course)); }
    }
    
    private string groupName;
    public string GroupName
    {
        get { return groupName; }
        set { groupName = value; OnPropertyChanged(nameof(GroupName)); }
    }
    
    private int specializationId;
    public int SpecializationId
    {
        get { return specializationId; }
        set { specializationId = value; OnPropertyChanged(nameof(SpecializationId)); }
    }
    
    private string specializationTitle;
    public string SpecializationTitle
    {
        get { return specializationTitle; }
        set { specializationTitle = value; OnPropertyChanged(nameof(SpecializationTitle)); }
    }
}
