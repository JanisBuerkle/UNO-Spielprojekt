using UNO_Spielprojekt.AddPlayer;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.MainMenu;
using UNO_Spielprojekt.Scoreboard;
using UNO_Spielprojekt.Setting;
using WPF_Spielprojekt_Schwimmen.Scoreboard;

namespace UNO_Spielprojekt.Window;

public class MainViewModel : ViewModelBase
{
    private bool _mainMenuVisible;
    private bool _scoreboardVisible;
    private bool _gameVisible;
    private bool _settingsVisible;
    private bool _addPlayerVisible;

    public AddPlayerViewModel AddPlayerViewModel { get; set; }
    public MainMenuViewModel MainMenuViewModel { get; set; }
    public GameViewModel GameViewModel { get; set; }
    public SettingsViewModel SettingsViewModel { get; }
    public ScoreboardViewModel ScoreboardViewModel { get; }

    public MainViewModel()
    {
        AddPlayerViewModel = new AddPlayerViewModel(this);
        ScoreboardViewModel = new ScoreboardViewModel(this);
        GameViewModel = new GameViewModel();
        SettingsViewModel = new SettingsViewModel(this);
        MainMenuViewModel = new MainMenuViewModel(this);
        MainMenuVisible = true;
    }

    public void GoToMainMenu()
    {
        SettingsVisible = false;
        GameVisible = false;
        ScoreboardVisible = false;
        AddPlayerVisible = false;
        MainMenuVisible = true;
    }

    public void GoToAddPlayer()
    {
        SettingsVisible = false;
        GameVisible = false;
        ScoreboardVisible = false;
        AddPlayerVisible = true;
        MainMenuVisible = false;
    }
    public void GoToSettings()
    {
        SettingsVisible = true;
        GameVisible = false;
        ScoreboardVisible = false;
        AddPlayerVisible = false;
        MainMenuVisible = false;
    }
    public void GoToGame()
    {
        SettingsVisible = false;
        GameVisible = true;
        ScoreboardVisible = false;
        AddPlayerVisible = false;
        MainMenuVisible = false;
    }
    public void GoToScoreboard()
    {
        SettingsVisible = false;
        GameVisible = false;
        ScoreboardVisible = true;
        AddPlayerVisible = false;
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
    
    public bool ScoreboardVisible
    {
        get => _scoreboardVisible;
        set
        {
            if (value == _scoreboardVisible) return;
            _scoreboardVisible = value;
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