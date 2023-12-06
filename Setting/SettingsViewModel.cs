using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Setting;

public class SettingsViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    public List<Language> MyLangs { get; }
    public List<WindowMode> MyModes { get; }
    public RelayCommand GoToMainMenuCommand { get; }

    public SettingsViewModel(Action goToMainMenuCommand)
    {
        MyLangs = new List<Language>()
        {
            new Language()
            {
                CultureName = "en-US", Flag = "pack://application:,,,/Assets/Languages/english.png",
                LangName = "English"
            },
            new Language()
            {
                CultureName = "de-DE", Flag = "pack://application:,,,/Assets/Languages/germany.png",
                LangName = "Deutsch"
            },
        };

        MyModes = new List<WindowMode>()
        {
            WindowMode.FullScreen,
            WindowMode.Windowed
        };

        GoToMainMenuCommand = new RelayCommand(goToMainMenuCommand);
    }

    public SettingsViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        GoToMainMenuCommand = new RelayCommand(mainViewModel.GoToMainMenu);
    }
    
}