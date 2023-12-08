using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt;

public class RulesViewModel
{
    private readonly MainViewModel _mainViewModel;
    public RelayCommand GoToGameCommand { get; }
    public GameViewModel GameViewModel;

    public RulesViewModel(MainViewModel mainViewModel, GameViewModel gameViewModel)
    {
        _mainViewModel = mainViewModel;
        GameViewModel = gameViewModel;
        GoToGameCommand = new RelayCommand(GoToGameMethod);
    }

    public void GoToGameMethod()
    {
        GameViewModel.InitializeGame();
        _mainViewModel.GoToGame();
    }
}