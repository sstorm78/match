using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Match.App.MatchCheckers;
using Match.App.Models;
using Match.App.Services;
using Moq;
using NUnit.Framework;

namespace Match.App.Tests.Services
{
    [TestFixture]
    public class MatchServiceTests
    {
        private Mock<IRandomService> _mockRandomService;

        [SetUp]
        public void Setup()
        {
            _mockRandomService = new Mock<IRandomService>();
        }

        [Test] 
        public void Play_Should_Return_Expected_Draw_Result()
        {
            var cards = new List<Card>
                        {
                            new Card(Suit.Clubs, Rank.Two),
                            new Card(Suit.Clubs, Rank.Three),
                            new Card(Suit.Clubs, Rank.Four),
                            new Card(Suit.Clubs, Rank.Five)
                        };

            var checkers = new List<IChecker>
                           {
                               new RankChecker()
                           };

            var sut = new MatchService(checkers, _mockRandomService.Object);
            sut.SetNumberOfPlayers(2);

            var result = sut.Play(cards);

            var expectedResult = new StringBuilder();
            expectedResult.AppendLine("");
            expectedResult.AppendLine("Game Results!");
            expectedResult.AppendLine("Player 1: 0 cards");
            expectedResult.AppendLine("Player 2: 0 cards");
            expectedResult.AppendLine("The Draw!");

            result.Should().Be(expectedResult.ToString());
        }

        [Test]
        public void Play_Should_Return_Player_1_as_The_Winner()
        {
            var cards = new List<Card>
                        {
                            new Card(Suit.Clubs, Rank.Two),
                            new Card(Suit.Clubs, Rank.Three),
                            new Card(Suit.Clubs, Rank.Four),
                            new Card(Suit.Clubs, Rank.Five)
                        };

            var checkers = new List<IChecker>
                           {
                               new SuitChecker()
                           };

            _mockRandomService.SetupSequence(i => i.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);

            var sut = new MatchService(checkers, _mockRandomService.Object);
            sut.SetNumberOfPlayers(2);

            var result = sut.Play(cards);

            var expectedResult = new StringBuilder();
            expectedResult.AppendLine("");
            expectedResult.AppendLine("Game Results!");
            expectedResult.AppendLine("Player 1: 1 cards");
            expectedResult.AppendLine("Player 2: 0 cards");
            expectedResult.AppendLine("The Winner is Player 1");

            result.Should().Be(expectedResult.ToString());
        }

        [Test]
        public void Play_Should_Return_Player_2_as_The_Winner()
        {
            var cards = new List<Card>
                        {
                            new Card(Suit.Clubs, Rank.Two),
                            new Card(Suit.Clubs, Rank.Three),
                            new Card(Suit.Clubs, Rank.Four),
                            new Card(Suit.Clubs, Rank.Five)
                        };

            var checkers = new List<IChecker>
                           {
                               new SuitChecker()
                           };

            _mockRandomService.SetupSequence(i => i.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(1);

            var sut = new MatchService(checkers, _mockRandomService.Object);
            sut.SetNumberOfPlayers(2);

            var result = sut.Play(cards);

            var expectedResult = new StringBuilder();
            expectedResult.AppendLine("");
            expectedResult.AppendLine("Game Results!");
            expectedResult.AppendLine("Player 1: 0 cards");
            expectedResult.AppendLine("Player 2: 1 cards");
            expectedResult.AppendLine("The Winner is Player 2");

            result.Should().Be(expectedResult.ToString());
        }
        
        [Test]
        public void SetNumberOfPlayers_Should_Set_Number_Of_Players_And_Play_Should_Return_Expected_Result_For_Three_Players()
        {
            var cards = new List<Card>
                        {
                            new Card(Suit.Clubs, Rank.Two),
                            new Card(Suit.Clubs, Rank.Three),
                            new Card(Suit.Clubs, Rank.Four),
                            new Card(Suit.Clubs, Rank.Five)
                        };

            var checkers = new List<IChecker>
                           {
                               new RankChecker()
                           };

            var sut = new MatchService(checkers, _mockRandomService.Object);
            sut.SetNumberOfPlayers(3);

            var result = sut.Play(cards);

            var expectedResult = new StringBuilder();
            expectedResult.AppendLine("");
            expectedResult.AppendLine("Game Results!");
            expectedResult.AppendLine("Player 1: 0 cards");
            expectedResult.AppendLine("Player 2: 0 cards");
            expectedResult.AppendLine("Player 3: 0 cards");
            expectedResult.AppendLine("The Draw!");

            result.Should().Be(expectedResult.ToString());
        }
    }
}
