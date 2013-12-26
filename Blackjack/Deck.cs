// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Deck.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Security.Cryptography;

    public class Deck
    {
        private readonly List<Card> cards = new List<Card>(52);

        public Deck()
        {
            this.Populate();
        }

        internal ReadOnlyCollection<Card> Cards
        {
            get { return this.cards.AsReadOnly(); }
        }

        /// <summary>
        /// Generates a new deck of 52 cards
        /// </summary>
        public void Populate()
        {
            this.cards.Clear();
            this.cards.AddRange(
                Enumerable.Range(1, 4)
                    .SelectMany(s => Enumerable.Range(1, 13).Select(n => new Card((Rank)n, (Suite)s))));
        }

        /// <summary>
        /// Shuffles playing cards in the deck.
        /// </summary>
        public void Shuffle()
        {
            var i = this.cards.Count;
            var rng = new RNGCryptoServiceProvider();

            while (i > 1)
            {
                // Generates a uniformly distributed random number 'i' in a range of [0..this.cards.Count)
                var buffer = new byte[8];
                rng.GetBytes(buffer);
                var j = (int)(BitConverter.ToUInt64(buffer, 0) % (ulong)this.cards.Count);

                // Swap two cards
                i--;
                var temp = this.cards[j];
                this.cards[j] = this.cards[i];
                this.cards[i] = temp;
            }
        }

        public void Deal(Hand hand)
        {
            if (this.cards.Count < 2)
            {
                // TODO: Add a descriptive error message
                throw new InvalidOperationException();
            }

            var card = this.cards.First();
            hand.AddCard(card);
            this.cards.Remove(card);

            card = this.cards.First();

            if (hand.IsDealer)
            {
                card.Flip();
            }

            hand.AddCard(card);
            this.cards.Remove(card);
        }

        public void GiveAdditionalCard(Hand hand)
        {
            if (this.cards.Count < 1)
            {
                // TODO: Add a descriptive error message
                throw new InvalidOperationException();
            }

            hand.AddCard(this.cards.First());
            this.cards.RemoveAt(0);
        }
    }
}
