using System;
using System.Collections.Generic;
using System.Linq;

namespace UNO_Spielprojekt.GamePage;

public class GameLogic
{
    public static Propertys prop = new();
    private readonly Random _random = new();
    private readonly List<string> _colors = new() { "Red", "Green", "Blue", "Yellow" };

    private readonly List<string> _values = Enumerable.Range(0, 10).Select(i => i.ToString())
        .Concat(new[] { "Skip", "+2", "Reverse" }).ToList();

    private readonly List<string> _specialCards = new() { "Wild ", "Draw +4" };

    public int PlayerCount()
    {
        return 1;
    }

    public int ChooseStartingPlayer()
    {
        return _random.Next(0, prop.CountOfPlayers);
    }

    public List<string> GenerateDeck()
    {
        var deck = new List<string>();

        foreach (var color in _colors)
        foreach (var value in _values)
            if (value != "0")
                deck.Add($"{color} {value}");

        foreach (var specialCard in _specialCards)
            for (var i = 0; i < 4; i++)
                deck.Add(specialCard);

        prop.Deck = deck.Concat(deck).ToList();

        return prop.Deck;
    }

    public List<CardViewModel> cards = new List<CardViewModel>()
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
        
        new CardViewModel { Color = "Green", Value = "1", ImageUri = "pack://application:,,,/Assets/cards/1/Blue.png" },
        new CardViewModel { Color = "Green", Value = "1", ImageUri = "pack://application:,,,/Assets/cards/1/Green.png" },
        new CardViewModel { Color = "Green", Value = "2", ImageUri = "pack://application:,,,/Assets/cards/2/Green.png" },
        new CardViewModel { Color = "Green", Value = "2", ImageUri = "pack://application:,,,/Assets/cards/2/Green.png" },
        new CardViewModel { Color = "Green", Value = "3", ImageUri = "pack://application:,,,/Assets/cards/3/Green.png" },
        new CardViewModel { Color = "Green", Value = "3", ImageUri = "pack://application:,,,/Assets/cards/3/Green.png" },
        new CardViewModel { Color = "Green", Value = "4", ImageUri = "pack://application:,,,/Assets/cards/4/Green.png" },
        new CardViewModel { Color = "Green", Value = "4", ImageUri = "pack://application:,,,/Assets/cards/4/Green.png" },
        new CardViewModel { Color = "Green", Value = "5", ImageUri = "pack://application:,,,/Assets/cards/5/Green.png" },
        new CardViewModel { Color = "Green", Value = "5", ImageUri = "pack://application:,,,/Assets/cards/5/Green.png" },
        new CardViewModel { Color = "Green", Value = "6", ImageUri = "pack://application:,,,/Assets/cards/6/Green.png" },
        new CardViewModel { Color = "Green", Value = "6", ImageUri = "pack://application:,,,/Assets/cards/6/Green.png" },
        new CardViewModel { Color = "Green", Value = "7", ImageUri = "pack://application:,,,/Assets/cards/7/Green.png" },
        new CardViewModel { Color = "Green", Value = "7", ImageUri = "pack://application:,,,/Assets/cards/7/Green.png" },
        new CardViewModel { Color = "Green", Value = "8", ImageUri = "pack://application:,,,/Assets/cards/8/Green.png" },
        new CardViewModel { Color = "Green", Value = "8", ImageUri = "pack://application:,,,/Assets/cards/8/Green.png" },
        new CardViewModel { Color = "Green", Value = "9", ImageUri = "pack://application:,,,/Assets/cards/9/Green.png" },
        new CardViewModel { Color = "Green", Value = "9", ImageUri = "pack://application:,,,/Assets/cards/9/Green.png" },
        new CardViewModel { Color = "Green", Value = "Reverse", ImageUri = "pack://application:,,,/Assets/cards/reverse/Green.png" },
        new CardViewModel { Color = "Green", Value = "Reverse", ImageUri = "pack://application:,,,/Assets/cards/reverse/Green.png" },
        new CardViewModel { Color = "Green", Value = "Skip", ImageUri = "pack://application:,,,/Assets/cards/skip/Green.png" },
        new CardViewModel { Color = "Green", Value = "Skip", ImageUri = "pack://application:,,,/Assets/cards/skip/Green.png" },  
        new CardViewModel { Color = "Green", Value = "+2", ImageUri = "pack://application:,,,/Assets/cards/+2/Green.png" },        
        new CardViewModel { Color = "Green", Value = "+2", ImageUri = "pack://application:,,,/Assets/cards/+2/Green.png" },
        
