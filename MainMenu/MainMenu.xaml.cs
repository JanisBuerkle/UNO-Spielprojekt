using System.Threading;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.Localization;

namespace UNO_Spielprojekt.MainMenu
{
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            SetLanguage(new System.Globalization.CultureInfo("en-US"));
        }

        private void SetLanguage(System.Globalization.CultureInfo culture)
        {
            LocalizationManager.SetLanguage(culture);
            StartButton.Content = LocalizationManager.GetLocalizedString("Play");
            ScoreboardButton.Content = LocalizationManager.GetLocalizedString("Scoreboard");
            ExitButton.Content = LocalizationManager.GetLocalizedString("Exit");
        }

        private void StartButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }

        private void ScoreboardButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Scoreboard.Scoreboard());
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(100);
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Setting.Settings());
        }
    }
}