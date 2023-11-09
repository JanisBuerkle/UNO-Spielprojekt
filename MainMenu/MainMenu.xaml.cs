using System.Threading;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.AddPlayer;

namespace UNO_Spielprojekt.MainMenu;

public partial class MainMenu : Page
{
    public MainMenu()
    {
        InitializeComponent(); 
    }

    private void StartButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AddPlayer.AddPlayer());
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