using System;
using System.Collections.Generic;
using Snap.Entities;
using Snap.Interfaces;

namespace Snap
{
    public class Game
    {
        private IDeck _deck;
        public bool UserWantsToPlay;

        public List<Player> Players { get; set; }

        public Game(string playerName, int extraPlayers, IDeck deck)
        {
            UserWantsToPlay = true;

            _deck = deck;

            //Players
            Players = new List<Player>();
            Players.Add(new Player(playerName, false));
            for (int i = 1; i <= extraPlayers; i++)
            {
                Players.Add(new Player("Player " + i, true));
            }
        }

        public void PlayGame()
        {
            while (UserWantsToPlay)
            {
                var round = new Round(Players, _deck);
                round.RoundWinner.NumberOfWins++;
                StateWinsPerPlayer();
                Console.WriteLine("Play again? Press Enter to play or Esc to stop.");
                var response = Console.ReadKey(true).Key;
                if (response == ConsoleKey.Escape)
                {
                    UserWantsToPlay = false;
                }
            }
        }

        private void StateWinsPerPlayer()
        {
            Console.CursorLeft = 20;
            Console.WriteLine("Rounds won per player:");
            foreach (var player in Players)
            {
                Console.CursorLeft = 20;
                Console.WriteLine("{0}: {1}", player.PlayerName, player.NumberOfWins);
            }
        }
    }
}