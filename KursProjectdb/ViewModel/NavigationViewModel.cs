using System.Windows.Input;
using KursProjectdb.Styles;
using KursProjectdb.Utills;
using KursProjectdb.View;

namespace KursProjectdb.ViewModel;

public class NavigationViewModel:ViewModelBase
{
    private object _currentView;
    public object CurrentView
    {
        get { return _currentView; }
        set { _currentView = value; OnPropertyChanged(); }
    }
    
    public ICommand SubjectCommand { get; set; }
    public ICommand HomeCommand { get; set; }
    public ICommand FacultyCommand { get; set; }
    public ICommand TeacherCommand { get; set; }
    public ICommand SpecializationCommand { get; set; }
    public ICommand GroupCommand { get; set; }
    public ICommand StudentCommand { get; set; }
    public ICommand AcademicPlanCommand { get; set; }
    public ICommand AcademicPerformanceCommand { get; set; }
    private void SubjectView(object obj) => CurrentView = new SubjectViewModel();
    private void HomePage(object obj) => CurrentView = new HomeViewModel();
    private void FacultyView(object obj) => CurrentView = new FacultyViewModel();
    private void SpecializationView(object obj) => CurrentView = new SpecializtionViewModel();
    private void TeacherView(object obj) => CurrentView = new TeacherViewModel();
    private void GroupView(object obj) => CurrentView = new GroupViewModel();
    private void StudentView(object obj) => CurrentView = new StudentViewModel();
    private void AcademicPlanView(object obj) => CurrentView = new AcademicPlanView();
    private void AcademicPerformanceView(object obj) => CurrentView = new AcademyPerformanceView();
    public NavigationViewModel()
    {
        SubjectCommand = new RelayCommand(SubjectView);
        HomeCommand = new RelayCommand(HomePage);
        FacultyCommand = new RelayCommand(FacultyView);
        TeacherCommand = new RelayCommand(TeacherView);
        SpecializationCommand = new RelayCommand(SpecializationView);
        GroupCommand = new RelayCommand(GroupView);
        StudentCommand = new RelayCommand(StudentView);
        AcademicPlanCommand = new RelayCommand(AcademicPlanView);
        AcademicPerformanceCommand = new RelayCommand(AcademicPerformanceView);
  
        CurrentView = new HomeViewModel();
    }
}