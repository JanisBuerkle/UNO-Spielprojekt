using System.Windows;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Window;
using tt.Tools.Logging;

namespace UNO_Spielprojekt.MainMenu;

public class MainMenuViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly ILogger logger;
    public RelayCommand GoToAddPlayerCommand { get; }
    public RelayCommand GoToScoreboardCommand { get; }
    public RelayCommand GoToSettings { get; }
    public RelayCommand ExitApplicationCommand { get; }

    public MainMenuViewModel(MainViewModel mainViewModel, ILogger logger)
    {
        _mainViewModel = mainViewModel;
        this.logger = logger;
        GoToAddPlayerCommand = new RelayCommand(GoToAddPlayerCommandMethod);
        GoToScoreboardCommand = new RelayCommand(GoToScoreboardCommandMethod);
        GoToSettings = new RelayCommand(GoToSettingsCommandMethod);
        ExitApplicationCommand = new RelayCommand(ExitApplication);
    }

    private void GoToAddPlayerCommandMethod()
    {
        logger.Info("AddPlayer Seite wurde geöffnet.");
        _mainViewModel.GoToAddPlayer();
    }
    private void GoToScoreboardCommandMethod()
    {
        logger.Info("Scoreboard Seite wurde geöffnet.");
        _mainViewModel.GoToScoreboard();
    }
    private void GoToSettingsCommandMethod()
    {
        logger.Info("Settings Seite wurde geöffnet.");
        _mainViewModel.GoToSettings();
    }
    private void ExitApplication()
    {
        logger.Info("Program wird mit dem 'Exit' Button beendet.");
        Application.Current.Shutdown();
    }
}