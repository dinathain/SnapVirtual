using Snap.Entities;

namespace Snap.Interfaces
{
    public interface IDeck
    {
        void Shuffle();

        Card DealCard();

        int DeckLength { get; }
    }
}