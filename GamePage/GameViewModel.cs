using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Window;
using Wpf.Ui.Controls;
using Button = System.Windows.Controls.Button;

namespace UNO_Spielprojekt.GamePage;

public class GameViewModel : ViewModelBase
{
    private string _middleCard;

    public string MiddleCard
    {
        get => _middleCard;
        set
        {
            if (_middleCard != value)
            {
                _middleCard = value;
                OnPropertyChanged();
            }
        }
    }

    private string _middleCardPic;

    public string MiddleCardPic
    {
        get => _middleCardPic;
        set
        {
            if (_middleCardPic != value)
            {
                _middleCardPic = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _chosenCard;
    private GameLogic _gameLogic;

    private ObservableCollection<string> _buttonTexts = new() { "Button 1", "Button 2", "Button 3" };

    private readonly MainViewModel MainViewModel;
    private StackPanel stackPanell { get; } = new();
    public PlayViewModel PlayViewModel;
    public CardViewModel CardViewModel { get; set; }
    public RelayCommand LegenCommand { get; }
    public RelayCommand ExitConfirmCommand { get; }

    public GameViewModel(MainViewModel mainViewModel, PlayViewModel playViewModel)
    {
        PlayViewModel = playViewModel;
        MainViewModel = mainViewModel;
        LegenCommand = new RelayCommand(LegenButtonMethod);
        ExitConfirmCommand = new RelayCommand(ExitConfirmCommandMethod);
    }

    private void ExitConfirmCommandMethod()
    {
        var exitConfirmWindow = new ExitConfirmWindow(MainViewModel);
        exitConfirmWindow.Owner = MainWindowView.Instance;
        exitConfirmWindow.ShowDialog();
    }

    public void InitializeGame()
    {
        _gameLogic = new GameLogic();
        InitializeGameProperties();
        InitializePlayersHands();
        _gameLogic.PlaceFirstCardInCenter();
        MiddleCard = GameLogic.prop.Center.FirstOrDefault();
        var middleCardSplitted = MiddleCard.Split();
        MiddleCardPic =
            $"pack://application:,,,/Assets/cards/{middleCardSplitted[1]}/{middleCardSplitted[0].ToLower()}.png";
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
        foreach (CardViewModel cards in _gameLogic.cards)
        {
            PlayViewModel.Cards.Add(cards);
            OnPropertyChanged(nameof(PlayViewModel.Cards));
        }
    }

    private void Test()
    {
        foreach (var test in GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand) Console.Write(test);
    }

    private Button CreateCardButton(string card)
    {
        var buttonStyle = (Style)Application.Current.FindResource("CardStyle")!;
        var cardSplit = card.Split();
        var color = cardSplit[0].ToLower();
        var value = cardSplit[1].ToLower();

        var button = new Button
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
        var imageBrush = new ImageBrush();

        if (color == "wild")
            imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/cards/wild/wild.png"));
        else
            imageBrush.ImageSource =
                new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png"));

        imageBrush.Stretch = Stretch.Fill;
        button.Background = imageBrush;
    }

    private void DisplayPlayerHand(List<Button> buttons)
    {
        var itemsControl = new ItemsControl
        {
            ItemsSource = buttons,
            ItemTemplate = Application.Current.TryFindResource("ButtonTemplate") as DataTemplate,
            ItemsPanel = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(WrapPanel)))
        };

        var scrollViewer = new ScrollViewer
        {
            Content = itemsControl,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
        };

        stackPanell.Children.Add(scrollViewer);
    }

    private void Card_Clicked(object sender, RoutedEventArgs e)
    {
        var tag = (sender as FrameworkElement)?.Tag as string;

        if (sender is Button clickedButton) HandleClickedCard(clickedButton, tag);
    }

    private void HandleClickedCard(Button clickedButton, string? tag)
    {
        GameLogic.prop.ClickedCard = clickedButton.Content;

        if (tag == "Card") ResetButtonMargins();

        if (clickedButton.Margin.Top == 5)
        {
            _chosenCard = true;
            MoveCardUp(clickedButton);
        }
    }

    private void ResetButtonMargins()
    {
        foreach (var button in stackPanell.Children.OfType<ScrollViewer>()
                     .SelectMany(sv => ((ItemsControl)sv.Content).Items.OfType<Button>()))
            if (button.Tag as string == "Card")
                button.Margin = new Thickness(5);
    }

    private void MoveCardUp(Button clickedButton)
    {
        var newMargin = clickedButton.Margin;
        newMargin.Top -= 100;
        clickedButton.Margin = newMargin;
    }

    private void LegenButtonMethod()
    {
        // if (_chosenCard)
        // {
        //     // var indexOfSelectedCard = GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand
        //     //     .IndexOf(GameLogic.prop.ClickedCard.ToString());
        //
        //     if (indexOfSelectedCard != -1)
        //     {
        //         GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand.RemoveAt(indexOfSelectedCard);
        //         GameLogic.prop.Center.Add(GameLogic.prop.ClickedCard.ToString());
        //         ShowCardInCenter();
        //         UpdatePlayerHand();
        //         RemoveButtonFromStackPanel(indexOfSelectedCard);
        //     }
        // }
    }

    private void RemoveButtonFromStackPanel(int index)
    {
        if (index >= 0 && stackPanell.Children.Count > index) stackPanell.Children.RemoveAt(index);
    }

    private void ShowCardInCenter()
    {
        var cardSplit = GameLogic.prop.Center[^1].Split();
        var color = cardSplit[0].ToLower();
        var value = cardSplit[1].ToLower();

        SetMiddleCardProperties(color, value);
        MiddleCard = GameLogic.prop.Center[^1];
    }

    private void UpdatePlayerHand()
    {
        foreach (var button in stackPanell.Children.OfType<Button>())
        {
            var buttonStyle = (Style)Application.Current.FindResource("CardStyle");
            var cardSplit = button.Content.ToString().Split();
            var color = cardSplit[0].ToLower();
            var value = cardSplit[1].ToLower();

            var updatedCard = $"{color} {value}";

            button.Content = updatedCard;
            button.Style = buttonStyle;
        }
    }

    private void SetMiddleCardProperties(string color, string value)
    {
        // MiddleCard.Content = GameLogic.prop.Center[^1];
        // MiddleCard.Width = 260;
        // MiddleCard.Height = 400;
        // MiddleCard.Margin = new Thickness(5);
        // MiddleCard.Tag = "Card";

        SetMiddleCardBackground(color, value);
    }

    private void SetMiddleCardBackground(string color, string value)
    {
        if (color == "wild")
        {
            var imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/wild/{color}.png")),
                Stretch = Stretch.Fill
            };
            // MiddleCard.Background = imageBrush; 
        }
        else
        {
            var imageBrush = new ImageBrush
            {
                ImageSource =
                    new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png")),
                Stretch = Stretch.Fill
            };
            // MiddleCard.Background = imageBrush;
        }
    }

    public void UpdateMiddleCard(string content, string color, string value)
    {
        // MiddleCard.Content = content;

        if (color == "wild")
        {
            var imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/wild/{color}.png")),
                Stretch = Stretch.Fill
            };

            // MiddleCard.Background = imageBrush;
        }
        else
        {
            var imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png")),
                Stretch = Stretch.Fill
            };

            // MiddleCard.Background = imageBrush;
        }
    }
}