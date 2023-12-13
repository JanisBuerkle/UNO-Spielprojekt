using System;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace UNO_Spielprojekt.GamePage;

public class PlayViewModel : ViewModelBase
{
    private ObservableCollection<CardViewModel> _cards = new ()
    {
        new CardViewModel { Color = "Blue", Value = "1", ImageUri = "pack://application:,,,/Assets/cards/1/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "1", ImageUri = "pack://application:,,,/Assets/cards/1/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "2", ImageUri = "pack://application:,,,/Assets/cards/2/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "2", ImageUri = "pack://application:,,,/Assets/cards/2/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "3", ImageUri = "pack://application:,,,/Assets/cards/3/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "3", ImageUri = "pack://application:,,,/Assets/cards/3/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "4", ImageUri = "pack://application:,,,/Assets/cards/4/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "4", ImageUri = "pack://application:,,,/Assets/cards/4/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "5", ImageUri = "pack://application:,,,/Assets/cards/5/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "5", ImageUri = "pack://application:,,,/Assets/cards/5/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "6", ImageUri = "pack://application:,,,/Assets/cards/6/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "6", ImageUri = "pack://application:,,,/Assets/cards/6/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "7", ImageUri = "pack://application:,,,/Assets/cards/7/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "7", ImageUri = "pack://application:,,,/Assets/cards/7/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "8", ImageUri = "pack://application:,,,/Assets/cards/8/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "8", ImageUri = "pack://application:,,,/Assets/cards/8/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "9", ImageUri = "pack://application:,,,/Assets/cards/9/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "9", ImageUri = "pack://application:,,,/Assets/cards/9/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "Reverse", ImageUri = "pack://application:,,,/Assets/cards/reverse/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "Reverse", ImageUri = "pack://application:,,,/Assets/cards/reverse/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "Skip", ImageUri = "pack://application:,,,/Assets/cards/skip/Blue.png" },
        new CardViewModel { Color = "Blue", Value = "Skip", ImageUri = "pack://application:,,,/Assets/cards/skip/Blue.png" },  
        new CardViewModel { Color = "Blue", Value = "+2", ImageUri = "pack://application:,,,/Assets/cards/+2/Blue.png" },        
        new CardViewModel { Color = "Blue", Value = "+2", ImageUri = "pack://application:,,,/Assets/cards/+2/Blue.png" },
    };

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