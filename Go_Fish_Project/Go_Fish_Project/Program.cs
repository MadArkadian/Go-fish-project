using System;
using System.Collections.Generic;

namespace Go_Fish_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Deck deckOfCards = new Deck();
            Console.WriteLine("Go Fish Simulation (Multiplayer) - By Kevin Blair");
            Console.WriteLine("===================================================");
            Console.WriteLine();


            //Console.WriteLine($"New deck \n {deckOfCards}");
            //Console.WriteLine();


            deckOfCards.Shuffle();


            // Console.WriteLine($"After shuffling \n {deckOfCards}");
            // Console.WriteLine();


            deckOfCards.Cut();

           


            // Console.WriteLine($"After cutting the deck \n {deckOfCards}");           
            //  Console.WriteLine();

            Player[] players = new Player[4];

            players[0] = new PlayerRandom("John");
            players[1] = new RightSidePlayer("Kate");
            players[2] = new LeftSidePlayer("Iris");
            players[3] = new RightSideRandomPlayer("Jessica");


            for (int i = 0; i < 5; i++)
            {
                foreach (Player player in players)
                {
                    Card c = deckOfCards.Deal();
                    player.Hand.AddCard(c);
                }
            }
            bool gameOver = false;
            int playerTurn = 0;
            string lastPlayer = "";
            int round = 1;
            Player currentPlayer;
            while (!gameOver)
            {
                int gameFullyOver = 0;
                if (playerTurn == 4)
                {
                    playerTurn = 0;
                }
                currentPlayer = players[playerTurn];
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].Hand.Size() == 0 && deckOfCards.Size() == 0)
                    {
                        gameFullyOver++;
                    }
                }
                if (gameFullyOver == 4)
                {
                    gameOver = true;
                    continue;
                }
                if (currentPlayer.Hand.Size() == 0)
                {
                    if (deckOfCards.Size() == 0)
                    {
                        playerTurn++;
                        continue;
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (deckOfCards.Size() != 0)
                            {
                                Card c = deckOfCards.Deal();
                                currentPlayer.Hand.AddCard(c);
                            }
                        }
                    }
                }
                if (lastPlayer != currentPlayer.Name)
                {
                    Console.WriteLine($"It is now {currentPlayer.Name} turn.");
                }
                else if (lastPlayer == currentPlayer.Name)
                {
                    Console.WriteLine($"It is still {currentPlayer.Name}'s turn.");
                }

                lastPlayer = currentPlayer.Name;
                Rank r = currentPlayer.ChooseRankToAskFor();
                Player p = currentPlayer.ChoosePlayerToAsk(players);
                int hasBookCounter = 0;
                if (p.Hand.Size() == 0)
                {
                    continue;
                }
                Console.WriteLine($"{currentPlayer.Name} says: {p.Name} do you have any {r}s?");
                if (p.HasCards(r))
                {
                    hasBookCounter = 0;
                    p.GiveCards(currentPlayer, r);
                    for (int i = 0; i < currentPlayer.Hand.Size(); i++)
                    {
                        if (currentPlayer.Hand.getRank(i) == r)
                        {
                            hasBookCounter++;

                        }
                    }
                    if (hasBookCounter == 4)
                    {
                        Console.WriteLine($">>> {currentPlayer.Name} HAS A BOOK! PLAYING A BOOK OF {r}s");
                        Console.WriteLine();
                        currentPlayer.IncreaseScore();
                        for (int i = 0; i < currentPlayer.Hand.Size(); i++)
                        {
                            if (currentPlayer.Hand.GetCard(i).rank == r)
                            {
                                Card c = currentPlayer.Hand.GetCard(i);
                                currentPlayer.Hand.RemoveCard(c);
                                i = -1;
                            }
                        }
                    }
                    continue;
                }
                else
                {
                    Console.WriteLine($"{p.Name} says: GO FISH!");
                    if (deckOfCards.Size() == 0)
                    {
                        Console.WriteLine($"Deck is empty. {currentPlayer.Name} cannot draw a card.");
                        Console.WriteLine($"{currentPlayer.Name}'s turn is over. {currentPlayer.Hand}");
                        Console.WriteLine($"SCORE: | {players[0].Name}: {players[0].GetScore()} | {players[1].Name}: {players[1].GetScore()} | {players[2].Name}: {players[2].GetScore()} | {players[3].Name}: {players[3].GetScore()} | [DECK: {deckOfCards.Size()}]");
                        Console.WriteLine();
                        playerTurn++;
                        round++;
                        continue;
                    }
                    Card c = deckOfCards.Deal();
                    Console.WriteLine($"{currentPlayer.Name} draws a {c} from the deck. The deck now has {deckOfCards.Size()} cards remaining");
                    currentPlayer.Hand.AddCard(c);
                    hasBookCounter = 0;
                    for (int i = 0; i < currentPlayer.Hand.Size(); i++)
                    {

                        if (currentPlayer.Hand.getRank(i) == c.rank)
                        {
                            hasBookCounter++;
                        }
                    }
                    if (hasBookCounter == 4)
                    {
                        Console.WriteLine($"{currentPlayer.Name} HAS A BOOK! PLAYING A BOOK OF {c.rank}s");
                        currentPlayer.IncreaseScore();

                        hasBookCounter = 0;
                        for (int i = 0; i < currentPlayer.Hand.Size(); i++)
                        {
                            if (currentPlayer.Hand.GetCard(i).rank == c.rank)
                            {
                                c = currentPlayer.Hand.GetCard(i);
                                currentPlayer.Hand.RemoveCard(c);
                                i = -1;
                            }
                        }
                    }
                    if (c.rank == r)
                    {
                        continue;
                    }

                    Console.WriteLine($"{currentPlayer.Name}'s turn is over. {currentPlayer.Hand}");
                    playerTurn++;
                    Console.WriteLine($"SCORE: | {players[0].Name}: {players[0].GetScore()} | {players[1].Name}: {players[1].GetScore()} | {players[2].Name}: {players[2].GetScore()} | {players[3].Name}: {players[3].GetScore()} | [DECK: {deckOfCards.Size()}]");
                    Console.WriteLine();
                    continue;

                }


                // gameOver = true;
            }
            Console.WriteLine();
            Console.WriteLine("========================= GAME OVER! =============================");
            Console.WriteLine();
            Console.WriteLine($"SCORE: | {players[0].Name}: {players[0].GetScore()} | {players[1].Name}: {players[1].GetScore()} | {players[2].Name}: {players[2].GetScore()} | {players[3].Name}: {players[3].GetScore()} | [DECK: {deckOfCards.Size()}]");
            Console.WriteLine();

            int highestTotalBooks = 0;
            int index = 0;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetScore() > highestTotalBooks)
                {
                    highestTotalBooks = players[i].GetScore();
                    index = i;
                }
            }
            Console.WriteLine($"After {round} turns,");
            Console.WriteLine($"The winner is {players[index].Name} with a total score of {players[index].GetScore()}");



            Console.ReadLine();



        }
    }
}
