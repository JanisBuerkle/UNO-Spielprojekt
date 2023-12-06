using System.Threading;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.Localization;

namespace UNO_Spielprojekt.MainMenu
{
    public partial class MainMenuView : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel), typeof(MainMenuViewModel), typeof(MainMenuView),
            new PropertyMetadata(default(MainMenuViewModel)));

        public MainMenuViewModel ViewModel
        {
            get => (MainMenuViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public MainMenuView()
        {
            InitializeComponent();
            SetLanguage(Thread.CurrentThread.CurrentUICulture);
        }

        private void SetLanguage(System.Globalization.CultureInfo culture)
        {
            LocalizationManager.SetLanguage(culture);
            ScoreboardButton.Content = GlobalLocalization.ScoreboardButton;
            ExitButton.Content = GlobalLocalization.ExitButton;
        }
        // private void StartButtonClicked(object sender, RoutedEventArgs e)
        // {
        //
        //     NavigationService?.Navigate(new AddPlayerView { ViewModel = new AddPlayerViewModel() });
        // }
        //
        // private void ScoreboardButtonClicked(object sender, RoutedEventArgs e)
        // {
        //     NavigationService?.Navigate(new Scoreboard.ScoreboardView());
        // }
        //
        // private void ExitButtonClicked(object sender, RoutedEventArgs e)
        // {
        //     Thread.Sleep(100);
        //     Application.Current.Shutdown();
        // }
        //
        // private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        // {
        //     NavigationService?.Navigate(new Setting.SettingsView(new MainWindowView()));
        // }
        //
        // private void Skip_OnClick(object sender, RoutedEventArgs e)
        // {
        //     GameLogic.prop.Players.Add(new Propertys() { PlayerName = "Hans" });
        //     GameLogic.prop.Players.Add(new Propertys() { PlayerName = "Peter" });
        //     NavigationService?.Navigate(new GameView());
        // }
    }
}