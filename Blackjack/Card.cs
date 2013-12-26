// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Card.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack
{
    using System;

    /// <summary>
    /// A playing card.
    /// </summary>
    public class Card
    {
        public Card(Rank rank, Suite suite)
        {
            this.Rank = rank;
            this.Suite = suite;
            this.IsFaceUp = true;
        }

        public Rank Rank { get; private set; }

        public Suite Suite { get; private set; }

        public bool IsFaceUp { get; private set; }

        public void Flip()
        {
            this.IsFaceUp = !this.IsFaceUp;
        }

        public override int GetHashCode()
        {
            return ((int)this.Rank << 3) | (int)this.Suite;
        }

        public override bool Equals(object obj)
        {
            var card = obj as Card;

            return card != null && card.Suite == this.Suite && card.Rank == this.Rank;
        }

        public override string ToString()
        {
            char suite = '?';

            switch (this.Suite)
            {
                case Suite.Club:
                    suite = '♣';
                    break;
                case Suite.Diamond:
                    suite = '♦';
                    break;
                case Suite.Heart:
                    suite = '♥';
                    break;
                case Suite.Spades:
                    suite = '♠';
                    break;
            }

            var num = (int)this.Rank > 1 && (int)this.Rank < 11 ?
                ((int)this.Rank).ToString() :
                Enum.GetName(typeof(Rank), this.Rank).Substring(0, 1);

            return num + " " + suite;
        }
    }
}
