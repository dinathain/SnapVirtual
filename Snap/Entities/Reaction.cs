namespace Snap.Entities
{
    public class Reaction
    {
        public Reaction(long reactionTime, Player player, bool isSnap)
        {
            Player = player;
            ReactionTime = reactionTime;

            if (ReactionTime != 0)
            {
                IsPlayerReacted = true;
            }

            if (isSnap && IsPlayerReacted)
            {
                CorrectReaction = true;
            }

            if (!isSnap && !IsPlayerReacted)
            {
                CorrectReaction = true;
            }
        }

        public bool IsPlayerReacted { get; set; }
        public long ReactionTime { get; set; }
        public Player Player { get; set; }
        public bool CorrectReaction { get; set; }
    }
}