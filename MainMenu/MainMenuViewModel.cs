using UNO_Spielprojekt.Setting;

namespace UNO_Spielprojekt.MainMenu;

public class MainMenuViewModel : ViewModelBase
{
    private SettingsViewModel SettingsViewModel;

    public MainMenuViewModel(SettingsViewModel settingsViewModel)
    {
        SettingsViewModel = settingsViewModel;
    }
}