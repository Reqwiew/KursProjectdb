using KursProjectdb.Utills;

namespace KursProjectdb.Model;

public class AcademicPerformance: ViewModelBase
{
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(nameof(Id)); }
    }
    
    private int studentId;
    public int StudentId
    {
        get { return studentId; }
        set { studentId = value; OnPropertyChanged(nameof(StudentId)); }
    }
    
    private string studentFullName;
    public string StudentFullName
    {
        get { return studentFullName; }
        set { studentFullName = value; OnPropertyChanged(nameof(StudentFullName)); }
    }
    
    private int academicPlanId;
    public int AcademicPlanId
    {
        get { return academicPlanId; }
        set { academicPlanId = value; OnPropertyChanged(nameof(AcademicPlanId)); }
    }
    private int subjectId;
    public int SubjectId
    {
        get { return subjectId; }
        set { subjectId = value; OnPropertyChanged(nameof(SubjectId)); }
    }
    private string subjectTitle;
    public string SubjectTitle
    {
        get { return subjectTitle; }
        set { subjectTitle = value; OnPropertyChanged(nameof(SubjectTitle)); }
    }
    
    private int term;
    public int Term
    {
        get { return term; }
        set { term = value; OnPropertyChanged(nameof(Term)); }
    }
    
    private int rating;
    public int Rating
    {
        get { return rating; }
        set { rating = value; OnPropertyChanged(nameof(Rating)); }
    }
    
    private DateTime dateOffset;
    public DateTime DateOffset
    {
        get { return dateOffset; }
        set { dateOffset = value; OnPropertyChanged(nameof(DateOffset)); }
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