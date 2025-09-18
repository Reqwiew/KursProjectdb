using System.Collections.ObjectModel;
using System.Linq;
using KursProjectdb.Model;
using KursProjectdb.Services;
using KursProjectdb.Styles;
using KursProjectdb.Utills;

namespace KursProjectdb.ViewModel;

public class AcademyPerformanceViewModel : ViewModelBase
{
    private AcademicPerformanceService academicPerformanceService;
    private StudentService studentService;
    private SubjectService subjectService;
    private GroupService groupService;
    private AcademicPlanService academicPlanService;

    #region DisplayOperation

    private ObservableCollection<AcademicPerformance> academicPerformanceList;

    public ObservableCollection<AcademicPerformance> AcademicPerformanceList
    {
        get => academicPerformanceList;
        set
        {
            academicPerformanceList = value;
            OnPropertyChanged(nameof(AcademicPerformanceList));
        }
    }

    private List<Student> studentList = new();

    public List<Student> StudentList
    {
        get => studentList;
        set
        {
            studentList = value;
            OnPropertyChanged(nameof(StudentList));
        }
    }

    private List<Subject> subjectList = new();

    public List<Subject> SubjectList
    {
        get => subjectList;
        set
        {
            subjectList = value;
            OnPropertyChanged(nameof(SubjectList));
        }
    }

    private List<Group> groupList = new();

    public List<Group> GroupList
    {
        get => groupList;
        set
        {
            groupList = value;
            OnPropertyChanged(nameof(GroupList));
        }
    }

    private List<AcademicPlan> academicPlanList = new();

    public List<AcademicPlan> AcademicPlanList
    {
        get => academicPlanList;
        set
        {
            academicPlanList = value;
            OnPropertyChanged(nameof(AcademicPlanList));
        }
    }

    private Student selectedStudent;
    public Student SelectedStudent
    {
        get { return selectedStudent; }
        set
        {
            selectedStudent = value;
            if (selectedStudent != null)
            {
                CurrentAcademicPerformance.StudentId = selectedStudent.Id;
            }
            OnPropertyChanged(nameof(SelectedStudent));
        }
    }

    private Subject selectedSubject;
    public Subject SelectedSubject
    {
        get { return selectedSubject; }
        set
        {
            selectedSubject = value;
            if (selectedSubject != null)
            {
                CurrentAcademicPerformance.SubjectId = selectedSubject.Id;
            }
            OnPropertyChanged(nameof(SelectedSubject));
        }
    }

    private Group selectedGroup;
    public Group SelectedGroup
    {
        get { return selectedGroup; }
        set
        {
            selectedGroup = value;
            if (selectedGroup != null)
            {
                CurrentAcademicPerformance.GroupId = selectedGroup.Id;
            }
            OnPropertyChanged(nameof(SelectedGroup));
        }
    }

    private AcademicPlan selectedAcademicPlan;
    public AcademicPlan SelectedAcademicPlan
    {
        get { return selectedAcademicPlan; }
        set
        {
            selectedAcademicPlan = value;
            if (selectedAcademicPlan != null)
            {
                CurrentAcademicPerformance.AcademicPlanId = selectedAcademicPlan.Id;
            }
            OnPropertyChanged(nameof(SelectedAcademicPlan));
        }
    }

    private void LoadData()
    {
        AcademicPerformanceList = new ObservableCollection<AcademicPerformance>(academicPerformanceService.GetAll());
        StudentList = studentService.GetAll();
        SubjectList = subjectService.GetAll();
        GroupList = groupService.GetAll();
        AcademicPlanList = academicPlanService.GetAll();
        
        if (CurrentAcademicPerformance != null && CurrentAcademicPerformance.StudentId > 0)
        {
            SelectedStudent = StudentList.FirstOrDefault(s => s.Id == CurrentAcademicPerformance.StudentId);
        }
        
        if (CurrentAcademicPerformance != null && CurrentAcademicPerformance.SubjectId > 0)
        {
            SelectedSubject = SubjectList.FirstOrDefault(s => s.Id == CurrentAcademicPerformance.SubjectId);
        }
        
        if (CurrentAcademicPerformance != null && CurrentAcademicPerformance.GroupId > 0)
        {
            SelectedGroup = GroupList.FirstOrDefault(g => g.Id == CurrentAcademicPerformance.GroupId);
        }
        
        if (CurrentAcademicPerformance != null && CurrentAcademicPerformance.AcademicPlanId > 0)
        {
            SelectedAcademicPlan = AcademicPlanList.FirstOrDefault(a => a.Id == CurrentAcademicPerformance.AcademicPlanId);
        }
    }

