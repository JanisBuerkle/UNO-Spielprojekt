using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Winner;

public class WinnerViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    public RelayCommand GoToMainMenuCommand { get; }

    private string _winnerName;

    public string WinnerName
    {
        get => _winnerName;
        set
        {
            if (_winnerName != value)
            {
                _winnerName = value;
                OnPropertyChanged();
            }
        }
    }

    private string _roundCounter;

    public string RoundCounter
    {
        get => _roundCounter;
        set
        {
            if (_roundCounter != value)
            {
                _roundCounter = value;
                OnPropertyChanged();
            }
        }
    }

    public WinnerViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        GoToMainMenuCommand = new RelayCommand(mainViewModel.GoToMainMenu);
    }
}