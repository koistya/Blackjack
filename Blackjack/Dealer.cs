// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dealer.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack
{
    public class Dealer : PlayerBase
    {
        public Dealer()
        {
            this.Hand = new Hand(isDealer: true);
        }
    }
}
