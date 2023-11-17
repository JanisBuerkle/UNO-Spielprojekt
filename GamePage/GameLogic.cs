using System;
using System.Collections.Generic;
using System.Linq;

namespace UNO_Spielprojekt.GamePage;

public class GameLogic
{
    public static Propertys prop = new Propertys(); 
    Random _random = new Random();
    List<string> _colors = new List<string> { "Red", "Green", "Blue", "Yellow" };
    List<string> _values = Enumerable.Range(0, 10).Select(i => i.ToString()).Concat(new string[] { "Skip", "+2", "Reverse" }).ToList();
    List<string> _specialCards = new List<string> { "Wild ", "Draw +4" };

    public  int PlayerCount()
    {
        return 1;
    }
    public int ChooseStartingPlayer()
    {
        return _random.Next(0, prop.CountOfPlayers);
    }
    public List<string> GenerateDeck()
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
    public void ShuffleDeck()
    {
        int number = prop.Deck.Count;
        while (number > 1)
        {
            number--;
            int card = _random.Next(number + 1);
            (prop.Deck[card], prop.Deck[number]) = (prop.Deck[number], prop.Deck[card]);
        }
    }

    public List<string> DealCards(int handSize)
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
    
    public List<string> PlaceFirstCardInCenter()
    {
        int randomCard = _random.Next(prop.Deck.Count);
        string selectedCard = prop.Deck[randomCard];
        prop.Deck.RemoveAt(randomCard);
        prop.Center.Add(selectedCard);

        return prop.Center;
    }
}