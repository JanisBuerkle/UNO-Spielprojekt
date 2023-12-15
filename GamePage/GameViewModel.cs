using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Window;
using Wpf.Ui.Controls;
using Button = System.Windows.Controls.Button;

namespace UNO_Spielprojekt.GamePage;

public class GameViewModel : ViewModelBase
{
    private CardViewModel _middleCard;

    private CardViewModel MiddleCard
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

    private ObservableCollection<string> _buttonTexts = new() { "Button 1", "Button 2", "Button 3" };

    private readonly MainViewModel _mainViewModel;
    private int StartingPlayer { get; set; }
    private PlayViewModel PlayViewModel { get; }
    private GameLogic GameLogic { get; set; }
    public object ClickedCard { get; set; }
    private int currentPlayer { get; set; }
    public CardViewModel CardViewModel { get; set; }
    public RelayCommand ZiehenCommand { get; }
    public RelayCommand LegenCommand { get; }
    public RelayCommand FertigCommand { get; }
    public RelayCommand UNOCommand { get; }
    public RelayCommand ExitConfirmCommand { get; }

    public ObservableCollection<CardViewModel> CurrentHand { get; set; } = new();
    public int selectedCardIndex { get; set; }
    public ICommand CardClickedCommand { get; private set; }

    public GameViewModel(MainViewModel mainViewModel, PlayViewModel playViewModel, GameLogic gameLogic)
    {
        GameLogic = gameLogic;
        PlayViewModel = playViewModel;
        _mainViewModel = mainViewModel;

        ZiehenCommand = new RelayCommand(ZiehenCommandMethod);
        LegenCommand = new RelayCommand(LegenCommandMethod);
        FertigCommand = new RelayCommand(FertigCommandMethod);
        UNOCommand = new RelayCommand(UnoCommandMethod);

        ExitConfirmCommand = new RelayCommand(ExitConfirmCommandMethod);
    }

    private bool _legen;
    private bool _ziehen;
    private bool _uno;

    private void ZiehenCommandMethod()
    {
        if (_ziehen || _legen)
        {
        }
        else
        {
            CardViewModel card = PlayViewModel.Cards.First();
            PlayViewModel.Cards.RemoveAt(0);
            GameLogic.players[currentPlayer].Hand.Add(card);
            SetCurrentHand();
            _ziehen = true;
            Console.WriteLine("Gezogen");
        }
    }

    public void LegenCommandMethod()
    {
        if (_ziehen || _legen)
        {
        }
        else
        {
            var selectedCard = GameLogic.players[currentPlayer].Hand[selectedCardIndex];
            if (selectedCard.Color == GameLogic.Center[GameLogic.Center.Count - 1].Color ||
                selectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                Console.WriteLine("Gelegt");
                GameLogic.players[currentPlayer].Hand.RemoveAt(selectedCardIndex);
                GameLogic.Center.Add(selectedCard);
                MiddleCardPic = selectedCard.ImageUri;
                SetCurrentHand();
                _legen = true;
            }
            else if (selectedCard.Color == "Wild" || selectedCard.Value == "+4")
            {
                Console.WriteLine("Gelegt");
                GameLogic.players[currentPlayer].Hand.RemoveAt(selectedCardIndex);
                GameLogic.Center.Add(selectedCard);
                MiddleCardPic = selectedCard.ImageUri;
                SetCurrentHand();
                _legen = true;
                
                var chooseColorView = new ChooseColorView()
                {
                    Owner = MainWindowView.Instance
                };
                chooseColorView.ShowDialog();
            }
            else
            {
            }
        }
    }

    private void FertigCommandMethod()
    {
        if (_legen || _ziehen )
        {
            _legen = false;
            _ziehen = false;
            _uno = false;
            
            if (currentPlayer == GameLogic.players.Count - 1)
            {
                currentPlayer = 0;
                Console.WriteLine("Neue Runde");
                Console.WriteLine(GameLogic.players[currentPlayer].PlayerName);
                SetCurrentHand();
            }
            else
            {
                currentPlayer += 1;
                Console.WriteLine(GameLogic.players[currentPlayer].PlayerName);
                SetCurrentHand();
            }
        }
    }

    private void UnoCommandMethod()
    {
        if (_uno == false)
        {
            if (GameLogic.players[currentPlayer].Hand.Count == 1)
            {
                Console.WriteLine("UNOOOOOOOOOOOOO!!!");
            }
            else
            {
                CardViewModel card = PlayViewModel.Cards.First();
                PlayViewModel.Cards.RemoveAt(0);
                GameLogic.players[currentPlayer].Hand.Add(card);
                SetCurrentHand();
                _uno = true;
                _ziehen = true;
                Console.WriteLine("Falsche UNO Aussage");
            }
        }
    }

    public void Game()
    {
        _mainViewModel.GoToGame();
    }

    public void SetCurrentHand()
    {
        if (GameLogic.players.Count != 0)
        {
            CurrentHand.Clear();
            foreach (var card in GameLogic.players[currentPlayer].Hand)
            {
                CurrentHand.Add(card);
            }
        }
    }

    private void ExitConfirmCommandMethod()
    {
        var exitConfirmWindow = new ExitConfirmWindow(_mainViewModel)
        {
            Owner = MainWindowView.Instance
        };
        exitConfirmWindow.ShowDialog();
    }

    public void InitializeGame()
    {
        InitializePlayersHands();
        InitializeGameProperties();
        GameLogic.PlaceFirstCardInCenter();

        MiddleCard = GameLogic.Center.FirstOrDefault();
        var middleCardPath = MiddleCard.ImageUri;
        MiddleCardPic = middleCardPath;
    }

    private void InitializeGameProperties()
    {
        StartingPlayer = GameLogic.ChooseStartingPlayer();
        currentPlayer = StartingPlayer;
        GameLogic.ShuffleDeck();
        GameLogic.DealCards(7);
    }

    private void InitializePlayersHands()
    {
        foreach (CardViewModel cards in GameLogic.cards)
        {
            PlayViewModel.Cards.Add(cards);
        }
    }

    // private void ResetButtonMargins()
    // {
    //     foreach (var button in stackPanell.Children.OfType<ScrollViewer>()
    //                  .SelectMany(sv => ((ItemsControl)sv.Content).Items.OfType<Button>()))
    //         if (button.Tag as string == "Card")
    //             button.Margin = new Thickness(5);
    // }
    // private void MoveCardUp(Button clickedButton)
    // {
    //     var newMargin = clickedButton.Margin;
    //     newMargin.Top -= 100;
    //     clickedButton.Margin = newMargin;
    // }
    // private void LegenButtonMethod()
    // {
    //     // if (_chosenCard)
    //     // {
    //     //     // var indexOfSelectedCard = GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand
    //     //     //     .IndexOf(GameLogic.prop.ClickedCard.ToString());
    //     //
    //     //     if (indexOfSelectedCard != -1)
    //     //     {
    //     //         GameLogic.prop.Players[GameLogic.prop.StartingPlayer].Hand.RemoveAt(indexOfSelectedCard);
    //     //         GameLogic.prop.Center.Add(GameLogic.prop.ClickedCard.ToString());
    //     //         ShowCardInCenter();
    //     //         UpdatePlayerHand();
    //     //         RemoveButtonFromStackPanel(indexOfSelectedCard);
    //     //     }
    //     // }
    // }
    // private void RemoveButtonFromStackPanel(int index)
    // {
    //     if (index >= 0 && stackPanell.Children.Count > index) stackPanell.Children.RemoveAt(index);
    // }
}