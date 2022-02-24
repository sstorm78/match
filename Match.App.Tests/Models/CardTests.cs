using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Match.App.Models;
using NUnit.Framework;

namespace Match.App.Tests.Models
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void Constructor_Should_Populate_Model_Properties()
        {
            var result = new Card(Suit.Clubs, Rank.Five);
            result.Suit.Should().Be(Suit.Clubs);
            result.Rank.Should().Be(Rank.Five);
        }
    }
}
