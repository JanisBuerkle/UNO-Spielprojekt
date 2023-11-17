using System.Collections.Generic;

namespace UNO_Spielprojekt.GamePage;

public class Propertys
{
    public string PlayerName { get; set; }
    public int Richtung { get; set; }
    public int Player { get; set; }
    public int PlayerSave { get; set; }
    public int ReverseCardPlayed { get; set; }
    public int NextPlayer { get; set; }
    public int CountOfPlayers { get; set; }
    public List<string> Hand { get; set; }
    public List<string> Deck { get; set; }
    public List<string> Center { get; set; }
    public int StartingPlayer { get; set; }
    public bool validCardPlayed { get; set; }
    public bool Draw { get; set; }
    public int ClickedCardInt { get; set; }
    public object ClickedCard { get; set; }
    public List<Propertys> props { get; set; }= new List<Propertys>();
    public Propertys()
    {
        Hand = new List<string>();
        Center = new List<string>();
        Deck = new List<string>(); 
    }
}