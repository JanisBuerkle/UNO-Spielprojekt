using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Scoreboard;

public class ScoreboardViewModel
{
    private readonly MainViewModel _mainViewModel;
    private readonly GameLogic _gameLogic;
    public RelayCommand GoToMainMenuCommand { get; }

    public ObservableCollection<string> ScoreboardPlayerName { get; set; }
    private ObservableCollection<ScoreboardPlayer> scoreboardPlayers;

    public ObservableCollection<ScoreboardPlayer> ScoreboardPlayers
    {
        get => scoreboardPlayers;
        set
        {
            if (scoreboardPlayers != value) scoreboardPlayers = value;
        }
    }

    public ScoreboardViewModel(MainViewModel mainViewModel, GameLogic gameLogic)
    {
        _mainViewModel = mainViewModel;
        _gameLogic = gameLogic;
        GoToMainMenuCommand = new RelayCommand(mainViewModel.GoToMainMenu);
    }

    private void Test()
    {
        if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            foreach (var name in ScoreboardPlayerName)
            {
                var player = new ScoreboardPlayer
                {
                    PlayerScoreboardName = name,
                    PlayerScoreboardScore = 1
                };
                ScoreboardPlayers.Add(player);
            }

        var sortedList = ScoreboardPlayers.OrderByDescending(ScoreboardPlayer => ScoreboardPlayer.PlayerScoreboardScore).ToList();
        scoreboardPlayers.Clear();
        foreach (var player in sortedList) scoreboardPlayers.Add(player);
    }
}