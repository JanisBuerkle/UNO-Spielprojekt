using System;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Setting;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.MainMenu;

public class MainMenuViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private SettingsViewModel SettingsViewModel;
    public RelayCommand GoToAddPlayerCommand { get; }

    public MainMenuViewModel(Action goToAddPlayerCommand)
    {
        GoToAddPlayerCommand = new RelayCommand(goToAddPlayerCommand);
    }

    public MainMenuViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        GoToAddPlayerCommand = new RelayCommand(mainViewModel.GoToAddPlayer);
    }
}