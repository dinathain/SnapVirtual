using Moq;
using Xunit;
using Snap;
using Snap.Entities;
using System.Collections.Generic;
using System.Linq;
using Snap.Interfaces;

namespace SnapVirtual.UnitTests
{
    public class RoundTest
    {
        private Round _sut;
        private readonly Mock<IDeck> _mockIDeck;

        public RoundTest()
        {
            _mockIDeck = new Mock<IDeck>();
        }

        [Fact]
        public void Round_Created_DeckShuffleInvoked()
        {
            //Arrange
            var players = new List<Player>
            {
                new Player("1", true),
                new Player("2", false)
            };

            //Act
            _sut = new Round(players, _mockIDeck.Object);

            _mockIDeck.Verify(m => m.Shuffle(), Times.Once());
        }

        [Fact]
        public void Round_Created_SnapDeckUnique()
        {
            //Arrange
            var players = new List<Player>();
            for (int i = 0; i < 12; i++)
            {
                players.Add(new Player("i", true));
            }
            var mockDeck = new SnapDeck();
            var listOfCards = new List<Card>();

            //Act
            _sut = new Round(players, mockDeck);
            foreach (var card in mockDeck.Deck)
            {
                listOfCards.Add(card);
            }

            //Assert
            var isUnique = listOfCards.Distinct().Count() == listOfCards.Count();
            Assert.True(isUnique); //deck is entirely unique
        }
    }
}