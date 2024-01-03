using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Winner;

public class WinnerViewModel
{
    private readonly MainViewModel _mainViewModel;
    public GameViewModel _gameViewModel { get; }
    public RelayCommand GoToMainMenuCommand { get; }
    
    public WinnerViewModel(MainViewModel mainViewModel, GameViewModel gameViewModel)
    {
        _mainViewModel = mainViewModel;
        _gameViewModel = gameViewModel;
        GoToMainMenuCommand = new RelayCommand(mainViewModel.GoToMainMenu);
    }
}