using System.Windows;

namespace UNO_Spielprojekt.Window
{
    public partial class App : Application
    {
        public App()
        {
            SetLanguage();
        }

        private static void SetLanguage()
        {
            // Startlanguage
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }
    }
}