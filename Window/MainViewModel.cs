using UNO_Spielprojekt.AddPlayer;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.MainMenu;
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
    private bool _rulesVisible;

    public AddPlayerViewModel AddPlayerViewModel { get; set; }
    public MainMenuViewModel MainMenuViewModel { get; set; }
    public RulesViewModel RulesViewModel { get; set; }
    public GameViewModel GameViewModel { get; set; }
    public SettingsViewModel SettingsViewModel { get; }
    public ScoreboardViewModel ScoreboardViewModel { get; }

    public MainViewModel()
    {
        AddPlayerViewModel = new AddPlayerViewModel(this);
        RulesViewModel = new RulesViewModel(this);
        ScoreboardViewModel = new ScoreboardViewModel(this);
        GameViewModel = new GameViewModel(this);
        SettingsViewModel = new SettingsViewModel(this);
        MainMenuViewModel = new MainMenuViewModel(this);
        MainMenuVisible = true;
    }

    public void GoToMainMenu()
    {
        MainMenuVisible = true;
        GameVisible = false;
        RulesVisible = false;
        SettingsVisible = false;
        AddPlayerVisible = false;
        ScoreboardVisible = false;
    }

    public void GoToAddPlayer()
    {
        AddPlayerVisible = true;
        GameVisible = false;
        RulesVisible = false;
        MainMenuVisible = false;
        SettingsVisible = false;
        ScoreboardVisible = false;
    }
    public void GoToSettings()
    {
        SettingsVisible = true;
        GameVisible = false;
        RulesVisible = false;
        MainMenuVisible = false;
        AddPlayerVisible = false;
        ScoreboardVisible = false;
    }
    public void GoToGame()
    {
        GameVisible = true;
        RulesVisible = false;
        MainMenuVisible = false;
        SettingsVisible = false;
        AddPlayerVisible = false;
        ScoreboardVisible = false;
    }
    public void GoToScoreboard()
    {
        ScoreboardVisible = true;
        GameVisible = false;
        RulesVisible = false;
        MainMenuVisible = false;
        SettingsVisible = false;
        AddPlayerVisible = false;
    }
    public void GoToRules()
    {
        RulesVisible = true;
        GameVisible = false;
        MainMenuVisible = false;
        SettingsVisible = false;
        AddPlayerVisible = false;
        ScoreboardVisible = false;
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
    
    public bool RulesVisible
    {
        get => _rulesVisible;
        set
        {
            if (value == _rulesVisible) return;
            _rulesVisible = value;
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