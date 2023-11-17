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
    private GameLogic _gameLogic = new GameLogic();

    public GameView()
    {
        InitializeComponent();
        DataContext = new GameLogic();
        GameLogic.prop.CountOfPlayers = _gameLogic.PlayerCount();
        GameLogic.prop.StartingPlayer = _gameLogic.ChooseStartingPlayer();
        _gameLogic.GenerateDeck();
        _gameLogic.ShuffleDeck();

        foreach (Propertys player in GameLogic.prop.props)
        {
            player.Hand = _gameLogic.DealCards(5);
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

        _gameLogic.PlaceFirstCardInCenter();
        ShowCardInCenter();
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
            GameLogic.prop.ClickedCard = clickedButton.Content;
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
        if (_chosenCard)
        {
            int indexOfSelectedCard = GameLogic.prop.props[GameLogic.prop.StartingPlayer].Hand.IndexOf(GameLogic.prop.ClickedCard.ToString());

            if (indexOfSelectedCard != -1)
            {
                GameLogic.prop.props[GameLogic.prop.StartingPlayer].Hand.RemoveAt(indexOfSelectedCard);
                GameLogic.prop.Center.Add(GameLogic.prop.ClickedCard.ToString());
                ShowCardInCenter();
                UpdatePlayerHand();
            }
        }
    }

    private void ShowCardInCenter()
    {
        string[] cardSplit = GameLogic.prop.Center[^1].Split();
        string color = cardSplit[0].ToLower();
        string value = cardSplit[1].ToLower();

        MiddleCard.Content = GameLogic.prop.Center[^1];
        MiddleCard.Width = 260;
        MiddleCard.Height = 400;
        MiddleCard.Margin = new Thickness(5);
        MiddleCard.Tag = "Card";

        if (color == "wild")
        {
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/wild/{color}.png")),
                Stretch = Stretch.Fill
            };

            MiddleCard.Background = imageBrush;
        }
        else
        {
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png")),
                Stretch = Stretch.Fill
            };

            MiddleCard.Background = imageBrush;
        }
        
    }

    private void UpdatePlayerHand()
    {
        // Durchgehe die vorhandenen Buttons im StackPanel und aktualisiere ihre Inhalte
        foreach (Button button in StackPanel.Children.OfType<Button>())
        {
            Style buttonStyle = (Style)FindResource("CardStyle");
            string[] cardSplit = button.Content.ToString().Split();
            string color = cardSplit[0].ToLower();
            string value = cardSplit[1].ToLower();

            // Erstelle den aktualisierten Inhalt für den Button
            string updatedCard = $"{color} {value}";

            // Aktualisiere den Inhalt des Buttons
            button.Content = updatedCard;
            button.Style = buttonStyle;

            if (color == "wild")
            {
                // Code für Wild-Karte
            }
            else
            {
                // Code für normale Karte
            }
        }
    }
}
