using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

namespace UNO_Spielprojekt.Setting;

public class SettingsViewModel : ViewModelBase
{
    public List<Language> MyLangs { get; }
    public List<WindowMode> MyModes { get; }
    public RelayCommand GoToMainMenuCommand { get; }

    public SettingsViewModel(Action command)
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

        GoToMainMenuCommand = new RelayCommand(command);
    }
}