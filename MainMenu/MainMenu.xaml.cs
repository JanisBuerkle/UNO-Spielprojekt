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
            SetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture);
        }


        private void SetLanguage(System.Globalization.CultureInfo culture)
        {
            LocalizationManager.SetLanguage(culture);
            StartButton.Content = GlobalLocalization.PlayButton;
            ScoreboardButton.Content = GlobalLocalization.ScoreboardButton;
            ExitButton.Content = GlobalLocalization.ExitButton;
        }



        private void StartButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddPlayer.AddPlayer());
        }

        private void ScoreboardButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Scoreboard.Scoreboard());
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(100);
            Application.Current.Shutdown();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Setting.Settings());
        }
    }
}