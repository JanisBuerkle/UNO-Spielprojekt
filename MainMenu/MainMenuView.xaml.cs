using System.Threading;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.AddPlayer;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Localization;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.MainMenu
{
    public partial class MainMenuView : Page
    {
        public MainMenuView()
        {
            InitializeComponent();
            SetLanguage(Thread.CurrentThread.CurrentUICulture);
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
            NavigationService?.Navigate(new AddPlayerView { ViewModel = new AddPlayerViewModel() });
        }

        private void ScoreboardButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Scoreboard.ScoreboardView());
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(100);
            Application.Current.Shutdown();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Setting.SettingsView(new MainWindowView()));
        }

        private void Skip_OnClick(object sender, RoutedEventArgs e)
        {
            GameLogic.prop.Players.Add(new Propertys() { PlayerName = "Hans" });
            GameLogic.prop.Players.Add(new Propertys() { PlayerName = "Peter" });
            NavigationService?.Navigate(new GameView());
        }
    }
}