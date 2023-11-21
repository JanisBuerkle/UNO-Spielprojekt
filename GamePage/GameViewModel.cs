using System.ComponentModel;

namespace UNO_Spielprojekt.GamePage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public class GameViewModel : ViewModelBase
{
    private bool _chosenCard;
    private GameLogic _gameLogic;

    public GameViewModel()
    { 
        _gameLogic = new GameLogic();
        GameLogic.prop.CountOfPlayers = _gameLogic.PlayerCount();
        GameLogic.prop.StartingPlayer = _gameLogic.ChooseStartingPlayer();
        _gameLogic.GenerateDeck();
        _gameLogic.ShuffleDeck();

        foreach (Propertys player in GameLogic.prop.Players)
        {
            player.Hand = _gameLogic.DealCards(5);
        }

        List<Button> buttons = new List<Button>();
        for (int i = 0; i < GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand.Count; i++)
        {
            Style buttonStyle = (Style)Application.Current.FindResource("CardStyle")!;
            string[] cardSplit = GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand[i].Split();
            string color = cardSplit[0].ToLower();
            string value = cardSplit[1].ToLower();
            Button button = new Button
            {
                Content = GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand[i],
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
            ItemTemplate = Application.Current.TryFindResource("ButtonTemplate") as DataTemplate,
            ItemsPanel = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(WrapPanel)))
        };

        ScrollViewer scrollViewer = new ScrollViewer
        {
            Content = itemsControl,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
        };

        StackiPanel.Children.Add(scrollViewer);

        _gameLogic.PlaceFirstCardInCenter();
        ShowCardInCenter();
    }

    private void Card_Clicked(object sender, RoutedEventArgs e)
    {
        string? tag = (sender as FrameworkElement)?.Tag as string;

        if (sender is Button clickedButton)
        {
            GameLogic.prop.ClickedCard = clickedButton.Content;
            if (tag == "Card")
            {
                foreach (Button button in _gameView.StackPanel.Children.OfType<ScrollViewer>()
                             .SelectMany(sv => ((ItemsControl)sv.Content).Items.OfType<Button>()))
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

    public void LegenButtonMethod()
    {
        if (_chosenCard)
        {
            int indexOfSelectedCard = GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand
                .IndexOf(GameLogic.prop.ClickedCard.ToString());

            if (indexOfSelectedCard != -1)
            {
                GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand.RemoveAt(indexOfSelectedCard);
                GameLogic.prop.Center.Add(GameLogic.prop.ClickedCard.ToString());
                ShowCardInCenter();
                UpdatePlayerHand();

                RemoveButtonFromStackPanel(indexOfSelectedCard);
            }
        }
    }



    private void ShowCardInCenter()
    {
        string[] cardSplit = GameLogic.prop.Center[^1].Split();
        string color = cardSplit[0].ToLower();
        string value = cardSplit[1].ToLower();
        
            _gameView.MiddleCard.Content = GameLogic.prop.Center[^1];
            _gameView.MiddleCard.Width = 260;
            _gameView.MiddleCard.Height = 400;
            _gameView.MiddleCard.Margin = new Thickness(5);
            _gameView.MiddleCard.Tag = "Card";

            if (color == "wild")
            {
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/wild/{color}.png")),
                    Stretch = Stretch.Fill
                };

                _gameView.MiddleCard.Background = imageBrush;
            }
            else
            {
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource =
                        new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png")),
                    Stretch = Stretch.Fill
                };

                _gameView.MiddleCard.Background = imageBrush;
            }
    }

    private void UpdatePlayerHand()
    {
        foreach (Button button in _gameView.StackPanel.Children.OfType<Button>())
        {
            Style buttonStyle = (Style)Application.Current.FindResource("CardStyle");
            string[] cardSplit = button.Content.ToString().Split();
            string color = cardSplit[0].ToLower();
            string value = cardSplit[1].ToLower();

            string updatedCard = $"{color} {value}";

            button.Content = updatedCard;
            button.Style = buttonStyle;

            if (color == "wild")
            {
            }
            else
            {
            }
        }
    }
}