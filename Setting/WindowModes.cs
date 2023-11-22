using System.Collections.Generic;

namespace UNO_Spielprojekt.Setting;

public class WindowModes
{
    public List<WindowMode> MyModes { get; set; } = GetModes();

    public static List<WindowMode> GetModes()
    {
        var list = new List<WindowMode>()
        {
            new WindowMode() { Symbol="C:\\Users\\bks\\RiderProjects\\UNO-Spielprojekt\\Assets\\Symbols\\fullscreen.png", Name= "Fullscreen"},
            new WindowMode() { Symbol="C:\\Users\\bks\\RiderProjects\\UNO-Spielprojekt\\Assets\\Symbols\\windowed.png", Name= "Windowed"},
        };
        return list;
    }
}