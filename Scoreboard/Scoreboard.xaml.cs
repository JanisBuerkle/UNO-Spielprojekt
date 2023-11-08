using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.MainMenu;

namespace UNO_Spielprojekt.Scoreboard;

public partial class Scoreboard : Page
{
    public Scoreboard()
    {
        InitializeComponent();
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new MainMenu.MainMenu()); 
    }
}