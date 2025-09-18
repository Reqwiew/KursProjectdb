using KursProjectdb.Utills;

namespace KursProjectdb.Model;

public class Student:ViewModelBase
{
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(nameof(Id)); }
    }
    
    private string fullName;
    public string FullName
    {
        get { return fullName; }
        set { fullName = value; OnPropertyChanged(nameof(FullName)); }
    }
    
    private DateTime birthday;
    public DateTime Birthday
    {
        get { return birthday; }
        set { birthday = value; OnPropertyChanged(nameof(Birthday)); }
    }
    
    private string gender;
    public string Gender
    {
        get { return gender; }
        set { gender = value; OnPropertyChanged(nameof(Gender)); }
    }
    private int groupId;
    public int GroupId
    {
        get { return groupId; }
        set { groupId = value; OnPropertyChanged(nameof(GroupId)); }
    }
    private string groupName;
    public string GroupName
    {
        get { return groupName; }
        set { groupName = value; OnPropertyChanged(nameof(GroupName)); }
    }
}