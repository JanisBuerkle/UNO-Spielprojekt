using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt;

public class RulesViewModel
{
    private readonly MainViewModel _mainViewModel;
    public RelayCommand GoToGameCommand { get; }

    public RulesViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        GoToGameCommand = new RelayCommand(_mainViewModel.GoToGame);
    }
}