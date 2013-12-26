// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeckTest.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void Deck_Constructor_Test()
        {
            // Arrange
            var deck = new Deck();

            // Assert
            Assert.IsNotNull(deck.Cards);
            Assert.AreEqual(52, deck.Cards.Count);
            Assert.AreEqual(52, deck.Cards.Distinct().Count());
            Assert.AreEqual(52, deck.Cards.Select(c => c.ToString()).Distinct().Count());
            Assert.AreEqual(52, deck.Cards.Select(c => c.GetHashCode()).Distinct().Count());
        }

        [TestMethod]
        public void Deck_Shuffle_Test()
        {
            // Arrange
            var position = 0;
            var newPosition = 0;
            var deck = new Deck();
            var positions = deck.Cards.ToDictionary(c => c, c => position++);

            // Act
            deck.Shuffle();

            foreach (var card in deck.Cards)
            {
                Console.WriteLine(card);
            }

            var positionChangeCount = deck.Cards.Select(c => positions[c] == newPosition++ ? 0 : 1).Sum();

            // Assert
            Assert.IsTrue(positionChangeCount > 52 * .9);
            Assert.AreEqual(52, deck.Cards.Count);
            Assert.AreEqual(52, deck.Cards.Distinct().Count());
            Assert.AreEqual(52, deck.Cards.Select(c => c.ToString()).Distinct().Count());
            Assert.AreEqual(52, deck.Cards.Select(c => c.GetHashCode()).Distinct().Count());
        }

        [TestMethod]
        public void Deck_Deal_Test()
        {
            // Arrange
            var deck = new Deck();
            var hand = new Hand();
            var card1 = deck.Cards[0];
            var card2 = deck.Cards[1];

            // Act
            deck.Deal(hand);

            // Assert
            Assert.AreEqual(card1, hand.Cards[0]);
            Assert.AreEqual(card2, hand.Cards[1]);
            Assert.AreEqual(50, deck.Cards.Count);
            Assert.IsFalse(deck.Cards.Contains(card1));
            Assert.IsFalse(deck.Cards.Contains(card2));

            deck.Populate();
            Assert.AreEqual(52, deck.Cards.Count);
        }

        [TestMethod]
        public void Deck_GiveAdditionalCard_Test()
        {
            // Arrange
            var deck = new Deck();
            var hand = new Hand();
            var card = deck.Cards[0];

            // Act
            deck.GiveAdditionalCard(hand);

            // Assert
            Assert.AreEqual(1, hand.Cards.Count);
            Assert.AreEqual(card, hand.Cards[0]);
            Assert.IsFalse(deck.Cards.Contains(card));
        }
    }
}
