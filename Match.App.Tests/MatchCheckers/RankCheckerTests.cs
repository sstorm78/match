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
    public class RankCheckerTests
    {
        private IChecker _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new RankChecker();
        }


        [Test]
        public void CanCheck_Should_Return_True_If_MatchCondition_Is_Ranks()
        {
            var result = _sut.CanCheck(MatchCondition.Ranks);
            result.Should().BeTrue();
        }

        [Test]
        public void CanCheck_Should_Return_False_If_MatchCondition_Is_Not_Ranks()
        {
            var result = _sut.CanCheck(MatchCondition.Suits);
            result.Should().BeFalse();
        }

        [Test]
        public void IsMatch_Should_Return_False_If_PreviousCard_Is_Null()
        {
            var result = _sut.IsMatch(new Card(Suit.Clubs, Rank.Ace), null);
            result.Should().BeFalse();
        }

        [Test]
        public void IsMatch_Should_Return_False_If_Ranks_Do_Not_Match()
        {
            var result = _sut.IsMatch(new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Clubs, Rank.Eight));
            result.Should().BeFalse();
        }

        [Test]
        public void IsMatch_Should_Return_True_If_Ranks_Match()
        {
            var result = _sut.IsMatch(new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Diamonds, Rank.Ace));
            result.Should().BeTrue();
        }
    }
}
