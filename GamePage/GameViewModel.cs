using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using tt.Tools.Logging;
using UNO_Spielprojekt.Window;
using UNO_Spielprojekt.Winner;

namespace UNO_Spielprojekt.GamePage;

public class GameViewModel : ViewModelBase
{
    private readonly Random _random = new();
    private Brush _theBackground;

    public Brush TheBackground
    {
        get => _theBackground;
        set
        {
            _theBackground = value;
            OnPropertyChanged(nameof(TheBackground));
        }
    }

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
    private readonly ILogger _logger;
    private PlayViewModel PlayViewModel { get; }
    private GameLogic GameLogic { get; }
    private WinnerViewModel WinnerViewModel { get; }


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
    private CardViewModel SelectedCard { get; set; }

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


    public GameViewModel(MainViewModel mainViewModel, PlayViewModel playViewModel, GameLogic gameLogic, ILogger logger,
        WinnerViewModel winnerViewModel)
    {
        TheBackground = Brushes.Transparent;
        GameLogic = gameLogic;
        WinnerViewModel = winnerViewModel;
        PlayViewModel = playViewModel;
        _mainViewModel = mainViewModel;
        this._logger = logger;

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
    private ChooseColorViewModel _chooseColorViewModel;
    private bool _chooseColorVisible;


    private void ZiehenCommandMethod()
    {
        if (_ziehen || _legen)
        {
        }
        else
        {
            TheBackground = Brushes.DarkSeaGreen;
            var card = PlayViewModel.Cards.First();
            PlayViewModel.Cards.RemoveAt(0);
            GameLogic.Players[CurrentPlayer].Hand.Add(card);
            SetCurrentHand();
            _ziehen = true;
            _logger.Info($"{CurrentPlayerName} hat eine Karte gezogen {card.Color} {card.Value}.");
        }
    }

    public void LegenCommandMethod()
    {
        SelectedCard = GameLogic.Players[CurrentPlayer].Hand[SelectedCardIndex];
        if (!_legen)
        {
            _logger.Info($"{CurrentPlayerName} hat {SelectedCard.Color} {SelectedCard.Value} angeklickt.");
            if (SelectedCard.Value == "Wild")
            {
                TheBackground = Brushes.DarkSeaGreen;
                _legen = true;

                ChooseColorViewModel = new ChooseColorViewModel();
                ChooseColorViewModel.PropertyChanged += ColorChoosen;
                ChooseColorVisible = true;
                SetCurrentHand();
                _logger.Info($"{CurrentPlayerName} hat Wild gespielt.");
            }
            else if (SelectedCard.Value == "+4")
            {
                TheBackground = Brushes.DarkSeaGreen;
                _legen = true;

                ChooseColorViewModel = new ChooseColorViewModel();
                ChooseColorViewModel.PropertyChanged += ColorChoosen;
                ChooseColorVisible = true;


                for (var i = 0; i < 4; i++)
                {
                    GameLogic.Players[NextPlayer].Hand.Add(PlayViewModel.Cards[0]);
                    PlayViewModel.Cards.RemoveAt(0);
                }

                SetCurrentHand();
                _logger.Info($"{CurrentPlayerName} hat {SelectedCard.Color} +4 gespielt.");
            }
            else if (SelectedCard.Color == GameLogic.Center[GameLogic.Center.Count - 1].Color ||
                     SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                TheBackground = Brushes.DarkSeaGreen;
                _legen = true;
                GameLogic.Players[CurrentPlayer].Hand.RemoveAt(SelectedCardIndex);
                GameLogic.Center.Add(SelectedCard);
                MiddleCardPic = SelectedCard.ImageUri;
                SetCurrentHand();
                _logger.Info($"{CurrentPlayerName} hat {SelectedCard.Color} {SelectedCard.Value} gespielt.");
            }

            if (SelectedCard.Value == "Skip" &&
                SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                TheBackground = Brushes.DarkSeaGreen;
                IsSkip = true;
                SetCurrentHand();
            }

            else if (SelectedCard.Value == "+2" &&
                     SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                TheBackground = Brushes.DarkSeaGreen;
                for (var i = 0; i < 2; i++)
                {
                    GameLogic.Players[NextPlayer].Hand.Add(PlayViewModel.Cards[0]);
                    PlayViewModel.Cards.RemoveAt(0);
                }

                SetCurrentHand();
            }
            else if (SelectedCard.Value == "Reverse" &&
                     SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                TheBackground = Brushes.DarkSeaGreen;
                if (IsReverse)
                    IsReverse = false;
                else
                    IsReverse = true;
                if (GameLogic.Players.Count == 2)
                {
                    if (CurrentPlayer == 0)
                    {
                        CurrentPlayer = GameLogic.Players.Count - 1;
                    }
                    else
                    {
                        CurrentPlayer--;
                    }
                }
            }
        }
    }

    private void ColorChoosen(object? sender, PropertyChangedEventArgs e)
    {
        ChooseColorVisible = false;
        GameLogic.Players[CurrentPlayer].Hand.RemoveAt(SelectedCardIndex);

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
        _logger.Info($"{CurrentPlayerName} hat eine Farbe ausgewählt.");
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
            CheckForWinner();
            CheckForDrawStack();
            _legen = false;
            _ziehen = false;
            TheBackground = Brushes.Transparent;
            _logger.Info($"{CurrentPlayerName} hat seinen Zug beendet.");
            if (GameLogic.Players[CurrentPlayer].Hand.Count <= 1)
            {
                UnoCommandMethod();
                _logger.Info("UNO wurde vergessen zu drücken.");
            }

            if (IsReverse)
            {
                if (CurrentPlayer == 0)
                {
                    CurrentPlayer = GameLogic.Players.Count - 1;
                    if (IsSkip)
                    {
                        CurrentPlayer = GameLogic.Players.Count - 2;
                        IsSkip = false;
                    }

                    if (CurrentPlayer == 0) NextPlayer = GameLogic.Players.Count - 1;


                    _logger.Info("Eine neue Runde hat begonnen.");
                    RoundCounter++;
                    RoundCounterString = $"Runde: {RoundCounter}/\u221e";
                }
                else
                {
                    CurrentPlayer--;
                    if (IsSkip)
                    {
                        if (CurrentPlayer == 0) CurrentPlayer = GameLogic.Players.Count - 1;
                        IsSkip = false;
                    }

                    NextPlayer = CurrentPlayer - 1;
                }
            }
            else if (!IsReverse)
            {
                if (CurrentPlayer == GameLogic.Players.Count - 1)
                {
                    CurrentPlayer = 0;
                    if (IsSkip)
                    {
                        CurrentPlayer += 1;
                        IsSkip = false;
                    }

                    if (CurrentPlayer == GameLogic.Players.Count - 1)
                    {
                        NextPlayer = 0;
                    }
                    else
                    {
                        NextPlayer = CurrentPlayer + 1;
                    }

                    _logger.Info("Neue Runde hat begonnen.");
                    RoundCounter++;
                    RoundCounterString = $"Runde: {RoundCounter}/\u221e";
                }
                else
                {
                    CurrentPlayer++;
                    if (IsSkip)
                    {
                        if (CurrentPlayer == GameLogic.Players.Count - 1)
                        {
                            CurrentPlayer = 0;
                        }
                        IsSkip = false;
                    }

                    if (CurrentPlayer == GameLogic.Players.Count - 1)
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
            CurrentPlayerName = GameLogic.Players[CurrentPlayer].PlayerName;
        }
    }

    private void UnoCommandMethod()
    {
        if (GameLogic.Players[CurrentPlayer].Uno == false)
        {
            if (GameLogic.Players[CurrentPlayer].Hand.Count <= 1)
            {
                _logger.Info("UNO wurde gerufen!");
                GameLogic.Players[CurrentPlayer].Uno = true;
            }
            else
            {
                _logger.Info("Uno wurde gedrückt, ohne das diese Person 1 Karte hatte.");
            }
        }
    }

    public void Game()
    {
        _mainViewModel.GoToGame();
    }

    public void SetCurrentHand()
    {
        _logger.Info("CurrentHand wurde gesetzt.");
        if (GameLogic.Players.Count != 0)
        {
            CurrentHand.Clear();
            foreach (var card in GameLogic.Players[CurrentPlayer].Hand) CurrentHand.Add(card);
        }

        CurrentPlayerName = GameLogic.Players[CurrentPlayer].PlayerName;
    }

    private void ExitConfirmCommandMethod()
    {
        var exitConfirmWindow = new ExitConfirmWindow(_mainViewModel)
        {
            Owner = MainWindowView.Instance
        };
        exitConfirmWindow.ShowDialog();
    }

    private void CheckForDrawStack()
    {
        if (PlayViewModel.Cards.Count <= 5)
        {
            var card1 = GameLogic.Center.Last();
            GameLogic.Center.RemoveAt(GameLogic.Center.Count - 1);
            foreach (var card in GameLogic.Center)
            {
                if (card.Value == "+4")
                {
                    card.Color = "Draw";
                    card.ImageUri = "pack://application:,,,/Assets/cards/+4/Draw.png";
                }
                else if (card.Value == "Wild")
                {
                    card.Color = "Wild";
                    card.ImageUri = "pack://application:,,,/Assets/cards/Wild/Wild.png";
                }

                PlayViewModel.Cards.Add(card);
            }

            var number = PlayViewModel.Cards.Count;
            while (number > 1)
            {
                number--;
                var card = _random.Next(number + 1);
                (PlayViewModel.Cards[card], PlayViewModel.Cards[number]) =
                    (PlayViewModel.Cards[number], PlayViewModel.Cards[card]);
            }

            GameLogic.Center.Clear();
            GameLogic.Center.Add(card1);
            _logger.Info("Center wird zu Deck hinzugefügt. (Mischen)");
        }
    }

    private void CheckForWinner()
    {
        if (GameLogic.Players[CurrentPlayer].Uno == false)
        {
            if (GameLogic.Players[CurrentPlayer].Hand.Count <= 1)
            {
                var card = PlayViewModel.Cards.First();
                PlayViewModel.Cards.RemoveAt(0);
                GameLogic.Players[CurrentPlayer].Hand.Add(card);
                SetCurrentHand();
                GameLogic.Players[CurrentPlayer].Uno = true;
                _ziehen = true;
                _logger.Info($"{CurrentPlayerName} hat vergessen Uno zu drücken.");
            }
        }

        if (GameLogic.Players[CurrentPlayer].Hand.Count == 0)
        {
            _logger.Info($"{CurrentPlayerName} hat Gewonnen!");
            WinnerViewModel.WinnerName = CurrentPlayerName;
            WinnerViewModel.RoundCounter = RoundCounter.ToString();
            _mainViewModel.GoToWinner();
        }
    }

    public void InitializeGame()
    {
        InitializePlayersHands();
        InitializeGameProperties();
        GameLogic.PlaceFirstCardInCenter();

        MiddleCard = GameLogic.Center.First();
        SelectedCard = MiddleCard;
        if (MiddleCard.Color == "Wild" || MiddleCard.Color == "Draw")
        {
            ChooseColorViewModel = new ChooseColorViewModel();
            ChooseColorViewModel.PropertyChanged += ColorChoosen;
            ChooseColorVisible = true;
            GameLogic.Center.RemoveAt(0);
        }

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

    public void ClearAllLists()
    {
        GameLogic.Center.Clear();
        GameLogic.Players.Clear();
        PlayViewModel.Cards.Clear();
    }
}