using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.AddPlayer;
using UNO_Spielprojekt.GamePage;

namespace UNO_Spielprojekt;

public partial class RulesView : Page
{
    public RulesView(PlayerData playerData)
    {
        InitializeComponent();
    }

    private void RulesButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new GameView());
    }
}