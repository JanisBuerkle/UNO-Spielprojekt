using System.Collections.ObjectModel;

namespace UNO_Spielprojekt.GamePage;

public class PlayViewModel : ViewModelBase
{
    private ObservableCollection<CardViewModel> _cards = new();

    public ObservableCollection<CardViewModel> Cards
    {
        get => _cards;
        set
        {
            if (Equals(value, _cards)) return;
            _cards = value;
            OnPropertyChanged();
        }
    }
}