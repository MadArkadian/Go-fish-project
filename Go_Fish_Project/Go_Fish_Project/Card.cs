using System;
using System.Collections.Generic;

namespace Go_Fish_Project
{
    public enum Suit
    {
        Club, Diamond, Spade, Heart
    }
    public enum Rank
    {
        Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Eleven, Jack, Queen, King
    }
    public class Card
    {
        public Suit suit { get; private set; }
        public Rank rank { get; private set; }
        public Card(Suit Suit, Rank Rank)
        {
            this.rank = Rank;
            this.suit = Suit;
        }
        public override string ToString()
        {
            return "[" + rank + " of " + suit + "]";
        }
    }
}
