using KursProjectdb.Utills;

namespace KursProjectdb.Model;

public class AcademicPlan:ViewModelBase
{
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(nameof(Id)); }
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
    
    private int subjectId;
    public int SubjectId
    {
        get { return subjectId; }
        set { subjectId = value; OnPropertyChanged(nameof(SubjectId)); }
    }
    
    private string subjectName;
    public string SubjectName
    {
        get { return subjectName; }
        set { subjectName = value; OnPropertyChanged(nameof(SubjectName)); }
    }
    
    
    private int term;
    public int Term
    {
        get { return term; }
        set { term = value; OnPropertyChanged(nameof(Term)); }
    }
    
    private int teacherId;
    public int TeacherId
    {
        get { return teacherId; }
        set { teacherId = value; OnPropertyChanged(nameof(TeacherId)); }
    }
    
    private string teacherFullName;
    public string TeacherFullName
    {
        get { return teacherFullName; }
        set { teacherFullName = value; OnPropertyChanged(nameof(TeacherFullName)); }
    }
    
    private int hours;
    public int Hours
    {
        get { return hours; }
        set { hours = value; OnPropertyChanged(nameof(Hours)); }
    }
    
    private string form;
    public string Form
    {
        get { return form; }
        set { form = value; OnPropertyChanged(nameof(Form)); }
    }
    
}