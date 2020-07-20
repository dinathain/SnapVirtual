using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Snap.Entities;

namespace Snap.Business_Logic
{
    public class CalculateReactions
    {
        private readonly List<Reaction> _reactions = new List<Reaction>();
        public Reaction FirstToReact { get; private set; }
        public bool Any { get; private set; }

        public CalculateReactions(bool isSnap, List<Player> players)
        {
            //Create and calculate reactions
            foreach (var player in players)
            {
                if (!player.InRound)
                {
                    continue;
                }
                var reaction = !player.IsVirtual ? MainPlayerReaction(player, isSnap) : new Reaction(RandomReactionTime(isSnap), player, isSnap);
                _reactions.Add(reaction);
            }

            CalculateFirstToReact();
            CountReactions();
        }

        private Reaction MainPlayerReaction(Player player, bool isSnap)
        {
            Reaction reaction = new Reaction(0, player, isSnap);
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < 2; i++)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.S)
                    {
                        watch.Stop();
                        reaction = new Reaction(watch.ElapsedMilliseconds, player, isSnap);
                    }
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            return reaction;
        }

        private long RandomReactionTime(bool isSnap)
        {
            Random random = new Random();
            var normalReactionTime = random.Next(1000, 1500);
            var rand = random.NextDouble();
            if (isSnap)
            {
                return normalReactionTime;
            }
            else
            {
                if (rand > .995) // small probability that virtual player will react if no snap exists
                {
                    return normalReactionTime;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void CalculateFirstToReact()
        {
            Reaction fastest = new Reaction(Int32.MaxValue, null, true);
            for (int i = 0; i < _reactions.Count; i++)
            {
                if (_reactions[i].IsPlayerReacted && (_reactions[i].ReactionTime < fastest.ReactionTime))
                    fastest = _reactions[i];
            }

            FirstToReact = fastest;
        }

        private void CountReactions()
        {
            var numberOfReactions = _reactions.Count(g => g.IsPlayerReacted);
            Any = numberOfReactions > 0;
        }
    }
}