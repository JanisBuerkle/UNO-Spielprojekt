using System.Collections.Generic;

namespace UNO_Spielprojekt.Setting;

public class WindowModes
{
    public List<WindowMode> MyModes { get; }

    public WindowModes()
    {
        MyModes = new List<WindowMode>()
        {
            WindowMode.FullScreen,
            WindowMode.Windowed
            // new WindowMode() { Symbol="pack://application:,,,/Assets/fullscreen.png", Name= "Fullscreen"},
            // new WindowMode() { Symbol="pack://application:,,,/Assets/Symbols/windowed.png", Name= "Windowed"},
        };
    }
}