        new CardViewModel { Color = "Red", Value = "1", ImageUri = "pack://application:,,,/Assets/cards/1/Red.png" },
        new CardViewModel { Color = "Red", Value = "1", ImageUri = "pack://application:,,,/Assets/cards/1/Red.png" },
        new CardViewModel { Color = "Red", Value = "2", ImageUri = "pack://application:,,,/Assets/cards/2/Red.png" },
        new CardViewModel { Color = "Red", Value = "2", ImageUri = "pack://application:,,,/Assets/cards/2/Red.png" },
        new CardViewModel { Color = "Red", Value = "3", ImageUri = "pack://application:,,,/Assets/cards/3/Red.png" },
        new CardViewModel { Color = "Red", Value = "3", ImageUri = "pack://application:,,,/Assets/cards/3/Red.png" },
        new CardViewModel { Color = "Red", Value = "4", ImageUri = "pack://application:,,,/Assets/cards/4/Red.png" },
        new CardViewModel { Color = "Red", Value = "4", ImageUri = "pack://application:,,,/Assets/cards/4/Red.png" },
        new CardViewModel { Color = "Red", Value = "5", ImageUri = "pack://application:,,,/Assets/cards/5/Red.png" },
        new CardViewModel { Color = "Red", Value = "5", ImageUri = "pack://application:,,,/Assets/cards/5/Red.png" },
        new CardViewModel { Color = "Red", Value = "6", ImageUri = "pack://application:,,,/Assets/cards/6/Red.png" },
        new CardViewModel { Color = "Red", Value = "6", ImageUri = "pack://application:,,,/Assets/cards/6/Red.png" },
        new CardViewModel { Color = "Red", Value = "7", ImageUri = "pack://application:,,,/Assets/cards/7/Red.png" },
        new CardViewModel { Color = "Red", Value = "7", ImageUri = "pack://application:,,,/Assets/cards/7/Red.png" },
        new CardViewModel { Color = "Red", Value = "8", ImageUri = "pack://application:,,,/Assets/cards/8/Red.png" },
        new CardViewModel { Color = "Red", Value = "8", ImageUri = "pack://application:,,,/Assets/cards/8/Red.png" },
        new CardViewModel { Color = "Red", Value = "9", ImageUri = "pack://application:,,,/Assets/cards/9/Red.png" },
        new CardViewModel { Color = "Red", Value = "9", ImageUri = "pack://application:,,,/Assets/cards/9/Red.png" },
        new CardViewModel { Color = "Red", Value = "Reverse", ImageUri = "pack://application:,,,/Assets/cards/reverse/Red.png" },
        new CardViewModel { Color = "Red", Value = "Reverse", ImageUri = "pack://application:,,,/Assets/cards/reverse/Red.png" },
        new CardViewModel { Color = "Red", Value = "Skip", ImageUri = "pack://application:,,,/Assets/cards/skip/Red.png" },
        new CardViewModel { Color = "Red", Value = "Skip", ImageUri = "pack://application:,,,/Assets/cards/skip/Red.png" },  
        new CardViewModel { Color = "Red", Value = "+2", ImageUri = "pack://application:,,,/Assets/cards/+2/Red.png" },        
        new CardViewModel { Color = "Red", Value = "+2", ImageUri = "pack://application:,,,/Assets/cards/+2/Red.png" },
        
