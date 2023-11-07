using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.MainMenu;

namespace UNO_Spielprojekt.Settings;

public partial class Settings : Page
{
    public Settings()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new MainMenu.MainMenu());
    }
}