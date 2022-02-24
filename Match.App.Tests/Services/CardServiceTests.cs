using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Match.App.Models;
using Match.App.Services;
using Moq;
using NUnit.Framework;

namespace Match.App.Tests.Services
{
    [TestFixture]
    public class CardServiceTests
    {
        private Mock<IRandomService> _mockRandomService;

        [SetUp]
        public void Setup()
        {
            _mockRandomService = new Mock<IRandomService>();
        }

        [TestCase(1,52)]
        [TestCase(2, 104)]
        [TestCase(3, 156)]
        public void DrawCards_Should_Return_Expected_Number_Of_Cards(int packs, int expectedCards)
        {
            var sut = new CardService(_mockRandomService.Object);
            var result = sut.DrawCards(packs);
            result.Count.Should().Be(expectedCards);
        }

        [Test]
        public void DrawCards_Should_Return_Expected_Set_Of_Cards()
        {
            var sut = new CardService(_mockRandomService.Object);
            var result = sut.DrawCards(1);
            foreach (var suit in (Suit[])Enum.GetValues(typeof(Suit)))
            {
                foreach (var rank in (Rank[])Enum.GetValues(typeof(Rank)))
                {
                    var contain = result.Any(i => i.Suit == suit && i.Rank == rank);
                    contain.Should().BeTrue();
                }
            }
        }

        [Test]
        public void DrawCards_Should_Return_Expected_Order_Of_Cards()
        {
            _mockRandomService.SetupSequence(i => i.Next(It.IsAny<int>()))
                .Returns(1)
                .Returns(2)
                .Returns(3);

            var sut = new CardService(_mockRandomService.Object);
            var result = sut.DrawCards(1);

            result.First().Rank.Should().Be(Rank.King);
            result.First().Suit.Should().Be(Suit.Diamonds);

            result[1].Rank.Should().Be(Rank.Queen);
            result[1].Suit.Should().Be(Suit.Diamonds);

            result[2].Rank.Should().Be(Rank.Jack);
            result[2].Suit.Should().Be(Suit.Diamonds);

        }
    }
}
