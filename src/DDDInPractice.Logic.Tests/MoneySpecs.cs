using FluentAssertions;
using System;
using Xunit;

namespace DDDInPractice.Logic.Tests
{
    public class MoneySpecs
    {
        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Constructor_When_PassNegativeValue_Should_ThrowsException(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
        {
            // Arrange
            Action action = () => new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            // Act/Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Sum_When_TwoMoneys_Should_ResultBeCorrect()
        {
            // Arrange
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            var result = money1 + money2;

            // Assert
            result.OneCentCount.Should().Be(2);
            result.TenCentCount.Should().Be(4);
            result.QuarterCount.Should().Be(6);
            result.OneDollarCount.Should().Be(8);
            result.FiveDollarCount.Should().Be(10);
            result.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void Subtract_When_TWoMoneys_Should_ResultBeCorrect()
        {
            // Arrange
            Money money1 = new Money(10, 10, 10, 10, 10, 10);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            var result = money1 - money2;

            // Assert
            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void Subtract_When_TrySubtractMoreThanExists_Should_ThrowException()
        {
            // Arrange
            Money money1 = new Money(0, 1, 0, 0, 0, 0);
            Money money2 = new Money(1, 0, 0, 0, 0, 0);

            // Act
            Action action = () => { Money money = money1 - money2; };

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Equals_When_TwoMoneysContainSameMoneyAmount_Should_BeTrue()
        {
            // Arrange
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            var areEquals = money1 == money2;

            // Assert
            areEquals.Should().BeTrue();
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void Equals_When_TwoMoneysContainDifferentMoneyAmount_ShouldNot_BeTrue()
        {
            // Arrange
            Money money1 = new Money(1, 0, 0, 0, 0, 0);
            Money money2 = new Money(100, 0, 0, 0, 0, 0);

            // Act
            var areEquals = money1 == money2;

            // Assert
            areEquals.Should().BeFalse();
            money1.GetHashCode().Should().NotBe(money2.GetHashCode());
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void Amount_When_Build_ShouldBe_CalculatedCorrectly(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount, decimal expectedAmount)
        {
            // Arrange
            var money = new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            // Act/Assert
            money.Amount.Should().Be(expectedAmount);
        }


    }
}
