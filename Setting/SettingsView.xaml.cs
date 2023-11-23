using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.Localization;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Setting
{
    public partial class SettingsView : Page
    {
        public static Language language = new Language();
        private MainWindowView _mainWindow;

        public SettingsView(MainWindowView mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            SetLanguage();
        }

        private void SetLanguage()
        {
            if (language.LangString == null)
            {
                CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
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
                LanguageChangeSnackBar.Title = "Erfolg:";
                LanguageChangeSnackBar.Message = "Du hast erfolgreich die Sprache geändert!";
                LanguageChangeSnackBar.Show();
            }
        }

        private void ScreenModus_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ScreenModus.SelectedItem is WindowMode selectedMode)
            {
                string selectedValue = selectedMode.Name;

                if (selectedValue == "Fullscreen")
                {
                    // WindowWidth = 300;
                    // WindowHeight = 200;
                    Console.Write("Fullscreen");
                    
                }
                else if (selectedValue == "Windowed")
                {
                    WindowWidth = 500;
                    WindowHeight = 300;
                    Console.Write("Windowed");
                }
            }
        }



    }
}