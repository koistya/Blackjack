// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameTest.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GameTest
    {
        private Game game;

        [TestInitialize]
        public void StartUp()
        {
            this.game = new Game();
        }

        [TestMethod]
        public void Game_Start_Test()
        {
            // Act
            this.game.Play(balance: 200, bet: 2);

            // Assert
            Assert.AreEqual(198, this.game.Player.Balance);
            Assert.AreEqual(2, this.game.Player.Bet);
        }
    }
}
