using System;
using System.Collections.Generic;

namespace LWTech.KevinBlair.Assignment5
{
    public class Deck
    {

        List<Card> swap;
        List<Card> cardDeck;
        Stack<Card> deck;
        private static Random _random = new Random();
        public Deck()
        {
            cardDeck = new List<Card>();
            deck = new Stack<Card>();
            Array suits = Enum.GetValues(typeof(Suit));
            Array ranks = Enum.GetValues(typeof(Rank));
            foreach (Suit suit in suits)
            {
                foreach (Rank rank in ranks)
                {
                    cardDeck.Add(new Card(suit, rank));
                }
            }
        }



        //the fisher yates shuffling algorithm found here: https://www.dotnetperls.com/fisher-yates-shuffle
        public void Shuffle()
        {
            int n = cardDeck.Count;
            for (int i = 0; i < n; i++)
            {
                // Use Next on random instance with an argument.
                // ... The argument is an exclusive bound.
                //     So we will not go past the end of the array.
                int r = i + _random.Next(n - i);
                Card t = cardDeck[r];
                cardDeck[r] = cardDeck[i];
                cardDeck[i] = t;
            }
        }

        public void Cut()
        {
            int cutSpot = _random.Next(51) + 1;
            swap = new List<Card>(cutSpot);
            int x = cardDeck.Count - cutSpot;

            for (int i = 0; i < cutSpot; i++)
            {
                swap.Add(cardDeck[i]);
            }

            for (int j = 0; j < x; j++)
            {
                cardDeck[j] = cardDeck[j + cutSpot];
            }

            for (int i = 0; i < cutSpot; i++)
            {
                cardDeck[i + x] = swap[i];
            }

            foreach (Card c in cardDeck)
            {
                deck.Push(c);
            }


        }

        public Card Deal()
        {
            /*Card c = cardDeck[Size() - 1];
            cardDeck.Remove(c);*/
            Card c = deck.Pop();
            return c;
        }

        public int Size()
        {
            return deck.Count;
        }

        public override string ToString()
        {
            string s = "[";
            foreach (Card card in deck)
            {
                s += "[" + card.rank + " of " + card.suit + "], ";
            }
            s += "]";
            return s;
        }
    }
}
