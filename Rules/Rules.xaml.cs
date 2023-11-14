using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.AddPlayer;
using UNO_Spielprojekt.GamePage;

namespace UNO_Spielprojekt;

public partial class Rules : Page
{
    private PlayerData playerData;


    public Rules(PlayerData playerData)
    {
        InitializeComponent();
        this.playerData = playerData;
        int playerCount = playerData!.PlayerName.Count;
        switch (playerCount)
        {
            case 1:
                TextBlock.Text = "Willkommen bei UNO " + playerData.PlayerName[0];
                break;
            case 2:
                TextBlock.Text = "Willkommen bei UNO " + playerData.PlayerName[0] + " & " + playerData.PlayerName[1];
                break;
            case 3:
                TextBlock.Text = "Willkommen bei UNO " + playerData.PlayerName[0] + ", " + playerData.PlayerName[1] +
                                 " & " +
                                 playerData.PlayerName[2];
                break;
            case 4:
                TextBlock.Text = "Willkommen bei UNO " + playerData.PlayerName[0] + ", " + playerData.PlayerName[1] +
                                 ", " +
                                 playerData.PlayerName[2] + " & " + playerData.PlayerName[3];
                break;
            case 5:
                TextBlock.Text = "Willkommen bei UNO " + playerData.PlayerName[0] + ", " + playerData.PlayerName[1] +
                                 ", " +
                                 playerData.PlayerName[2] + ", " + playerData.PlayerName[3] + " & " +
                                 playerData.PlayerName[4];
                break;
        }
    }

    private void RulesButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new Game());
    }
}