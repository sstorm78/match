using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Match.App.MatchCheckers;
using Match.App.Models;
using NUnit.Framework;

namespace Match.App.Tests.MatchCheckers
{
    [TestFixture]
    public class SuitCheckerTests
    {
        private IChecker _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SuitChecker();
        }

        [Test]
        public void CanCheck_Should_Return_True_If_MatchCondition_Is_Suits()
        {
            var result = _sut.CanCheck(MatchCondition.Suits);
            result.Should().BeTrue();
        }

        [Test]
        public void CanCheck_Should_Return_False_If_MatchCondition_Is_Not_Suit()
        {
            var result = _sut.CanCheck(MatchCondition.Ranks);
            result.Should().BeFalse();
        }

        [Test]
        public void IsMatch_Should_Return_False_If_PreviousCard_Is_Null()
        {
            var result = _sut.IsMatch(new Card(Suit.Clubs, Rank.Ace), null);
            result.Should().BeFalse();
        }

        [Test]
        public void IsMatch_Should_Return_False_If_Suits_Do_Not_Match()
        {
            var result = _sut.IsMatch(new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Hearts, Rank.Ace));
            result.Should().BeFalse();
        }

        [Test]
        public void IsMatch_Should_Return_True_If_Suits_Match()
        {
            var result = _sut.IsMatch(new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Clubs, Rank.Ace));
            result.Should().BeTrue();
        }
    }
}
