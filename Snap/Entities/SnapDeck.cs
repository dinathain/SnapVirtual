using System;
using Snap.Interfaces;

namespace Snap.Entities
{
    public class SnapDeck : IDeck
    {
        public Card[] Deck;
        private const int NumberOfCards = 52;
        private int _currentCard;
        private readonly Random _random;

        public int DeckLength => Deck.Length;

        public SnapDeck()
        {
            string[] faces = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            string[] suits = { "Hearts", "Diamonds", "Spades", "Clubs" };

            Deck = new Card[NumberOfCards];
            _currentCard = 0;
            _random = new Random();

            //Fill deck
            for (int i = 0; i < DeckLength; i++)
            {
                Deck[i] = new Card(faces[i % 11], suits[i / 13]);
            }
        }

        public void Shuffle()
        {
            _currentCard = 0;
            for (int first = 0; first < DeckLength; first++)
            {
                int second = _random.Next(NumberOfCards);
                Card temp = Deck[first];
                Deck[first] = Deck[second];
                Deck[second] = temp;
            }
        }

        public Card DealCard()
        {
            if (_currentCard < DeckLength)
                return Deck[_currentCard++];
            else return null;
        }
    }
}