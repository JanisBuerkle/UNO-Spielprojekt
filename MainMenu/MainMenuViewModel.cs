using System.Windows;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.MainMenu;

public class MainMenuViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    public RelayCommand GoToAddPlayerCommand { get; }
    public RelayCommand GoToScoreboardCommand { get; }
    public RelayCommand GoToSettings { get; }
    public RelayCommand ExitApplicationCommand { get; }

    public MainMenuViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        GoToAddPlayerCommand = new RelayCommand(_mainViewModel.GoToAddPlayer);
        GoToScoreboardCommand = new RelayCommand(_mainViewModel.GoToScoreboard);
        GoToSettings = new RelayCommand(_mainViewModel.GoToSettings);
        ExitApplicationCommand = new RelayCommand(ExitApplication);
    }

    private void ExitApplication()
    {
        Application.Current.Shutdown();
    }
}