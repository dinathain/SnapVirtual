using Moq;
using Xunit;
using Snap;
using Snap.Interfaces;

namespace SnapVirtual.UnitTests
{
    public class GameTest
    {
        private Game _sut;
        private readonly Mock<IDeck> _mockIDeck;
        private readonly Mock<Round> _mockRound;

        public GameTest()
        {
            _mockIDeck = new Mock<IDeck>();
            _mockRound = new Mock<Round>();
        }

        [Fact]
        public void Game_Created_AddsPlayers()
        {
            //Act
            _sut = new Game("", 2, _mockIDeck.Object);

            //Assert
            Assert.Equal(3, _sut.Players.Count);
        }
    }
}