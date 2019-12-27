using System;
using System.Collections.Generic;

namespace LWTech.KevinBlair.Assignment5
{
    public abstract class Player
    {
        public string Name { get; private set; }
        public Hand Hand { get; private set; }
        private int score = 0;
        // Other Properties/member variables go here

        public Player(string name)
        {
            if (name.ToCharArray(0, name.Length) == null || name.ToCharArray(0, name.Length).Equals(""))
            {
                throw new ArgumentOutOfRangeException("Each player must have a name.");
            }
            this.Name = name;
            this.Hand = new Hand();
        }

        public abstract Player ChoosePlayerToAsk(Player[] players);
        public abstract Rank ChooseRankToAskFor();
        public bool HasCards(Rank rank)
        {
            for (int i = 0; i < this.Hand.Size(); i++)
            {
                if (Hand.getRank(i) == rank)
                {
                    return true;
                }
            }
            return false;
        }
        public void GiveCards(Player p, Rank r)
        {
            Hand h = new Hand();
            for (int i = 0; i < this.Hand.Size(); i++)
            {
                if (this.Hand.GetCard(i).rank == r)
                {
                    Console.WriteLine($"{p.Name} gets the {this.Hand.GetCard(i)} from {this.Name}");
                    h.AddCard(this.Hand.GetCard(i));
                    this.Hand.RemoveCard(this.Hand.GetCard(i));
                    i = -1;
                }

            }
            for (int i = 0; i < h.Size(); i++)
            {
                p.Hand.AddCard(h.GetCard(i));
            }
            h = null;
        }
        public void IncreaseScore()
        {
            this.score++; ;
        }

        public int GetScore()
        {
            return score;
        }

        // Other Player methods go here

        public override string ToString()
        {
            string s = Name + "'s Hand: ";
            s += Hand.ToString();
            return s;
        }
    }

    public class PlayerRandom : Player
    {
        static Random rand = new Random();

        public PlayerRandom(string name) : base(name + "(RND)")
        {
            if (name.ToCharArray(0, name.Length) == null || name.ToCharArray(0, name.Length).Equals(""))
            {
                throw new ArgumentOutOfRangeException("Each player must have a name.");
            }
        }
        public override Player ChoosePlayerToAsk(Player[] players)
        {

            int rng = rand.Next(players.Length - 1);
            while (players[rng].Name.Equals(this.Name))
                rng = rand.Next(players.Length - 1);
            return players[rng];
        }
        public override Rank ChooseRankToAskFor()
        {
            int rng = rand.Next(Hand.Size() - 1);
            Card c = Hand.GetCard(rng);
            return c.rank;
        }
    }

    public class RightSidePlayer : Player
    {
        static Random rand = new Random();

        public RightSidePlayer(string name) : base(name + "(RSP)")
        {
            if (name.ToCharArray(0, name.Length) == null || name.ToCharArray(0, name.Length).Equals(""))
            {
                throw new ArgumentOutOfRangeException("Each player must have a name.");
            }
        }
        public override Player ChoosePlayerToAsk(Player[] players)
        {
            int i = players.Length - 1;
            while (players[i].Hand.Size() == 0)
            {
                i++;
                if (i > players.Length - 1)
                {
                    i = 0;
                }
                else if (players[i] == this)
                {
                    i++;
                    if (i > players.Length - 1)
                    {
                        i = 0;
                    }
                }
            }

            return players[i];
        }
        public override Rank ChooseRankToAskFor()
        {
            int i = Hand.Size() - 1;
            Card c = Hand.GetCard(i);
            return c.rank;
        }
    }
    public class LeftSidePlayer : Player
    {
        static Random rand = new Random();

        public LeftSidePlayer(string name) : base(name + "(LSP)")
        {
            if (name.ToCharArray(0, name.Length) == null || name.ToCharArray(0, name.Length).Equals(""))
            {
                throw new ArgumentOutOfRangeException("Each player must have a name.");
            }
        }
        public override Player ChoosePlayerToAsk(Player[] players)
        {

            int i = 0;
            while (players[i].Hand.Size() == 0)
            {
                i--;
                if (i < 0)
                {
                    i = players.Length - 1;
                }
                else if (players[i] == this)
                {
                    i--;
                    if (i < 0)
                    {
                        i = players.Length - 1;
                    }
                }
            }

            return players[i];
        }
        public override Rank ChooseRankToAskFor()
        {
            int i = 0;
            Card c = Hand.GetCard(i);
            return c.rank;
        }
    }
    public class RightSideRandomPlayer : Player
    {
        static Random rand = new Random();

        public RightSideRandomPlayer(string name) : base(name + "(RSRP)")
        {
            if (name.ToCharArray(0, name.Length) == null || name.ToCharArray(0, name.Length).Equals(""))
            {
                throw new ArgumentOutOfRangeException("Each player must have a name.");
            }
        }
        public override Player ChoosePlayerToAsk(Player[] players)
        {

            int rng = rand.Next(players.Length - 1);
            while (players[rng].Equals(this))
                rng = rand.Next(players.Length - 1);
            return players[rng];
        }
        public override Rank ChooseRankToAskFor()
        {
            int i = Hand.Size() - 1;
            Card c = Hand.GetCard(i);
            return c.rank;
        }
    }
}
