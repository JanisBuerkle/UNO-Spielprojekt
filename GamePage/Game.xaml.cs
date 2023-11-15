using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UNO_Spielprojekt.GamePage;

public partial class Game
{
    private bool _chosenCard;

    public Game()
    {
        InitializeComponent();
        GameLogic.prop.CountOfPlayers = GameLogic.PlayerCount();
        GameLogic.prop.StartingPlayer = GameLogic.ChooseStartingPlayer();
        GameLogic.GenerateDeck();
        GameLogic.ShuffleDeck();
        foreach (Propertys player in GameLogic.prop.props)
        {
            player.Hand = GameLogic.DealCards(7);
        }
        // <Button Tag="Card" Name="RedZero" Height="400" Width="260" HorizontalAlignment="Left" VerticalAlignment="Top" Style="{StaticResource CardStyle}" Click="RedZero_Click">
        //     <Button.Background>
        //     <ImageBrush ImageSource="C:\Users\bks\RiderProjects\UNO-Spielprojekt\Assets\Red0.png" Stretch="Fill">
        //     </ImageBrush>
        //     </Button.Background>
        //     </Button>
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new MainMenu.MainMenu());
    }

    private void RedZero_Click(object sender, RoutedEventArgs e)
    {
        string? tag = (sender as FrameworkElement)?.Tag as string;
        if (sender is Button clickedButton)
        {
            if (tag == "Card")
            {
                foreach (Button button in StackPanel.Children.OfType<Button>().Where(b => (b.Tag as string) == tag))
                {
                    button.Margin = new Thickness(5);
                }
            }

            if (clickedButton.Margin.Top == 5)
            {
                _chosenCard = true;
                Thickness newMargin = clickedButton.Margin;
                newMargin.Top -= 100;
                clickedButton.Margin = newMargin;
            }
        }
    }

    private void LegenButton(object sender, RoutedEventArgs e)
    {
        Console.WriteLine(_chosenCard ? "Du hast eine Karte gelegt!" : "Du hast keine Karte ausgewählt!");
    }
}