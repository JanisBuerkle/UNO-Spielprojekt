using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.Input;

namespace UNO_Spielprojekt.GamePage
{
    public class GameViewModel : ViewModelBase
    {
        private string _middleCard;
        public string MiddleCard
        {
            get { return _middleCard; }
            set
            {
                if (_middleCard != value)
                {
                    _middleCard = value;
                    OnPropertyChanged(nameof(MiddleCard));
                }
            }
        }

        private bool _chosenCard;
        private GameLogic _gameLogic;
        private ObservableCollection<string> buttonTexts = new ObservableCollection<string> { "Button 1", "Button 2", "Button 3" };
        public StackPanel stackPanell { get; set; } = new StackPanel();
        public RelayCommand LegenCommand { get; }

        public GameViewModel()
        {
            InitializeGame();
            MiddleCard = GameLogic.prop.Center.FirstOrDefault();
            LegenCommand = new RelayCommand(LegenButtonMethod);
        }

        private void InitializeGame()
        {
            _gameLogic = new GameLogic();
            InitializeGameProperties();
            InitializePlayersHands();
            InitializeUI();
            _gameLogic.PlaceFirstCardInCenter();
            
        }

        private void InitializeGameProperties()
        {
            GameLogic.prop.CountOfPlayers = _gameLogic.PlayerCount();
            GameLogic.prop.StartingPlayer = _gameLogic.ChooseStartingPlayer();
            _gameLogic.GenerateDeck();
            _gameLogic.ShuffleDeck();
        }

        private void InitializePlayersHands()
        {
            foreach (Propertys player in GameLogic.prop.Players)
            {
                player.Hand = _gameLogic.DealCards(5);
            }
        }

        private void InitializeUI()
        {
            // List<Button> buttons = CreateButtonsForPlayerHand();
            // DisplayPlayerHand(buttons);
        }

        // public List<Button> CreateButtonsForPlayerHand()
        // {
        //     List<Button> buttons = new List<Button>();
        //
        //     foreach (var card in GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand)
        //     {
        //         Button button = CreateCardButton(card);
        //         buttons.Add(button);
        //     }
        //     return buttons;
        // }

        private Button CreateCardButton(string card)
        {
            Style buttonStyle = (Style)Application.Current.FindResource("CardStyle")!;
            string[] cardSplit = card.Split();
            string color = cardSplit[0].ToLower();
            string value = cardSplit[1].ToLower();

            Button button = new Button
            {
                Content = card,
                Width = 260,
                Height = 400,
                Margin = new Thickness(5),
                Tag = "Card",
                Style = buttonStyle
            };

            SetButtonBackground(button, color, value);

            button.Click += Card_Clicked;
            return button;
        }

        private void SetButtonBackground(Button button, string color, string value)
        {
            ImageBrush imageBrush = new ImageBrush();
            
            if (color == "wild")
            {
                imageBrush.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/wild/wild.png"));
            }
            else
            {
                imageBrush.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png"));
            }

            imageBrush.Stretch = Stretch.Fill;
            button.Background = imageBrush;
        }

        private void DisplayPlayerHand(List<Button> buttons)
        {
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

            stackPanell.Children.Add(scrollViewer);
        }

        private void Card_Clicked(object sender, RoutedEventArgs e)
        {
            string? tag = (sender as FrameworkElement)?.Tag as string;
            
            if (sender is Button clickedButton)
            {
                HandleClickedCard(clickedButton, tag);
            }
        }

        private void HandleClickedCard(Button clickedButton, string? tag)
        {
            GameLogic.prop.ClickedCard = clickedButton.Content;

            if (tag == "Card")
            {
                ResetButtonMargins();
            }
            if (clickedButton.Margin.Top == 5)
            {
                _chosenCard = true;
                MoveCardUp(clickedButton);
            }
        }

        private void ResetButtonMargins()
        {
            foreach (Button button in stackPanell.Children.OfType<ScrollViewer>()
                .SelectMany(sv => ((ItemsControl)sv.Content).Items.OfType<Button>()))
            {
                if ((button.Tag as string) == "Card")
                {
                    button.Margin = new Thickness(5);
                }
            }
        }

        private void MoveCardUp(Button clickedButton)
        {
            Thickness newMargin = clickedButton.Margin; 
            newMargin.Top -= 100;
            clickedButton.Margin = newMargin;
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
                    // ShowCardInCenter();
                    UpdatePlayerHand();
                    RemoveButtonFromStackPanel(indexOfSelectedCard);
                }
            }
        }

        public void RemoveButtonFromStackPanel(int index)
        {
            if (index >= 0 && stackPanell.Children.Count > index)
            {
                stackPanell.Children.RemoveAt(index);
            }
        }

        // private void ShowCardInCenter()
        // {
        //     string[] cardSplit = GameLogic.prop.Center[^1].Split();
        //     string color = cardSplit[0].ToLower();
        //     string value = cardSplit[1].ToLower();
        //
        //     SetMiddleCardProperties(color, value);
        //     MiddleCard = GameLogic.prop.Center[^1];
        // }
        //
        // private void SetMiddleCardProperties(string color, string value)
        // {
        //     MiddleCard.Content = GameLogic.prop.Center[^1];
        //     MiddleCard.Width = 260;
        //     MiddleCard.Height = 400;
        //     MiddleCard.Margin = new Thickness(5);
        //     MiddleCard.Tag = "Card";
        //
        //     SetMiddleCardBackground(color, value);
        // }
        //
        // private void SetMiddleCardBackground(string color, string value)
        // {
        //     if (color == "wild")
        //     {
        //         ImageBrush imageBrush = new ImageBrush
        //         {
        //             ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/wild/{color}.png")),
        //             Stretch = Stretch.Fill
        //         };
        //
        //         MiddleCard.Background = imageBrush;
        //     }
        //     else
        //     {
        //         ImageBrush imageBrush = new ImageBrush
        //         {
        //             ImageSource =
        //                 new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png")),
        //             Stretch = Stretch.Fill
        //         };
        //
        //         MiddleCard.Background = imageBrush;
        //     }
        // }

        private void UpdatePlayerHand()
        {
            foreach (Button button in stackPanell.Children.OfType<Button>())
            {
                Style buttonStyle = (Style)Application.Current.FindResource("CardStyle");
                string[] cardSplit = button.Content.ToString().Split();
                string color = cardSplit[0].ToLower();
                string value = cardSplit[1].ToLower();

                string updatedCard = $"{color} {value}";

                button.Content = updatedCard;
                button.Style = buttonStyle;
            }
        }
    }
}
