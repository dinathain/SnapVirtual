namespace Snap.Entities
{
    public class Card
    {
        public Card(string face, string suit)
        {
            Face = face;
            Suit = suit;
        }

        public string Print()
        {
            return Face + " of " + Suit;
        }

        public string Face { get; set; }
        public string Suit { get; set; }
    }
}