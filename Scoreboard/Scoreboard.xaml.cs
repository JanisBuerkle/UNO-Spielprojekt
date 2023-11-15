using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.MainMenu;
using WPF_Spielprojekt_Schwimmen.Scoreboard;

namespace UNO_Spielprojekt.Scoreboard;

public partial class Scoreboard : Page
{
    public Scoreboard()
    {
        InitializeComponent();
        ScoreboardViewModel viewModel = new ScoreboardViewModel();
        this.DataContext = viewModel;
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new MainMenu.MainMenu()); 
    }
}