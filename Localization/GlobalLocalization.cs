using UNO_Spielprojekt.Setting;

namespace UNO_Spielprojekt.Localization;

public static class GlobalLocalization
{
    public static Language language => new();

    public static string PlayButton => LocalizationManager.GetLocalizedString("Play");
    public static string ScoreboardButton => LocalizationManager.GetLocalizedString("Scoreboard");
    public static string ExitButton => LocalizationManager.GetLocalizedString("Exit");
}