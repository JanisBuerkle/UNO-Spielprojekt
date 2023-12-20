using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;
using tt.Tools.Logging;


namespace UNO_Spielprojekt.AddPlayer;

public class AddPlayerViewModel : INotifyPropertyChanged
{
    private readonly MainViewModel _mainViewModel;
    public RelayCommand GoToMainMenuCommand { get; }
    public GameLogic GameLogic { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public RelayCommand WeiterButtonCommand { get; }

    private readonly ILogger logger;
    public AddPlayerViewModel(MainViewModel mainViewModel, GameLogic gameLogic, ILogger logger)
    {
        this.logger = logger;
        _mainViewModel = mainViewModel;
        GameLogic = gameLogic;
        PlayerNames = new ObservableCollection<NewPlayerViewModel>();
        PlayViewModel playViewModel = new PlayViewModel();
        
        GoToMainMenuCommand = new RelayCommand(GoToMainMenuCommandMethod);
        WeiterButtonCommand = new RelayCommand(WeiterButtonCommandMethod);
    }

    private void GoToMainMenuCommandMethod()
    {
        logger.Info("MainMenu wurde geöffnet.");
        _mainViewModel.GoToMainMenu();
    }
    private void WeiterButtonCommandMethod()
    {
        foreach (var t in PlayerNames)
        {
            logger.Info($"Neuer Spieler: {t.Name} wurde hinzugefügt.");
            GameLogic.players.Add(new Players { PlayerName = t.Name });
        }
        logger.Info("Rules Seite wurde geöffnet.");
        _mainViewModel.GoToRules();
    }

    public ObservableCollection<NewPlayerViewModel> PlayerNames { get; }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}