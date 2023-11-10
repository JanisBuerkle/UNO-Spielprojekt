using System.Windows;
using UNO_Spielprojekt.Setting;

namespace UNO_Spielprojekt.Window
{
    public partial class App : Application
    {
        public App()
        {
            Language language = new Language();
            int x = 0; 
            if (x == 0)
            {
                language.Lang = "de-DE";
                x = x + 1;
            }ess gibt 
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language.Lang);

        }
    }
}