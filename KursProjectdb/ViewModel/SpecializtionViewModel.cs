using System.Collections.ObjectModel;
using KursProjectdb.Model;
using KursProjectdb.Services;
using KursProjectdb.Styles;
using KursProjectdb.Utills;
using System.Linq;

namespace KursProjectdb.ViewModel;

public class SpecializtionViewModel : ViewModelBase
{
    private SpecializationService specService;
    private FacultyeService facService;

    #region DisplayOperation

    private ObservableCollection<Specialization> specList;

    public ObservableCollection<Specialization> SpecList
    {
        get => specList;
        set
        {
            specList = value;
            OnPropertyChanged(nameof(SpecList));
        }
    }

    private List<Facultye> facList = new();

    public List<Facultye> FacList
    {
        get => facList;
        set
        {
            facList = value;
            OnPropertyChanged(nameof(FacList));
        }
    }

    private Facultye selectedFaculty;
    public Facultye SelectedFaculty
    {
        get { return selectedFaculty; }
        set
        {
            selectedFaculty = value;
            if (selectedFaculty != null)
            {
                CurrentSpecialization.FacultyId = selectedFaculty.Id;
            }
            OnPropertyChanged(nameof(SelectedFaculty));
        }
    }

    private void LoadData()
    {
        SpecList = new ObservableCollection<Specialization>(specService.GetAll());
        FacList = facService.GetAll();
        
       
        if (CurrentSpecialization != null && CurrentSpecialization.FacultyId > 0)
        {
            SelectedFaculty = FacList.FirstOrDefault(f => f.Id == CurrentSpecialization.FacultyId);
        }
    }

    #endregion

    private Specialization currentSpecialization;

    public Specialization CurrentSpecialization
    {
        get { return currentSpecialization; }
        set
        {
            currentSpecialization = value;
            OnPropertyChanged(nameof(CurrentSpecialization));
            
 
            if (currentSpecialization != null && FacList != null)
            {
                SelectedFaculty = FacList.FirstOrDefault(f => f.Id == currentSpecialization.FacultyId);
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

    public SpecializtionViewModel()
    {
        specService = new SpecializationService();
        facService = new FacultyeService();
        CurrentSpecialization = new Specialization();
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
            var IsSaved = specService.Add(CurrentSpecialization);
            LoadData();
            if (IsSaved)
            {
                Message = "Специальность сохранена";
                CurrentSpecialization = new Specialization();
                SelectedFaculty = null;
            }
            else
                Message = "Ошибка сохранения специальности";
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
            if (CurrentSpecialization.Id == 0)
            {
                Message = "Выберите специальность для редактирования";
                return;
            }

            var IsUpdated = specService.Update(CurrentSpecialization);
            LoadData();
            if (IsUpdated)
                Message = "Специальность обновлена";
            else
                Message = "Ошибка обновления специальности";
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
            if (CurrentSpecialization.Id == 0)
            {
                Message = "Выберите специальность для удаления";
                return;
            }

            var IsDeleted = specService.Delete(CurrentSpecialization.Id);
            LoadData();
            if (IsDeleted)
            {
                Message = "Специальность удалена";
                CurrentSpecialization = new Specialization();
                SelectedFaculty = null;
            }
            else
                Message = "Ошибка удаления специальности";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion
}