using System.Globalization;
using System.Resources;
using UNO_Spielprojekt.Resources;

namespace UNO_Spielprojekt.Setting;

public class WindowMode
{
    private static ResourceManager _resourceManager;

    static WindowMode()
    {
        _resourceManager = new ResourceManager("UNO_Spielprojekt.Resources.Resource", typeof(Resource).Assembly);
    }
    public string? Symbol { get; set; }
    public string? Name { get; set; }
}