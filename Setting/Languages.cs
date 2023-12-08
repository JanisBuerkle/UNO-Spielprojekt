using System.Collections.Generic;

namespace UNO_Spielprojekt.Setting;

public class Languages
{
    public List<Language> MyLangs { get; set; } = GetLangs();

    public static List<Language> GetLangs()
    {
        var list = new List<Language>
        {
            new()
            {
                CultureName = "en-US", Flag = "pack://application:,,,/Assets/Languages/english.png",
                LangName = "English"
            },
            new()
            {
                CultureName = "de-DE", Flag = "pack://application:,,,/Assets/Languages/germany.png",
                LangName = "Deutsch"
            }
        };
        return list;
    }
}