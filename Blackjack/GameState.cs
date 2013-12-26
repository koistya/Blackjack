// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameState.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack
{
    public enum GameState : byte
    {
        Unknown,
        PlayerWon,
        DealerWon,
        Draw
    }
}
