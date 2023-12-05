using System.Globalization;
using System.Resources;
using UNO_Spielprojekt.Resources;

namespace UNO_Spielprojekt.Setting;

public class Language
{
    private static ResourceManager _resourceManager;

    static Language()
    {
        _resourceManager = new ResourceManager("UNO_Spielprojekt.Resources.Resource", typeof(Resource).Assembly);
    }

    public string? Flag { get; set; }
    public string? LangName { get; set; }
    public string? CultureName { get; set; }
    public string? LangString { get; set; }
    public CultureInfo LangCulture { get; set; }
}