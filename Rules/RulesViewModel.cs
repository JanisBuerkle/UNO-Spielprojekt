using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt;

public class RulesViewModel
{
    public RelayCommand GoToGameCommand { get; }
    private GameViewModel GameViewModel;

    public RulesViewModel(MainViewModel mainViewModel, GameViewModel gameViewModel)
    {
        GameViewModel = gameViewModel;
        GoToGameCommand = new RelayCommand(GoToGameMethod);
    }

    private void GoToGameMethod()
    {
        GameViewModel.InitializeGame();
        GameViewModel.SetCurrentHand();
        GameViewModel.Game();
    }
}