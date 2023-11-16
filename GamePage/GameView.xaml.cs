using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UNO_Spielprojekt.GamePage;

public partial class GameView
{
    private bool _chosenCard;

    public GameView()
    {
        InitializeComponent();
        GameLogic.prop.CountOfPlayers = GameLogic.PlayerCount();
        GameLogic.prop.StartingPlayer = GameLogic.ChooseStartingPlayer();
        GameLogic.GenerateDeck();
        GameLogic.ShuffleDeck();
        foreach (Propertys player in GameLogic.prop.props)
        {
            player.Hand = GameLogic.DealCards(50);
        }

        var resourceDictionary = new ResourceDictionary();
        resourceDictionary.Source = new Uri("/Styles/Styles.xaml", UriKind.Relative);
        Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

        List<Button> buttons = new List<Button>();
        for (int i = 0; i < GameLogic.prop.props[GameLogic.prop.StartingPlayer].Hand.Count; i++)
        {
            Style buttonStyle = (Style)FindResource("CardStyle");
            string[] cardSplit = GameLogic.prop.props[GameLogic.prop.StartingPlayer].Hand[i].Split();
            string color = cardSplit[0].ToLower();
            string value = cardSplit[1].ToLower();
            Button button = new Button
            {
                Content = GameLogic.prop.props[GameLogic.prop.StartingPlayer].Hand[i],
                Width = 260,
                Height = 400,

                Margin = new Thickness(5),
                Tag = "Card",
                Style = buttonStyle
            };

            if (color == "wild")
            {
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/wild/wild.png")),
                    Stretch = Stretch.Fill
                };
                button.Background = imageBrush;
            }
            else
            {
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png")),
                    Stretch = Stretch.Fill
                };
                button.Background = imageBrush;
            }

            button.Click += Card_Clicked;

            buttons.Add(button);
        }

        ItemsControl itemsControl = new ItemsControl
        {
            ItemsSource = buttons,
            ItemTemplate = FindResource("ButtonTemplate") as DataTemplate,
            ItemsPanel = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(WrapPanel)))
        };

        ScrollViewer scrollViewer = new ScrollViewer
        {
            Content = itemsControl,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
        };
        StackPanel.Children.Add(scrollViewer);
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new MainMenu.MainMenuView());
    }

    private void Card_Clicked(object sender, RoutedEventArgs e)
    {
        string? tag = (sender as FrameworkElement)?.Tag as string;

        if (sender is Button clickedButton)
        {
            GameLogic.prop.clickedCard = clickedButton.Content;
            if (tag == "Card")
            {
                foreach (Button button in StackPanel.Children.OfType<ScrollViewer>().SelectMany(sv => ((ItemsControl)sv.Content).Items.OfType<Button>()))
                {
                    if ((button.Tag as string) == "Card")
                    {
                        button.Margin = new Thickness(5);
                    }
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
        Console.WriteLine(_chosenCard ? "Du hast eine Karte gelegt: " + GameLogic.prop.clickedCard : "Du hast keine Karte ausgewählt!");
    }
}