using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using tt.Tools.Logging;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.GamePage;

public class GameViewModel : ViewModelBase
{
    private Brush _theBackground;

    public Brush TheBackground
    {
        get { return _theBackground; }
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
        TheBackground = Brushes.Transparent;
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
    private ChooseColorViewModel _chooseColorViewModel;
    private bool _chooseColorVisible;


    private void ZiehenCommandMethod()
    {
        if (_ziehen || _legen)
        {
        }
        else
        {
            TheBackground = Brushes.Wheat;
            var card = PlayViewModel.Cards.First();
            PlayViewModel.Cards.RemoveAt(0);
            GameLogic.players[CurrentPlayer].Hand.Add(card);
            SetCurrentHand();
            _ziehen = true;
            logger.Info($"{CurrentPlayerName} hat eine Karte gezogen {card.Color} {card.Value}.");
        }
    }

    public void LegenCommandMethod()
    {
        SelectedCard = GameLogic.players[CurrentPlayer].Hand[SelectedCardIndex];
        if (!_legen)
        {
            logger.Info($"{CurrentPlayerName} hat {SelectedCard.Color} {SelectedCard.Value} angeklickt.");
            if (SelectedCard.Value == "Wild")
            {
                TheBackground = Brushes.Wheat;
                _legen = true;

                ChooseColorViewModel = new ChooseColorViewModel();
                ChooseColorViewModel.PropertyChanged += ColorChoosen;
                ChooseColorVisible = true;
                SetCurrentHand();
                logger.Info($"{CurrentPlayerName} hat Wild gespielt.");
            }
            else if (SelectedCard.Value == "+4")
            {
                TheBackground = Brushes.Wheat;
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
                logger.Info($"{CurrentPlayerName} hat {SelectedCard.Color} +4 gespielt.");
            }
            else if (SelectedCard.Color == GameLogic.Center[GameLogic.Center.Count - 1].Color ||
                     SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                TheBackground = Brushes.Wheat;
                _legen = true;
                GameLogic.players[CurrentPlayer].Hand.RemoveAt(SelectedCardIndex);
                GameLogic.Center.Add(SelectedCard);
                MiddleCardPic = SelectedCard.ImageUri;
                SetCurrentHand();
                logger.Info($"{CurrentPlayerName} hat {SelectedCard.Color} {SelectedCard.Value} gespielt.");
            }

            if (SelectedCard.Value == "Skip" &&
                SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                TheBackground = Brushes.Wheat;
                IsSkip = true;
                SetCurrentHand();
            }

            else if (SelectedCard.Value == "+2" &&
                     SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                TheBackground = Brushes.Wheat;
                for (var i = 0; i < 2; i++)
                {
                    GameLogic.players[NextPlayer].Hand.Add(PlayViewModel.Cards[0]);
                    PlayViewModel.Cards.RemoveAt(0);
                }

                SetCurrentHand();
            }
            else if (SelectedCard.Value == "Reverse" &&
                     SelectedCard.Value == GameLogic.Center[GameLogic.Center.Count - 1].Value)
            {
                TheBackground = Brushes.Wheat;
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
        logger.Info($"{CurrentPlayerName} hat eine Farbe ausgewählt.");
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
            _legen = false;
            _ziehen = false;
            TheBackground = Brushes.Transparent;
            logger.Info($"{CurrentPlayerName} hat seinen Zug beendet.");
            if (GameLogic.players[CurrentPlayer].Hand.Count <= 1)
            {
                UnoCommandMethod();
                logger.Info("UNO wurde vergessen zu drücken.");
            }

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

                    logger.Info("Eine neue Runde hat begonnen.");
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
            CurrentPlayerName = GameLogic.players[CurrentPlayer].PlayerName + " (" + CurrentPlayer + ")";
        }
    }

    private void UnoCommandMethod()
    {
        if (GameLogic.players[CurrentPlayer].Uno == false)
        {
            if (GameLogic.players[CurrentPlayer].Hand.Count <= 1)
            {
                logger.Info("UNO wurde gerufen!");
                GameLogic.players[CurrentPlayer].Uno = true;
            }
            else
            {
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
        logger.Info("CurrentHand wurde gesetzt.");
        if (GameLogic.players.Count != 0)
        {
            CurrentHand.Clear();
            foreach (var card in GameLogic.players[CurrentPlayer].Hand) CurrentHand.Add(card);
        }

        CurrentPlayerName = GameLogic.players[CurrentPlayer].PlayerName + " (" + CurrentPlayer + ")";
    }

    private void ExitConfirmCommandMethod()
    {
        var exitConfirmWindow = new ExitConfirmWindow(_mainViewModel)
        {
            Owner = MainWindowView.Instance
        };
        exitConfirmWindow.ShowDialog();
    }


    private void CheckForWinner()
    {
        if (GameLogic.players[CurrentPlayer].Uno == false)
        {
            if (GameLogic.players[CurrentPlayer].Hand.Count <= 1)
            {
                var card = PlayViewModel.Cards.First();
                PlayViewModel.Cards.RemoveAt(0);
                GameLogic.players[CurrentPlayer].Hand.Add(card);
                SetCurrentHand();
                GameLogic.players[CurrentPlayer].Uno = true;
                _ziehen = true;
                logger.Info($"{CurrentPlayerName} hat vergessen Uno zu drücken.");
            }
        }

        if (GameLogic.players[CurrentPlayer].Hand.Count == 0)
        {
            logger.Info($"{CurrentPlayerName} hat Gewonnen!");
            _mainViewModel.GoToWinner();
        }
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