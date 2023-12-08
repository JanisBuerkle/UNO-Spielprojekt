using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Scoreboard;
using UNO_Spielprojekt.Window;

namespace WPF_Spielprojekt_Schwimmen.Scoreboard;

public class ScoreboardViewModel
{
    private readonly MainViewModel _mainViewModel;
    public RelayCommand GoToMainMenuCommand { get; }
    private ObservableCollection<ScoreboardPlayer> scoreboardPlayers = new();

    public ObservableCollection<ScoreboardPlayer> ScoreboardPlayers
    {
        get => scoreboardPlayers;
        set
        {
            if (scoreboardPlayers != value) scoreboardPlayers = value;
        }
    }

    public ScoreboardViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        GoToMainMenuCommand = new RelayCommand(mainViewModel.GoToMainMenu);
        if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            for (var i = 0; i < 10; i++)
            {
                var random = new Random();
                var player = new ScoreboardPlayer
                {
                    PlayerScoreboardName = $"Player {i}",
                    PlayerScoreboardScore = random.Next(1, 100)
                };
                ScoreboardPlayers.Add(player);
            }

        var sortedList =
            ScoreboardPlayers.OrderByDescending(ScoreboardPlayer => ScoreboardPlayer.PlayerScoreboardScore).ToList();
        scoreboardPlayers.Clear();
        foreach (var player in sortedList) scoreboardPlayers.Add(player);
    }
}