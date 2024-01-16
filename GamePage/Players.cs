using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UNO_Spielprojekt.GamePage;

public class Players
{
    public string PlayerName { get; set; }
    public ObservableCollection<CardViewModel> Hand = new();
    public bool Uno { get; set; }

    // public int Player { get; set; }
    // public int CountOfPlayers { get; set; }
    // public List<string> Deck { get; set; }
    // public List<string> Center { get; set; }
    // public int StartingPlayer { get; set; }
    //
    // public List<Players> Player { get; set; } = new();
    // public int Richtung { get; set; }
    // public int PlayerSave { get; set; }
    // public int ReverseCardPlayed { get; set; }
    // public int NextPlayer { get; set; }
    // public bool validCardPlayed { get; set; }
    // public bool Draw { get; set; }
    // public int ClickedCardInt { get; set; }

    // public Players()
    // {
    //     Hand = new List<CardViewModel>();
    //     Center = new List<string>();
    //     Deck = new List<string>();
    // }
}