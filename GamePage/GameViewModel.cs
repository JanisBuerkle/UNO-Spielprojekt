using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using tt.Tools.Logging;
using UNO_Spielprojekt.Window;

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


    private readonly MainViewModel _mainViewModel;
    private readonly ILogger logger;
    private PlayViewModel PlayViewModel { get; }
    private GameLogic GameLogic { get; }


    private int StartingPlayer { get; set; }
    private int CurrentPlayer { get; set; }
    private int NextPlayer { get; set; }
    public ObservableCollection<CardViewModel> CurrentHand { get; set; } = new();
    public int SelectedCardIndex { get; set; }
    private int RoundCounter { get; set; }
    
    private string _roundCounterString;
    public string RoundCounterString
    {
        get => _roundCounterString;
        set
        {
            if (_roundCounterString != value)
            {
                _roundCounterString = value;
                OnPropertyChanged();
            }
        }
    }

    private bool IsSkip { get; set; }
    private bool IsReverse { get; set; }
    public CardViewModel SelectedCard { get; set; }

    private string _currentPlayerName;

    public string CurrentPlayerName
    {
        get => _currentPlayerName;
        set
        {
            if (_currentPlayerName != value)
            {
                _currentPlayerName = value;
                OnPropertyChanged();
            }
        }
    }

    public RelayCommand ZiehenCommand { get; }
    public RelayCommand LegenCommand { get; }
    public RelayCommand FertigCommand { get; }
    public RelayCommand UnoCommand { get; }
    public RelayCommand ExitConfirmCommand { get; }


    public GameViewModel(MainViewModel mainViewModel, PlayViewModel playViewModel, GameLogic gameLogic, ILogger logger)
    {
        GameLogic = gameLogic;
        PlayViewModel = playViewModel;
        _mainViewModel = mainViewModel;
        this.logger = logger;

        ZiehenCommand = new RelayCommand(ZiehenCommandMethod);
        LegenCommand = new RelayCommand(LegenCommandMethod);
        FertigCommand = new RelayCommand(FertigCommandMethod);
        UnoCommand = new RelayCommand(UnoCommandMethod);

        ExitConfirmCommand = new RelayCommand(ExitConfirmCommandMethod);
        RoundCounter = 1;
        RoundCounterString = $"Runde: {RoundCounter}/\u221e";
    }

    private bool _legen;
    private bool _ziehen;
    private bool _uno;
    private ChooseColorViewModel _chooseColorViewModel;
    private bool _chooseColorVisible;


    private void ZiehenCommandMethod()
    {
        if (_ziehen || _legen)
        {
        }
        else
        {
            var card = PlayViewModel.Cards.First();
            PlayViewModel.Cards.RemoveAt(0);
            GameLogic.players[CurrentPlayer].Hand.Add(card);
            SetCurrentHand();
            _ziehen = true;
            Console.WriteLine("Gezogen");
        }
    }

    public void LegenCommandMethod()
    {
        SelectedCard = GameLogic.players[CurrentPlayer].Hand[SelectedCardIndex];
        if (!_legen)
        {

            if (SelectedCard.Value == "Wild")
            {
                _legen = true;

                ChooseColorViewModel = new ChooseColorViewModel();
                ChooseColorViewModel.PropertyChanged += ColorChoosen;
                ChooseColorVisible = true;
                SetCurrentHand();
            }
            else if (SelectedCard.Value == "+4")
            {
                _legen = true;

                ChooseColorViewModel = new ChooseColorViewModel();
                ChooseColorViewModel.PropertyChanged += ColorChoosen;
                ChooseColorVisible = true;
                

                for (var i = 0; i < 4; i++)
                {
                    GameLogic.players[NextPlayer].Hand.Add(PlayViewModel.Cards[0]);
                    PlayViewModel.Cards.RemoveAt(0);
                }

                SetCurrentHand();
            }
            else if (SelectedCard.Color == GameLogic.Center[GameLogic.Center.Count - 1].Color ||
                     SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                _legen = true;
                GameLogic.players[CurrentPlayer].Hand.RemoveAt(SelectedCardIndex);
                GameLogic.Center.Add(SelectedCard);
                MiddleCardPic = SelectedCard.ImageUri;
                SetCurrentHand();
            }

            if (SelectedCard.Value == "Skip")
            {
                IsSkip = true;
                SetCurrentHand();
            }

            else if (SelectedCard.Value == "+2")
            {
                for (var i = 0; i < 2; i++)
                {
                    GameLogic.players[NextPlayer].Hand.Add(PlayViewModel.Cards[0]);
                    PlayViewModel.Cards.RemoveAt(0);
                }

                SetCurrentHand();
            }
            else if (SelectedCard.Value == "Reverse")
            {
                if (IsReverse)
                    IsReverse = false;
                else
                    IsReverse = true;
                if (GameLogic.players.Count == 2)
                {
                    if (CurrentPlayer == 0)
                    {
                        CurrentPlayer = GameLogic.players.Count - 1;
                    }
                    else
                    {
                        CurrentPlayer--;
                    }
                }
            }

            logger.Info($"{SelectedCard.Color} {SelectedCard.Value} wurde gelegt.");
        }
    }

    private void ColorChoosen(object? sender, PropertyChangedEventArgs e)
    {
        ChooseColorVisible = false;
        GameLogic.players[CurrentPlayer].Hand.RemoveAt(SelectedCardIndex);

        if (SelectedCard.Value == "Wild")
        {
            GameLogic.Center.Add(GameLogic.WildCards[ChooseColorViewModel.ChoosenColor]);
            MiddleCardPic = GameLogic.WildCards[ChooseColorViewModel.ChoosenColor].ImageUri;
        }
        else
        {
            GameLogic.Center.Add(GameLogic.Draw4Cards[ChooseColorViewModel.ChoosenColor]);
            MiddleCardPic = GameLogic.Draw4Cards[ChooseColorViewModel.ChoosenColor].ImageUri;
        }
        SetCurrentHand();
    }

    public ChooseColorViewModel ChooseColorViewModel
    {
        get => _chooseColorViewModel;
        set
        {
            if (Equals(value, _chooseColorViewModel)) return;
            _chooseColorViewModel = value;
            OnPropertyChanged();
        }
    }

    public bool ChooseColorVisible
    {
        get => _chooseColorVisible;
        set
        {
            if (value == _chooseColorVisible) return;
            _chooseColorVisible = value;
            OnPropertyChanged();
        }
    }

    private void FertigCommandMethod()
    {
        if (_legen || _ziehen)
        {
            _legen = false;
            _ziehen = false;
            _uno = false;

            if (IsReverse)
            {
                if (CurrentPlayer == 0)
                {
                    CurrentPlayer = GameLogic.players.Count - 1;
                    if (IsSkip)
                    {
                        CurrentPlayer = GameLogic.players.Count - 2;
                        IsSkip = false;
                    }

                    NextPlayer = CurrentPlayer - 1;

                    logger.Info("Neue Runde hat begonnen.");
                    RoundCounter++;
                    RoundCounterString = $"Runde: {RoundCounter}/\u221e";

                }
                else
                {
                    CurrentPlayer--;
                    if (IsSkip)
                    {
                        if (CurrentPlayer == 0) CurrentPlayer = GameLogic.players.Count - 1;
                        IsSkip = false;
                    }

                    if (CurrentPlayer == 0) NextPlayer = GameLogic.players.Count - 1;
                }
            }
            else if (!IsReverse)
            {
                if (CurrentPlayer == GameLogic.players.Count - 1)
                {
                    CurrentPlayer = 0;
                    if (IsSkip)
                    {
                        CurrentPlayer = 1;
                        IsSkip = false;
                    }

                    NextPlayer = CurrentPlayer + 1;

                    logger.Info("Neue Runde hat begonnen.");
                    RoundCounter++;
                    RoundCounterString = $"Runde: {RoundCounter}/\u221e";
                }
                else
                {
                    CurrentPlayer++;
                    if (IsSkip)
                    {
                        if (CurrentPlayer == GameLogic.players.Count - 1) CurrentPlayer = 0;
                        IsSkip = false;
                    }

                    if (CurrentPlayer == GameLogic.players.Count - 1)
                    {
                        NextPlayer = 0;
                    }
                    else
                    {
                        NextPlayer = CurrentPlayer + 1;
                    }
                }
            }

            SetCurrentHand();
            CurrentPlayerName = GameLogic.players[CurrentPlayer].PlayerName + " " + CurrentPlayer;
            Console.WriteLine(CurrentPlayerName);
        }
    }

    private void UnoCommandMethod()
    {
        if (_uno == false)
        {
            if (GameLogic.players[CurrentPlayer].Hand.Count == 1)
            {
                logger.Info("UNO wurde gerufen!");
            }
            else
            {
                var card = PlayViewModel.Cards.First();
                PlayViewModel.Cards.RemoveAt(0);
                GameLogic.players[CurrentPlayer].Hand.Add(card);
                SetCurrentHand();
                _uno = true;
                _ziehen = true;
                logger.Info("Uno wurde gedrückt, ohne das diese Person 1 Karte hatte.");
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
            foreach (var card in GameLogic.players[CurrentPlayer].Hand) CurrentHand.Add(card);
        }

        CurrentPlayerName = GameLogic.players[CurrentPlayer].PlayerName + " " + CurrentPlayer;
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
        CurrentPlayer = StartingPlayer;
        GameLogic.ShuffleDeck();
        GameLogic.DealCards(7);
    }

    private void InitializePlayersHands()
    {
        foreach (var cards in GameLogic.cards) PlayViewModel.Cards.Add(cards);
    }
}