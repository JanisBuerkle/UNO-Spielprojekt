using CommunityToolkit.Mvvm.Input;
using tt.Tools.Logging;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt;

public class RulesViewModel
{
    public RelayCommand GoToGameCommand { get; }
    private GameViewModel GameViewModel;
    private readonly ILogger _logger;

    public RulesViewModel(MainViewModel mainViewModel, ILogger logger)
    {
        _logger = logger;
        GameViewModel = mainViewModel.GameViewModel;
        GoToGameCommand = new RelayCommand(GoToGameMethod);
    }

    private void GoToGameMethod()
    {
        _logger.Info("Spiel wird gestartet.");
        GameViewModel.InitializeGame();
        GameViewModel.SetCurrentHand();
        GameViewModel.Game();
    }
}