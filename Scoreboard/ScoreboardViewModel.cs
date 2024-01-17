using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Scoreboard;

[Serializable]
public class ScoreboardViewModel : ViewModelBase
{

    private readonly MainViewModel _mainViewModel;
    
    private ObservableCollection<ScoreboardPlayer> _scoreboardPlayers = new ObservableCollection<ScoreboardPlayer>();
    public ObservableCollection<ScoreboardPlayer> ScoreboardPlayers
    {
        get => _scoreboardPlayers;
        set
        {
            if (_scoreboardPlayers != value)
            {
                _scoreboardPlayers = value;
            }
        }
    }
    public RelayCommand GoToMainMenuCommand { get; }
    public ScoreboardViewModel _scoreboardViewModel;
    public ScoreboardViewModel(MainViewModel mainViewModel)
    {
        _scoreboardViewModel = this;
        _mainViewModel = mainViewModel;
        GoToMainMenuCommand = new RelayCommand(Test);
        LoadGameData();
        ScoreboardPlayers = _scoreboardViewModel.ScoreboardPlayers;
    }
    
    private void LoadGameData()
    {
        if (File.Exists("GameData.xml"))
        {
            using (var reader = new StreamReader("GameData.xml"))
            {
                var serializer = new XmlSerializer(typeof(ScoreboardViewModel));
                _scoreboardViewModel = (ScoreboardViewModel)serializer.Deserialize(reader);
            }
        }
    } 
    public void Test()
    {
        _mainViewModel.GoToMainMenu();
    }
    public ScoreboardViewModel()
    {
        
    }
}