    #endregion

    private AcademicPerformance currentAcademicPerformance;

    public AcademicPerformance CurrentAcademicPerformance
    {
        get { return currentAcademicPerformance; }
        set
        {
            currentAcademicPerformance = value;
            OnPropertyChanged(nameof(CurrentAcademicPerformance));
            
            if (currentAcademicPerformance != null && StudentList != null)
            {
                SelectedStudent = StudentList.FirstOrDefault(s => s.Id == currentAcademicPerformance.StudentId);
            }
            
            if (currentAcademicPerformance != null && SubjectList != null)
            {
                SelectedSubject = SubjectList.FirstOrDefault(s => s.Id == currentAcademicPerformance.SubjectId);
            }
            
            if (currentAcademicPerformance != null && GroupList != null)
            {
                SelectedGroup = GroupList.FirstOrDefault(g => g.Id == currentAcademicPerformance.GroupId);
            }
            
            if (currentAcademicPerformance != null && AcademicPlanList != null)
            {
                SelectedAcademicPlan = AcademicPlanList.FirstOrDefault(a => a.Id == currentAcademicPerformance.AcademicPlanId);
            }
        }
    }

    private string message;

    public string Message
    {
        get { return message; }
        set
        {
            message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public AcademyPerformanceViewModel()
    {
        academicPerformanceService = new AcademicPerformanceService();
        studentService = new StudentService();
        subjectService = new SubjectService();
        groupService = new GroupService();
        academicPlanService = new AcademicPlanService();
        CurrentAcademicPerformance = new AcademicPerformance();
        LoadData();
        saveCommand = new RelayCommandSQL(Save);
        updateCommand = new RelayCommandSQL(Update);
        deleteCommand = new RelayCommandSQL(Delete);
    }

    #region SaveOperation

    private RelayCommandSQL saveCommand;

    public RelayCommandSQL SaveCommand
    {
        get { return saveCommand; }
    }

    public void Save()
    {
        try
        {
            var IsSaved = academicPerformanceService.Add(CurrentAcademicPerformance);
            LoadData();
            if (IsSaved)
            {
                Message = "Успеваемость сохранена";
                CurrentAcademicPerformance = new AcademicPerformance();
                SelectedStudent = null;
                SelectedSubject = null;
                SelectedGroup = null;
                SelectedAcademicPlan = null;
            }
            else
                Message = "Ошибка сохранения успеваемости";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion

    #region UpdateOperation

    private RelayCommandSQL updateCommand;

    public RelayCommandSQL UpdateCommand
    {
        get { return updateCommand; }
    }

    public void Update()
    {
        try
        {
            if (CurrentAcademicPerformance.Id == 0)
            {
                Message = "Выберите запись успеваемости для редактирования";
                return;
            }

            var IsUpdated = academicPerformanceService.Update(CurrentAcademicPerformance);
            LoadData();
            if (IsUpdated)
                Message = "Успеваемость обновлена";
            else
                Message = "Ошибка обновления успеваемости";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion

    #region DeleteOperation

    private RelayCommandSQL deleteCommand;

    public RelayCommandSQL DeleteCommand
    {
        get { return deleteCommand; }
    }

    public void Delete()
    {
        try
        {
            if (CurrentAcademicPerformance.Id == 0)
            {
                Message = "Выберите запись успеваемости для удаления";
                return;
            }

            var IsDeleted = academicPerformanceService.Delete(CurrentAcademicPerformance.Id);
            LoadData();
            if (IsDeleted)
            {
                Message = "Успеваемость удалена";
                CurrentAcademicPerformance = new AcademicPerformance();
                SelectedStudent = null;
                SelectedSubject = null;
                SelectedGroup = null;
                SelectedAcademicPlan = null;
            }
            else
                Message = "Ошибка удаления успеваемости";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion
}