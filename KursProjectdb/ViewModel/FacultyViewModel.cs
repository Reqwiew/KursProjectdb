using System.Collections.ObjectModel;
using KursProjectdb.Model;
using KursProjectdb.Services;
using KursProjectdb.Styles;
using KursProjectdb.Utills;

namespace KursProjectdb.ViewModel;

public class FacultyViewModel : ViewModelBase
{
    private FacultyeService facService;

    #region DisplayOperation

    private ObservableCollection<Facultye> facList;

    public ObservableCollection<Facultye> FacList
    {
        get => facList;
        set
        {
            facList = value;
            OnPropertyChanged(nameof(FacList));
        }
    }

    private void LoadData()
    {
        FacList = new ObservableCollection<Facultye>(facService.GetAll());
    }

    #endregion

    private Facultye currentFaculty;

    public Facultye CurrentFaculty
    {
        get { return currentFaculty; }
        set
        {
            currentFaculty = value;
            OnPropertyChanged(nameof(CurrentFaculty));
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

    public FacultyViewModel()
    {
        facService = new FacultyeService();
        LoadData();
        CurrentFaculty = new Facultye();
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
            var IsSaved = facService.Add(CurrentFaculty);
            LoadData();
            if (IsSaved)
                Message = "Факультет сохранен";
            else
                Message = "Ошибка сохранения факультета";
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
            var IsUpdated = facService.Update(CurrentFaculty);
            LoadData();
            if (IsUpdated)
                Message = "факультет обновлен";
            else
                Message = "Ошибка обновления факультета";
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
            var IsDeleted = facService.Delete(CurrentFaculty.Id);
            LoadData();
            if (IsDeleted)
                Message = "факультет удален";
            else
                Message = "Ошибка удаления факультета";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion
}