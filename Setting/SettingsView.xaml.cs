using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.Localization;

namespace UNO_Spielprojekt.Setting
{
    public partial class SettingsView : Page
    {
        public static  Language language = new Language();
        public SettingsView()
        {
            InitializeComponent();
            SetLanguage();
        }

        private void SetLanguage()
        {
            if (language.LangString == null)
            {
                System.Globalization.CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
                language.LangCulture = culture;
            }
            else
            {
                CultureInfo culture = new CultureInfo(language.LangString);
                language.LangCulture = culture;
            }
            
            
            Header.Text = LocalizationManager.GetLocalizedString("Header");
            Console.WriteLine($@"Header text: {Header.Text}");
        }


        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenu.MainMenuView());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (ComboBox.SelectedItem is Language selectedLanguage)
            {
                language.LangString = selectedLanguage.CultureName;
                SetLanguage();
            }
        }
    }
}