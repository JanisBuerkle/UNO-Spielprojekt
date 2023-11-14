using System.Collections.Generic;

namespace UNO_Spielprojekt.Setting;

public class Languages
{
    public List<Language> MyLangs { get; set; } = GetLangs();

    public static List<Language> GetLangs()
    {
        var list = new List<Language>()
        {
            new Language() { CultureName = "en-US" , Flag="C:\\Users\\bks\\RiderProjects\\UNO-Spielprojekt\\Assets\\english (2).png", LangName="English"},
            new Language() { CultureName = "de-DE", Flag="C:\\Users\\bks\\RiderProjects\\UNO-Spielprojekt\\Assets\\germany.png", LangName="Deutsch"},

        };
        return list;
    }
    
}