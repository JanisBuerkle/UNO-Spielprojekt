using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.Resources;

namespace UNO_Spielprojekt.Setting;

public partial class Settings : Page
{
    public string SettingsTitle { get; set; }
    public Settings()
    {
        InitializeComponent();
        Button.Content = Resource.Button;
        Label.Content = Resource.Label;
        Header.Text = Resource.Header;
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new MainMenu.MainMenu());
    }
    

}