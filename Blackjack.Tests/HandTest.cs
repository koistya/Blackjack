// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandTest.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HandTest
    {
        [TestMethod]
        public void Hand_AddCard_Test()
        {
            // Arrange
            var hand = new Hand();

            // Act & Assert
            hand.AddCard(new Card(Rank.Ace, Suite.Club));
            Assert.AreEqual(10, hand.SoftValue);
            Assert.AreEqual(10, hand.TotalValue);

            hand.AddCard(new Card(Rank.Two, Suite.Club));
            Assert.AreEqual(12, hand.SoftValue);
            Assert.AreEqual(12, hand.TotalValue);

            hand.AddCard(new Card(Rank.Ace, Suite.Club));
            Assert.AreEqual(22, hand.SoftValue);
            Assert.AreEqual(13, hand.TotalValue);

            hand.AddCard(new Card(Rank.Eight, Suite.Club));
            Assert.AreEqual(30, hand.SoftValue);
            Assert.AreEqual(21, hand.TotalValue);

            hand.Clear();
            Assert.AreEqual(0, hand.Cards.Count);
            Assert.AreEqual(0, hand.SoftValue);
            Assert.AreEqual(0, hand.TotalValue);
        }

        [TestMethod]
        public void Hand_IsDealer_Test()
        {
            // Arrange
            var hand1 = new Hand(isDealer: true);
            var hand2 = new Hand(isDealer: false);

            // Assert
            Assert.IsTrue(hand1.IsDealer);
            Assert.IsFalse(hand2.IsDealer);
        }

        [TestMethod]
        public void Hand_Should_Fire_OnChanged_Event()
        {
            // Arrange
            var hand = new Hand();
            var onChangedCalled = false;

            // Act
            hand.Changed += (object sender, EventArgs args) => onChangedCalled = true;
            hand.AddCard(new Card(Rank.Ace, Suite.Club));

            // Assert
            Assert.IsTrue(onChangedCalled);
        }
    }
}
