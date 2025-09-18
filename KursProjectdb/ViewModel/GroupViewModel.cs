using System.Collections.ObjectModel;
using KursProjectdb.Model;
using KursProjectdb.Services;
using KursProjectdb.Utills;
using System.Linq;
using KursProjectdb.Styles;

namespace KursProjectdb.ViewModel;

public class GroupViewModel : ViewModelBase
{
    private GroupService groupService;
    private SpecializationService specService;

    #region DisplayOperation

    private ObservableCollection<Group> groupList;

    public ObservableCollection<Group> GroupList
    {
        get => groupList;
        set
        {
            groupList = value;
            OnPropertyChanged(nameof(GroupList));
        }
    }

    private List<Specialization> specList = new();

    public List<Specialization> SpecList
    {
        get => specList;
        set
        {
            specList = value;
            OnPropertyChanged(nameof(SpecList));
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
                CurrentGroup.SpecializationId = selectedSpecialization.Id;
            }
            OnPropertyChanged(nameof(SelectedSpecialization));
        }
    }

    private void LoadData()
    {
        GroupList = new ObservableCollection<Group>(groupService.GetAll());
        SpecList = specService.GetAll();
        
       
        if (CurrentGroup != null && CurrentGroup.SpecializationId > 0)
        {
            SelectedSpecialization = SpecList.FirstOrDefault(s => s.Id == CurrentGroup.SpecializationId);
        }
    }

    #endregion

    private Group currentGroup;

    public Group CurrentGroup
    {
        get { return currentGroup; }
        set
        {
            currentGroup = value;
            OnPropertyChanged(nameof(CurrentGroup));
            
            if (currentGroup != null && SpecList != null)
            {
                SelectedSpecialization = SpecList.FirstOrDefault(s => s.Id == currentGroup.SpecializationId);
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

    public GroupViewModel()
    {
        groupService = new GroupService();
        specService = new SpecializationService();
        CurrentGroup = new Group();
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
            var IsSaved = groupService.Add(CurrentGroup);
            LoadData();
            if (IsSaved)
            {
                Message = "Группа сохранена";
                CurrentGroup = new Group();
                SelectedSpecialization = null;
            }
            else
                Message = "Ошибка сохранения группы";
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
            if (CurrentGroup.Id == 0)
            {
                Message = "Выберите группу для редактирования";
                return;
            }

            var IsUpdated = groupService.Update(CurrentGroup);
            LoadData();
            if (IsUpdated)
                Message = "Группа обновлена";
            else
                Message = "Ошибка обновления группы";
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
            if (CurrentGroup.Id == 0)
            {
                Message = "Выберите группу для удаления";
                return;
            }

            var IsDeleted = groupService.Delete(CurrentGroup.Id);
            LoadData();
            if (IsDeleted)
            {
                Message = "Группа удалена";
                CurrentGroup = new Group();
                SelectedSpecialization = null;
            }
            else
                Message = "Ошибка удаления группы";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    #endregion
}