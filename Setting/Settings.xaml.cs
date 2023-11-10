// In Settings.xaml.cs
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.Localization;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Setting
{
    public partial class Settings : Page
    {
        Language language = new Language();
        public Settings()
        {
            InitializeComponent();
            SetLanguage(new CultureInfo("en-US"));
        }

        private void SetLanguage(CultureInfo culture)
        {

            CultureInfo culture1 = new CultureInfo("en-US");
            
            LocalizationManager.SetLanguage(culture1);
            Header.Text = LocalizationManager.GetLocalizedString("Header");

            
            Console.WriteLine($"Header text set to: {Header.Text}");
        }

        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu.MainMenu());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (ComboBox.SelectedItem is Language selectedLanguage)
            {
                SetLanguage(new CultureInfo(selectedLanguage.CultureName));
            }
        }
    }
}