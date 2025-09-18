using System.Collections.ObjectModel;
using KursProjectdb.Model;
using KursProjectdb.Services;
using KursProjectdb.Styles;
using KursProjectdb.Utills;

namespace KursProjectdb.ViewModel;

public class SubjectViewModel : ViewModelBase
{
    private SubjectService subService;

    #region DisplayOperation

    private ObservableCollection<Subject> subList;

    public ObservableCollection<Subject> SubList
    {
        get => subList;
        set
        {
            subList = value;
            OnPropertyChanged(nameof(SubList));
        }
    }

    private void LoadData()
    {
        SubList = new ObservableCollection<Subject>(subService.GetAll());
    }

    #endregion

    private Subject currentSubject;

    public Subject CurrentSubject
    {
        get { return currentSubject; }
        set
        {
            currentSubject = value;
            OnPropertyChanged(nameof(CurrentSubject));
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

    public SubjectViewModel()
    {
        subService = new SubjectService();
        LoadData();
        CurrentSubject = new Subject();
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
            var IsSaved = subService.Add(CurrentSubject);
            LoadData();
            if (IsSaved)
                Message = "Предмет сохранен";
            else
                Message = "Ошибка сохранения предмета";
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
            var IsUpdated = subService.Update(CurrentSubject);
            LoadData();
            if (IsUpdated)
                Message = "Предмет обновлен";
            else
                Message = "Ошибка обновления предмет";
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
            var IsDeleted = subService.Delete(CurrentSubject.Id);
            LoadData();
            if (IsDeleted)
                Message = "Предмет удален";
            else
                Message = "Ошибка удаления предмета";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion
}