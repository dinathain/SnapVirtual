using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Snap.Business_Logic;
using Snap.Entities;
using Snap.Interfaces;

namespace Snap
{
    public class Round
    {
        private readonly IDeck _deck;
        private readonly List<Player> _players;
        private Player _activePlayer;
        public List<Card> MiddlePile = new List<Card>();

        private bool IsRoundWon
        {
            get
            {
                var numberOfPlayersStillIn = _players.Count(g => g.InRound);
                return numberOfPlayersStillIn == 1;
            }
        }

        private bool IsSnap
        {
            get
            {
                var middlePileSize = MiddlePile.Count;
                if (middlePileSize < 2)
                {
                    return false;
                }
                else
                {
                    var card1 = MiddlePile[middlePileSize - 1];
                    var card2 = MiddlePile[middlePileSize - 2];
                    return card1.Face == card2.Face;
                }
            }
        }

        public Player RoundWinner { get; private set; }

        public Round(List<Player> players, IDeck deck)
        {
            _players = players;

            _deck = deck;
            _deck.Shuffle();

            Deal();

            PlayRound();
        }

        private void Deal()
        {
            var j = 0;
            for (int i = 0; i < _deck.DeckLength; i++)
            {
                _players[j].Stack.Add(_deck.DealCard());
                j++;
                if (j == _players.Count)
                {
                    j = 0;
                }
            }
        }

        public void PlayRound()
        {
            for (var i = 0; i < _players.Count; i++)
            {
                if (_players.Count(s => s.InRound) == 0)
                {
                    break;
                }
                if (_players[i].InRound)
                {
                    _activePlayer = _players[i];

                    //play
                    PlayCard(_activePlayer);
                    var isSnap = IsSnap;
                    var reactions = new CalculateReactions(isSnap, _players);

                    //handle reactions
                    if (reactions.Any)
                        HandleReactions(reactions);

                    //Check if round is over
                    if (IsRoundWon)
                    {
                        RoundWinner = _players.Find(g => g.InRound);
                        Console.WriteLine("Congratulations {0}! You have won this  roundofSnap!", RoundWinner.PlayerName);
                        break;
                    }
                }

                if (i == (_players.Count - 1)) i = -1;
            }
        }

        //Active player takes top card from their own pile and reveals, placing onto "middle pile"
        public void PlayCard(Player player)
        {
            var cardToPlay = player.Stack.First();
            MiddlePile.Add(cardToPlay);
            player.Stack.RemoveAt(0);
            Console.WriteLine(cardToPlay.Print());
        }

        private void HandleReactions(CalculateReactions reactions)
        {
            var fastest = reactions.FirstToReact;

            RightAlign();
            Console.WriteLine("{0} yelled SNAP!", fastest.Player.PlayerName);

            RightAlign();
            if (reactions.FirstToReact.CorrectReaction)
            {
                SuccessfulSnap(fastest.Player);
            }
            if (!fastest.CorrectReaction)
            {
                FalseSnap(fastest.Player);
            }

            StateCardsPerPlayer(); //Show latest card stats

            RightAlign();
            Console.WriteLine("Replaying in 3 seconds...");
            Thread.Sleep(3000);
        }

        //Quick stats for user to gauge progress
        private void StateCardsPerPlayer()
        {
            RightAlign();
            Console.WriteLine("Cards remaining:");
            foreach (var player in _players)
            {
                RightAlign();
                if (player.InRound)
                {
                    Console.WriteLine("{0}: {1}", player.PlayerName, player.StackSize);
                }
                else
                {
                    Console.WriteLine("{0} is OUT of this round.", player.PlayerName);
                }
            }
        }

        public void FalseSnap(Player loser)
        {
            Console.WriteLine("Bad luck... that was not a SNAP. {0} gives a card to all other players!", loser.PlayerName);
            foreach (var player in _players)
            {
                if (player != loser)
                {
                    while (loser.StackSize > 0)
                    {
                        player.Stack.Add(loser.Stack.ElementAt(0));
                        loser.Stack.RemoveAt(0);
                    }
                }
            }
        }

        public void SuccessfulSnap(Player player)
        {
            Console.WriteLine("{0} takes all cards drawn so far!", player.PlayerName);
            var selected = MiddlePile.ToList();
            player.Stack.AddRange(selected);
            MiddlePile.RemoveRange(0, MiddlePile.Count - 1);
        }

        //Creates indent for key messages throughout game, avoids confusion with column of cards
        private void RightAlign()
        {
            Console.CursorLeft = 20;
        }
    }
}