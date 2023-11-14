using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UNO_Spielprojekt.GamePage;

public partial class Game : Page
{
    public bool chosenCard;

    public Game()
    {
        InitializeComponent();
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new MainMenu.MainMenu());
    }

    private void RedZero_Click(object sender, RoutedEventArgs e)
    {

        string tag = (sender as FrameworkElement)?.Tag as string;

        if (tag == "Card")
        {
            foreach (Button button in StackPanel.Children.OfType<Button>().Where(b => (b.Tag as string) == tag))
            {
                button.Margin = new Thickness(5);
            }
        }

        if (sender is Button clickedButton)
        {
            if (clickedButton.Margin.Top == 5)
            {
                chosenCard = true;
                Thickness newMargin = clickedButton.Margin;
                newMargin.Top -= 100;
                clickedButton.Margin = newMargin;
            }
            else if (clickedButton.Margin.Top == 105)
            {
                chosenCard = false;
                Thickness newMargin = clickedButton.Margin;
                newMargin.Top += 100;
                clickedButton.Margin = newMargin;
            }
        }
    }


    private void LegenButton(object sender, RoutedEventArgs e)
    {
        if (chosenCard)
        {
            Console.WriteLine("Du hast eine Karte gelegt!");
        }
        else
        {
            Console.WriteLine("Du hast keine Karte ausgewählt!");
        }
    }
}