// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Player : PlayerBase
    {
        private decimal balance;
        private decimal bet;

        public Player()
        {
            this.Hand = new Hand(isDealer: false);
        }

        public event EventHandler BalanceChanged;

        public decimal Balance
        {
            get
            {
                return this.balance;
            }

            set
            {
                if (this.balance != value)
                {
                    this.balance = value;

                    if (this.BalanceChanged != null)
                    {
                        this.BalanceChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public decimal Bet
        {
            get
            {
                return this.bet;
            }

            set
            {
                if (this.bet == value)
                {
                    return;
                }

                if (value > this.balance + this.bet && this.balance > 0)
                {
                    this.bet += this.balance;
                    this.Balance = 0;
                }
                else if (value < 0 && this.bet > 0)
                {
                    var temp = this.bet + this.balance;
                    this.bet = 0;
                    this.Balance = temp;
                }
                else if (value >= 0 && value <= this.balance + this.bet)
                {
                    var temp = this.balance + this.bet;
                    this.bet = value;
                    this.Balance = temp - value;
                }
            }
        }
    }
}
