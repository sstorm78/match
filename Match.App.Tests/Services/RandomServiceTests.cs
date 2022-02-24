using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Match.App.Services;
using NUnit.Framework;

namespace Match.App.Tests.Services
{
    [TestFixture]
    public class RandomServiceTests
    {
        [Test]
        public void Next_Should_Return_Number_Less_Than_5()
        {
            var sut = new RandomService();
            var result = sut.Next(6);
            result.Should().BeLessOrEqualTo(5);
        }

        [Test]
        public void Next_Should_Return_Number_More_Than_1_And_Less_Than_5()
        {
            var sut = new RandomService();
            var result = sut.Next(1, 6);
            result.Should().BeLessOrEqualTo(5);
            result.Should().BeGreaterOrEqualTo(1);
        }
    }
}
