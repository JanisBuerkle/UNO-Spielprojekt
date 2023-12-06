using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.MainMenu;
using WPF_Spielprojekt_Schwimmen.Scoreboard;

namespace UNO_Spielprojekt.Scoreboard;

public partial class ScoreboardView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(ScoreboardViewModel), typeof(ScoreboardView), new PropertyMetadata(default(ScoreboardViewModel)));

    public ScoreboardViewModel ViewModel
    {
        get => (ScoreboardViewModel)GetValue(ViewModelProperty); 
        set => SetValue(ViewModelProperty, value); 
    }
    public ScoreboardView()
    {
        InitializeComponent();
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        // NavigationService.Navigate(new MainMenu.MainMenuView()); 
    }
}