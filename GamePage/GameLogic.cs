using System;
using System.Collections.Generic;
using System.Linq;

namespace UNO_Spielprojekt.GamePage;

public class GameLogic
{
    public static Propertys prop = new Propertys();
    static Random _random = new Random();
    
    static List<string> _colors = new List<string> { "Red", "Green", "Blue", "Yellow" };
    static List<string> _values = Enumerable.Range(0, 10).Select(i => i.ToString()).Concat(new string[] { "Skip", "+2", "Reverse" }).ToList();
    static List<string> _specialCards = new List<string> { "Wild ", "Draw +4" };

    public static int PlayerCount()
    {
        return 1;
    }
    public static int ChooseStartingPlayer()
    {
        return _random.Next(0, prop.CountOfPlayers);
    }
    public static List<string> GenerateDeck()
    {
        List<string> deck = new List<string>();
        foreach (string color in _colors)
        {
            foreach (string value in _values)
            {
                deck.Add($"{color} {value}");
                prop.Deck = deck;
                if (value != "0")
                {
                    prop.Deck.Add(color + " " + value);
                }
            }
        }
            
        foreach (string specialCard in _specialCards)
        {
            for (int i = 0; i < 4; i++)
            {
                deck.Add(specialCard); 
            }

        }
        return prop.Deck;
    }
    public static void ShuffleDeck()
    {
        int number = prop.Deck.Count;
        while (number > 1)
        {
            number--;
            int card = _random.Next(number + 1);
            string value = prop.Deck[card];
            prop.Deck[card] = prop.Deck[number];
            prop.Deck[number] = value;
        }
    }

    public static List<string> DealCards(int handSize)
    {
        List<string> hand = new List<string>();
        for (prop.Player = 0; prop.Player < handSize; prop.Player++)
        {
            string card = prop.Deck.First();
            prop.Deck.RemoveAt(0);
            hand.Add(card);
        }
        return hand;
    }
}