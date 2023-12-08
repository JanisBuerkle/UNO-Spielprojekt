using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.GamePage;

public class ExitConfirmViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public ExitConfirmViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public void ConfirmButtonCommandMethod()
    {
        _mainViewModel.GoToMainMenu();
    }
}