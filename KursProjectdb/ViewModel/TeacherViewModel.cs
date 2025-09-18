using System.Collections.ObjectModel;
using KursProjectdb.Model;
using KursProjectdb.Services;
using KursProjectdb.Styles;
using KursProjectdb.Utills;

namespace KursProjectdb.ViewModel;

public class TeacherViewModel : ViewModelBase
{
    private TeacherService teachService;

    #region DisplayOperation

    private ObservableCollection<Teacher> teachList;

    public ObservableCollection<Teacher> TeachList
    {
        get => teachList;
        set
        {
            teachList = value;
            OnPropertyChanged(nameof(TeachList));
        }
    }

    private void LoadData()
    {
        TeachList = new ObservableCollection<Teacher>(teachService.GetAll());
    }

    #endregion

    private Teacher currentTeacher;

    public Teacher CurrentTeacher
    {
        get { return currentTeacher; }
        set
        {
            currentTeacher = value;
            OnPropertyChanged(nameof(CurrentTeacher));
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

    public TeacherViewModel()
    {
        teachService = new TeacherService();
        LoadData();
        CurrentTeacher = new Teacher();
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
            var IsSaved = teachService.Add(CurrentTeacher);
            LoadData();
            if (IsSaved)
                Message = "преподователь сохранен";
            else
                Message = "Ошибка сохранения преподователя";
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
            var IsUpdated = teachService.Update(CurrentTeacher);
            LoadData();
            if (IsUpdated)
                Message = "преподователь обновлен";
            else
                Message = "Ошибка обновления преподователя";
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
            var IsDeleted = teachService.Delete(CurrentTeacher.Id);
            LoadData();
            if (IsDeleted)
                Message = "преподователь удален";
            else
                Message = "Ошибка удаления преподователя";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion
}