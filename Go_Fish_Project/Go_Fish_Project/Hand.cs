using System;
using System.Collections.Generic;

namespace Go_Fish_Project
{
    public class Hand
    {
        private List<Card> cards;
        public Hand()
        {
            cards = new List<Card>();
        }

        public int Size()
        {
            return cards.Count;
        }

        public void AddCard(Card c)
        {
            cards.Add(c);
        }

        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        public Card GetCard(int num)
        {
            return cards[num];
        }

        public Rank getRank(int i)
        {
            return cards[i].rank;
        }

        public override string ToString()
        {
            string s = "[";
            foreach (Card card in cards)
            {
                s += "[" + card.rank + " of " + card.suit + "], ";
            }
            s += "]";
            return s;
        }
    }
}
