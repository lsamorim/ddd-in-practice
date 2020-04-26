using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DDDInPractice.Logic.Tests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void InsertMoney_When_PassMoney_Should_ChangeMoneyInTransactionCorrectly()
        {
            // Arrange
            var sut = new SnackMachine();

            // Act
            sut.InsertMoney(Money.Cent);
            sut.InsertMoney(Money.Dollar);

            // Assert
            sut.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void InsertMoney_When_PassMoreThanOneCentOrDolar_Should_ThowInvalidOperationException()
        {
            // Arrange
            var sut = new SnackMachine();
            var twoDollar = Money.Dollar + Money.Dollar;

            // Act
            Action action = () => sut.InsertMoney(twoDollar);

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ReturnMoney_When_NotContainsMoneyTransaction_Should_KeepsMoneyInTransactionZero()
        {
            // Arrange
            var sut = new SnackMachine();
            sut.InsertMoney(Money.Dollar);

            // Act
            sut.ReturnMoney();

            // Assert
            sut.MoneyInTransaction.Amount.Should().Be(0);
        }

        [Fact]
        public void BuySnack_When_Confirmed_Should_AddMoneyInTransactionToMoneyInside()
        {
            // Arrange
            var sut = new SnackMachine();
            sut.InsertMoney(Money.Dollar);
            sut.InsertMoney(Money.Dollar);

            // Act
            sut.BuySnack();

            // Assert
            sut.MoneyInTransaction.Should().Be(Money.None);
            sut.MoneyInside.Amount.Should().Be(2);
        }


    }
}
