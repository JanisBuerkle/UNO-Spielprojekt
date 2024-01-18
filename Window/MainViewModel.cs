using System.Reflection.Metadata.Ecma335;
using UNO_Spielprojekt.AddPlayer;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Logging;
using UNO_Spielprojekt.MainMenu;
using UNO_Spielprojekt.Scoreboard;
using UNO_Spielprojekt.Setting;
using UNO_Spielprojekt.Winner;


namespace UNO_Spielprojekt.Window;

public class MainViewModel : ViewModelBase
{
    private bool _mainMenuVisible;
    private bool _scoreboardVisible;
    private bool _gameVisible;
    private bool _winnerVisible;
    private bool _settingsVisible;
    private bool _addPlayerVisible;
    private bool _rulesVisible;

    public GameData GameData { get; set; }
    public AddPlayerViewModel AddPlayerViewModel { get; set; }
    public MainMenuViewModel MainMenuViewModel { get; set; }
    public RulesViewModel RulesViewModel { get; set; }
    public GameViewModel GameViewModel { get; set; }
    private GameLogic GameLogic { get; set; }
    private PlayViewModel PlayViewModel { get; set; }
    public SettingsViewModel SettingsViewModel { get; }
    public ScoreboardViewModel ScoreboardViewModel { get; }
    public WinnerViewModel WinnerViewModel { get; }


    public MainViewModel()
    {
        var loggerFactory = new SerilogLoggerFactory();
        var logger = loggerFactory.CreateLogger("Uno-Spielprojekt");
        
        PlayViewModel = new PlayViewModel();
        WinnerViewModel = new WinnerViewModel(this);
        GameLogic = new GameLogic(PlayViewModel, logger);
        ScoreboardViewModel = new ScoreboardViewModel(this, logger);
        GameViewModel = new GameViewModel(this, PlayViewModel, GameLogic, logger, WinnerViewModel, ScoreboardViewModel);
        RulesViewModel = new RulesViewModel(this, logger);
        GameData = new GameData(ScoreboardViewModel, GameViewModel);
        SettingsViewModel = new SettingsViewModel(this, logger);
        MainMenuViewModel = new MainMenuViewModel(this, logger, GameData);
        AddPlayerViewModel = new AddPlayerViewModel(this, GameLogic, logger);
        MainMenuVisible = true;
    }
    
    public void GoToMainMenu()
    {
        MainMenuVisible = true;
        GameVisible = false;
        RulesVisible = false;
        WinnerVisible = false;
        SettingsVisible = false;
        AddPlayerVisible = false;
        ScoreboardVisible = false;
    }

    public void GoToAddPlayer()
    {
        AddPlayerVisible = true;
        GameVisible = false;
        RulesVisible = false;
        WinnerVisible = false;
        MainMenuVisible = false;
        SettingsVisible = false;
        ScoreboardVisible = false;
    }

    public void GoToSettings()
    {
        SettingsVisible = true;
        GameVisible = false;
        RulesVisible = false;
        WinnerVisible = false;
        MainMenuVisible = false;
        AddPlayerVisible = false;
        ScoreboardVisible = false;
    }

    public void GoToGame()
    {
        GameVisible = true;
        RulesVisible = false;
        WinnerVisible = false;
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
        WinnerVisible = false;
        MainMenuVisible = false;
        SettingsVisible = false;
        AddPlayerVisible = false;
        GameData.Load();
    }

    public void GoToRules()
    {
        RulesVisible = true;
        GameVisible = false;
        WinnerVisible = false;
        MainMenuVisible = false;
        SettingsVisible = false;
        AddPlayerVisible = false;
        ScoreboardVisible = false;
    }

    public void GoToWinner()
    {
        WinnerVisible = true;
        GameVisible = false;
        RulesVisible = false;
        SettingsVisible = false;
        MainMenuVisible = false;
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

    public bool WinnerVisible
    {
        get => _winnerVisible;
        set
        {
            if (value == _winnerVisible) return;
            _winnerVisible = value;
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