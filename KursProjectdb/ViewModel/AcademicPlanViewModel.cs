using System.Collections.ObjectModel;
using System.Linq;
using KursProjectdb.Model;
using KursProjectdb.Services;
using KursProjectdb.Styles;
using KursProjectdb.Utills;

namespace KursProjectdb.ViewModel;

public class AcademicPlanViewModel : ViewModelBase
{
    private AcademicPlanService academicPlanService;
    private SpecializationService specializationService;
    private SubjectService subjectService;
    private TeacherService teacherService;

    #region DisplayOperation

    private ObservableCollection<AcademicPlan> academicPlanList;

    public ObservableCollection<AcademicPlan> AcademicPlanList
    {
        get => academicPlanList;
        set
        {
            academicPlanList = value;
            OnPropertyChanged(nameof(AcademicPlanList));
        }
    }

    private List<Specialization> specializationList = new();

    public List<Specialization> SpecializationList
    {
        get => specializationList;
        set
        {
            specializationList = value;
            OnPropertyChanged(nameof(SpecializationList));
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

    private List<Teacher> teacherList = new();

    public List<Teacher> TeacherList
    {
        get => teacherList;
        set
        {
            teacherList = value;
            OnPropertyChanged(nameof(TeacherList));
        }
    }

    private Specialization selectedSpecialization;
    public Specialization SelectedSpecialization
    {
        get { return selectedSpecialization; }
        set
        {
            selectedSpecialization = value;
            if (selectedSpecialization != null)
            {
                CurrentAcademicPlan.SpecializationId = selectedSpecialization.Id;
            }
            OnPropertyChanged(nameof(SelectedSpecialization));
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
                CurrentAcademicPlan.SubjectId = selectedSubject.Id;
            }
            OnPropertyChanged(nameof(SelectedSubject));
        }
    }

    private Teacher selectedTeacher;
    public Teacher SelectedTeacher
    {
        get { return selectedTeacher; }
        set
        {
            selectedTeacher = value;
            if (selectedTeacher != null)
            {
                CurrentAcademicPlan.TeacherId = selectedTeacher.Id;
            }
            OnPropertyChanged(nameof(SelectedTeacher));
        }
    }

    private void LoadData()
    {
        AcademicPlanList = new ObservableCollection<AcademicPlan>(academicPlanService.GetAll());
        SpecializationList = specializationService.GetAll();
        SubjectList = subjectService.GetAll();
        TeacherList = teacherService.GetAll();
        
        if (CurrentAcademicPlan != null && CurrentAcademicPlan.SpecializationId > 0)
        {
            SelectedSpecialization = SpecializationList.FirstOrDefault(s => s.Id == CurrentAcademicPlan.SpecializationId);
        }
        
        if (CurrentAcademicPlan != null && CurrentAcademicPlan.SubjectId > 0)
        {
            SelectedSubject = SubjectList.FirstOrDefault(s => s.Id == CurrentAcademicPlan.SubjectId);
        }
        
        if (CurrentAcademicPlan != null && CurrentAcademicPlan.TeacherId > 0)
        {
            SelectedTeacher = TeacherList.FirstOrDefault(t => t.Id == CurrentAcademicPlan.TeacherId);
        }
    }

    #endregion

    private AcademicPlan currentAcademicPlan;

    public AcademicPlan CurrentAcademicPlan
    {
        get { return currentAcademicPlan; }
        set
        {
            currentAcademicPlan = value;
            OnPropertyChanged(nameof(CurrentAcademicPlan));
            
            if (currentAcademicPlan != null && SpecializationList != null)
            {
                SelectedSpecialization = SpecializationList.FirstOrDefault(s => s.Id == currentAcademicPlan.SpecializationId);
            }
            
            if (currentAcademicPlan != null && SubjectList != null)
            {
                SelectedSubject = SubjectList.FirstOrDefault(s => s.Id == currentAcademicPlan.SubjectId);
            }
            
            if (currentAcademicPlan != null && TeacherList != null)
            {
                SelectedTeacher = TeacherList.FirstOrDefault(t => t.Id == currentAcademicPlan.TeacherId);
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

    public AcademicPlanViewModel()
    {
        academicPlanService = new AcademicPlanService();
        specializationService = new SpecializationService();
        subjectService = new SubjectService();
        teacherService = new TeacherService();
        CurrentAcademicPlan = new AcademicPlan();
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
            var IsSaved = academicPlanService.Add(CurrentAcademicPlan);
            LoadData();
            if (IsSaved)
            {
                Message = "Учебный план сохранен";
                CurrentAcademicPlan = new AcademicPlan();
                SelectedSpecialization = null;
                SelectedSubject = null;
                SelectedTeacher = null;
            }
            else
                Message = "Ошибка сохранения учебного плана";
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
            if (CurrentAcademicPlan.Id == 0)
            {
                Message = "Выберите учебный план для редактирования";
                return;
            }

            var IsUpdated = academicPlanService.Update(CurrentAcademicPlan);
            LoadData();
            if (IsUpdated)
                Message = "Учебный план обновлен";
            else
                Message = "Ошибка обновления учебного плана";
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
            if (CurrentAcademicPlan.Id == 0)
            {
                Message = "Выберите учебный план для удаления";
                return;
            }

            var IsDeleted = academicPlanService.Delete(CurrentAcademicPlan.Id);
            LoadData();
            if (IsDeleted)
            {
                Message = "Учебный план удален";
                CurrentAcademicPlan = new AcademicPlan();
                SelectedSpecialization = null;
                SelectedSubject = null;
                SelectedTeacher = null;
            }
            else
                Message = "Ошибка удаления учебного плана";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion
}