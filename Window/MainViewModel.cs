using UNO_Spielprojekt.AddPlayer;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.MainMenu;
using UNO_Spielprojekt.Setting;

namespace UNO_Spielprojekt.Window;

public class MainViewModel : ViewModelBase
{
    private bool _mainMenuVisible;
    private bool _gameVisible;
    private bool _settingsVisible;
    private bool _addPlayerVisible;
    
    public AddPlayerViewModel AddPlayerViewModel { get; set; }
    public MainMenuViewModel MainMenuViewModel { get; set; }
    public GameViewModel GameViewModel { get; set; }
    public SettingsViewModel SettingsViewModel { get; }
    
    public MainViewModel()
    {
        AddPlayerViewModel = new AddPlayerViewModel();
        GameViewModel = new GameViewModel();
        SettingsViewModel = new SettingsViewModel(this);
        MainMenuViewModel = new MainMenuViewModel(this);
        SettingsVisible = true; 
    }

    public void GoToMainMenu()
    {
        SettingsVisible = false;
        GameVisible = false;
        AddPlayerVisible = false;
        MainMenuVisible = true;
    }
    public void GoToAddPlayer()
    {
        SettingsVisible = false;
        GameVisible = false;
        AddPlayerVisible = true;
        MainMenuVisible = false;
    }
    
    public bool MainMenuVisible 
    {
        get => _mainMenuVisible;
        set
        {
            if (value == _mainMenuVisible) return; 
            _mainMenuVisible = value;
            OnPropertyChanged();
        }
    }

    public bool GameVisible
    {
        get => _gameVisible;
        set
        {
            if (value == _gameVisible) return;
            _gameVisible = value;
            OnPropertyChanged();
        }
    }

    public bool SettingsVisible
    {
        get => _settingsVisible;
        set
        {
            if (value == _settingsVisible) return;
            _settingsVisible = value;
            OnPropertyChanged();
        }
    } 
    public bool AddPlayerVisible
    {
        get => _addPlayerVisible;
        set
        {
            if (value == _addPlayerVisible) return;
            _addPlayerVisible = value;
            OnPropertyChanged();
        }
    } 
}