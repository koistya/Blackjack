// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CardTest.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void Card_ToString_Test()
        {
            // Arrange
            var card1 = new Card(Rank.Queen, Suite.Club);
            var card2 = new Card(Rank.Ace, Suite.Diamond);
            var card3 = new Card(Rank.Two, Suite.Heart);
            var card4 = new Card(Rank.Ten, Suite.Spades);
            var card5 = new Card(Rank.Jack, Suite.Spades);
            
            // Assert
            Assert.AreEqual("Q ♣", card1.ToString());
            Assert.AreEqual("A ♦", card2.ToString());
            Assert.AreEqual("2 ♥", card3.ToString());
            Assert.AreEqual("10 ♠", card4.ToString());
            Assert.AreEqual("J ♠", card5.ToString());
        }

        [TestMethod]
        public void Card_Equals_Test()
        {
            // Arrange
            var card1 = new Card(Rank.Queen, Suite.Club);
            var card2 = new Card(Rank.Queen, Suite.Spades);
            var card3 = new Card(Rank.Two, Suite.Club);
            var card4 = new Card(Rank.Queen, Suite.Club);

            // Assert
            Assert.AreNotEqual(card1, card2);
            Assert.AreNotEqual(card1, card3);
            Assert.AreNotEqual(card2, card3);
            Assert.AreEqual(card1, card4);
        }

        [TestMethod]
        public void Card_GetHashCode_Test()
        {
            var card1 = new Card(Rank.Ace, Suite.Club);
            var card2 = new Card(Rank.King, Suite.Spades);

            Assert.AreEqual(9, card1.GetHashCode());
            Assert.AreEqual(108, card2.GetHashCode());
        }
    }
}
