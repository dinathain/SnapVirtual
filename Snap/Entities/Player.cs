using System.Collections.Generic;

namespace Snap.Entities
{
    public class Player
    {
        public string PlayerName { get; set; }
        public bool IsVirtual { get; set; }
        public List<Card> Stack;
        public int NumberOfWins { get; set; }

        public Player(string playerName, bool isVirtual)
        {
            PlayerName = playerName;
            IsVirtual = isVirtual;
            Stack = new List<Card>();
            NumberOfWins = 0;
        }

        public int StackSize => Stack.Count;

        public bool InRound
        {
            get
            {
                if (StackSize == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}