using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.MainMenu;

namespace UNO_Spielprojekt.AddPlayer;

public partial class AddPlayer : Page
{
    public AddPlayer()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new MainMenu.MainMenu());
    }
}