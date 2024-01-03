using System.Collections.Generic;

namespace UNO_Spielprojekt.Setting;

public class ThemeModes
{
    public List<ThemeMode> MyThemeModes { get; }

    public ThemeModes()
    {
        MyThemeModes = new List<ThemeMode>
        {
            ThemeMode.Dark,
            ThemeMode.Bright
        };
    }
}