// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerTest.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void Player_Initialization_Test()
        {
            // Arrange
            var player = new Player();
            
            // Assert
            Assert.IsFalse(player.Hand.IsDealer);
            Assert.AreEqual(0, player.Balance);
            Assert.AreEqual(0, player.Bet);
        }

        [TestMethod]
        public void Player_Balance_and_Bet_Test()
        {
            // Arrange
            var player = new Player();

            // Act & Assert
            player.Balance = 5;
            Assert.AreEqual(0, player.Bet);
            Assert.AreEqual(5, player.Balance);

            player.Bet = 1;
            Assert.AreEqual(1, player.Bet);
            Assert.AreEqual(4, player.Balance);

            player.Bet = 2;
            Assert.AreEqual(2, player.Bet);
            Assert.AreEqual(3, player.Balance);

            player.Bet = 5;
            Assert.AreEqual(5, player.Bet);
            Assert.AreEqual(0, player.Balance);

            player.Bet = 6;
            Assert.AreEqual(5, player.Bet);
            Assert.AreEqual(0, player.Balance);

            player.Bet = 0;
            Assert.AreEqual(0, player.Bet);
            Assert.AreEqual(5, player.Balance);

            player.Bet = -1;
            Assert.AreEqual(0, player.Bet);
            Assert.AreEqual(5, player.Balance);

            player.Bet = 1;
            player.Bet = 6;
            Assert.AreEqual(5, player.Bet);
            Assert.AreEqual(0, player.Balance);

            player.Bet = 1;
            player.Bet = -1;
            Assert.AreEqual(0, player.Bet);
            Assert.AreEqual(5, player.Balance);
        }

        [TestMethod]
        public void Player_OnBalanceChanged_Test()
        {
            // Arrange
            var onBalanceChangedCalled = false;
            var player = new Player();
            player.BalanceChanged += (object s, EventArgs e) => onBalanceChangedCalled = true;

            // Act
            player.Balance = 10;

            // Assert
            Assert.IsTrue(onBalanceChangedCalled);

            // Make sure that OnBalanceChanged doesn't fire if the balance didn't change
            player.Balance = 10;
            player.Bet = 1;
            onBalanceChangedCalled = false;
            player.Bet = 1;
            Assert.IsFalse(onBalanceChangedCalled);
        }
    }
}
