// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerBase.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack
{
    public abstract class PlayerBase
    {
        protected PlayerBase()
        {
        }

        public Hand Hand { get; protected set; }
    }
}
