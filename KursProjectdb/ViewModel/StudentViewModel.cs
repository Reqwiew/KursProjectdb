using System.Collections.ObjectModel;
using KursProjectdb.Model;
using KursProjectdb.Services;
using KursProjectdb.Styles;
using KursProjectdb.Utills;

namespace KursProjectdb.ViewModel;

public class StudentViewModel : ViewModelBase
{
    private StudentService studService;
    private GroupService groupService;

    #region DisplayOperation

    private ObservableCollection<Student> studList;

    public ObservableCollection<Student> StudList
    {
        get => studList;
        set
        {
            studList = value;
            OnPropertyChanged(nameof(StudList));
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

    private Group selectedGroup;
    public Group SelectedGroup
    {
        get { return selectedGroup; }
        set
        {
            selectedGroup = value;
            if (selectedGroup != null)
            {
                CurrentStudent.GroupId = selectedGroup.Id;
            }
            OnPropertyChanged(nameof(SelectedGroup));
        }
    }

    private void LoadData()
    {
        StudList = new ObservableCollection<Student>(studService.GetAll());
        GroupList = groupService.GetAll();
        
        if (CurrentStudent != null && CurrentStudent.GroupId > 0)
        {
            SelectedGroup = GroupList.FirstOrDefault(s => s.Id == CurrentStudent.GroupId);
        }
    }

    #endregion

    private Student currentStudent;

    public Student CurrentStudent
    {
        get { return currentStudent; }
        set
        {
            currentStudent = value;
            OnPropertyChanged(nameof(CurrentStudent));
            
            if (currentStudent != null && GroupList != null)
            {
                SelectedGroup = GroupList.FirstOrDefault(s => s.Id == currentStudent.GroupId);
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

    public StudentViewModel()
    {
        studService = new StudentService();
        groupService = new GroupService();
        CurrentStudent = new Student();
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
            var IsSaved = studService.Add(CurrentStudent);
            LoadData();
            if (IsSaved)
            {
                Message = "Студента сохранена";
                CurrentStudent = new Student();
                SelectedGroup = null;
            }
            else
                Message = "Ошибка сохранения студента";
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
            if (CurrentStudent.Id == 0)
            {
                Message = "Выберите студента для редактирования";
                return;
            }

            var IsUpdated = studService.Update(CurrentStudent);
            LoadData();
            if (IsUpdated)
                Message = "Студент обновлена";
            else
                Message = "Ошибка обновления студента";
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
            if (CurrentStudent.Id == 0)
            {
                Message = "Выберите студента для удаления";
                return;
            }

            var IsDeleted = studService.Delete(CurrentStudent.Id);
            LoadData();
            if (IsDeleted)
            {
                Message = "студент удален";
                CurrentStudent = new Student();
                SelectedGroup = null;
            }
            else
                Message = "Ошибка удаления студент";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion
}