        new CardViewModel { Color = "Yellow", Value = "1", ImageUri = "pack://application:,,,/Assets/cards/1/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "1", ImageUri = "pack://application:,,,/Assets/cards/1/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "2", ImageUri = "pack://application:,,,/Assets/cards/2/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "2", ImageUri = "pack://application:,,,/Assets/cards/2/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "3", ImageUri = "pack://application:,,,/Assets/cards/3/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "3", ImageUri = "pack://application:,,,/Assets/cards/3/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "4", ImageUri = "pack://application:,,,/Assets/cards/4/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "4", ImageUri = "pack://application:,,,/Assets/cards/4/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "5", ImageUri = "pack://application:,,,/Assets/cards/5/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "5", ImageUri = "pack://application:,,,/Assets/cards/5/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "6", ImageUri = "pack://application:,,,/Assets/cards/6/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "6", ImageUri = "pack://application:,,,/Assets/cards/6/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "7", ImageUri = "pack://application:,,,/Assets/cards/7/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "7", ImageUri = "pack://application:,,,/Assets/cards/7/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "8", ImageUri = "pack://application:,,,/Assets/cards/8/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "8", ImageUri = "pack://application:,,,/Assets/cards/8/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "9", ImageUri = "pack://application:,,,/Assets/cards/9/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "9", ImageUri = "pack://application:,,,/Assets/cards/9/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "Reverse", ImageUri = "pack://application:,,,/Assets/cards/reverse/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "Reverse", ImageUri = "pack://application:,,,/Assets/cards/reverse/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "Skip", ImageUri = "pack://application:,,,/Assets/cards/skip/Yellow.png" },
        new CardViewModel { Color = "Yellow", Value = "Skip", ImageUri = "pack://application:,,,/Assets/cards/skip/Yellow.png" },  
        new CardViewModel { Color = "Yellow", Value = "+2", ImageUri = "pack://application:,,,/Assets/cards/+2/Yellow.png" },        
        new CardViewModel { Color = "Yellow", Value = "+2", ImageUri = "pack://application:,,,/Assets/cards/+2/Yellow.png" },
        
        new CardViewModel { Color = "Wild", Value = "Wild", ImageUri = "pack://application:,,,/Assets/cards/Wild/Wild.png" },
        new CardViewModel { Color = "Wild", Value = "Wild", ImageUri = "pack://application:,,,/Assets/cards/Wild/Wild.png" },  
        new CardViewModel { Color = "Wild", Value = "Wild", ImageUri = "pack://application:,,,/Assets/cards/Wild/Wild.png" },        
        new CardViewModel { Color = "Wild", Value = "Wild", ImageUri = "pack://application:,,,/Assets/cards/Wild/Wild.png" },
        
        new CardViewModel { Color = "Draw", Value = "+4", ImageUri = "pack://application:,,,/Assets/cards/+4/Draw.png" },
        new CardViewModel { Color = "Draw", Value = "+4", ImageUri = "pack://application:,,,/Assets/cards/+4/Draw.png" },  
        new CardViewModel { Color = "Draw", Value = "+4", ImageUri = "pack://application:,,,/Assets/cards/+4/Draw.png" },        
        new CardViewModel { Color = "Draw", Value = "+4", ImageUri = "pack://application:,,,/Assets/cards/+4/Draw.png" },
    };

    public void ShuffleDeck()
    {
        var number = prop.Deck.Count;
        while (number > 1)
        {
            number--;
            var card = _random.Next(number + 1);
            (prop.Deck[card], prop.Deck[number]) = (prop.Deck[number], prop.Deck[card]);
        }
    }

    public List<string> DealCards(int handSize)
    {
        var hand = new List<string>();
        for (prop.Player = 0; prop.Player < handSize; prop.Player++)
        {
            var card = prop.Deck.First();
            prop.Deck.RemoveAt(0);
            hand.Add(card);
        }

        return hand;
    }

    public List<string> PlaceFirstCardInCenter()
    {
        var randomCard = _random.Next(prop.Deck.Count);
        var selectedCard = prop.Deck[randomCard];
        prop.Deck.RemoveAt(randomCard);
        prop.Center.Add(selectedCard);

        return prop.Center;
    }
}