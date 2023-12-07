using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.AddPlayer;
using UNO_Spielprojekt.GamePage;

namespace UNO_Spielprojekt;

public partial class RulesView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(RulesViewModel), typeof(RulesView), new PropertyMetadata(default(RulesViewModel)));

    public RulesViewModel ViewModel
    {
        get => (RulesViewModel)GetValue(ViewModelProperty); 
        set => SetValue(ViewModelProperty, value); 
    }
    public RulesView()
    {
        InitializeComponent();
    }

    // private void RulesButtonClicked(object sender, RoutedEventArgs e)
    // {
    //     // NavigationService?.Navigate(new GameView());
    // }
}