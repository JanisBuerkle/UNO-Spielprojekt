using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using tt.Tools.Logging;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Scoreboard;

[Serializable]
public class ScoreboardViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    private List<ScoreboardPlayer> _scoreboardPlayers = new();
    public List<ScoreboardPlayer> ScoreboardPlayers
    {
        get => _scoreboardPlayers;
        set
        {
            if (_scoreboardPlayers != value)
            {
                _scoreboardPlayers = value;
                OnPropertyChanged();
            }
        }
    }

    
    public RelayCommand GoToMainMenuCommand { get; }
    private GameViewModel _gameViewModel;
    private ScoreboardViewModel _scoreboardViewModel;
    private readonly ILogger _logger;
    public ScoreboardViewModel(MainViewModel mainViewModel, ILogger logger)
    {
        _logger = logger;
        _scoreboardViewModel = this;
        _mainViewModel = mainViewModel;
        GoToMainMenuCommand = new RelayCommand(Test);
        LoadGameData();
    }
    
    private void LoadGameData()
    {
        ScoreboardPlayers = _gameViewModel.LoadPlayersFromXml("GameData.xml");

        List<ScoreboardPlayer> sortedList = ScoreboardPlayers.OrderByDescending(ScoreboardPlayer => ScoreboardPlayer.PlayerScoreboardScore).ToList();
        ScoreboardPlayers.Clear();
        foreach (var player in sortedList)
        {
            ScoreboardPlayers.Add(player);
        }
    } 
    public void Test()
    {
        _mainViewModel.GoToMainMenu();
    }
}