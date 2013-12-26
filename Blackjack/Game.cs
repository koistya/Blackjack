// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack
{
    using System;

    public class Game
    {
        private GameAction allowedActions;
        private GameState lastState;
        private Deck deck;

        public Game()
        {
            this.Dealer = new Dealer();
            this.Player = new Player();
            this.LastState = GameState.Unknown;
            this.AllowedActions = GameAction.None;
        }

        public event EventHandler LastStateChanged;

        public event EventHandler AllowedActionsChanged;

        public Player Player { get; private set; }

        public Dealer Dealer { get; private set; }

        public GameAction AllowedActions
        {
            get
            {
                return this.allowedActions;
            }

            private set
            {
                if (this.allowedActions != value)
                {
                    this.allowedActions = value;

                    if (this.AllowedActionsChanged != null)
                    {
                        this.AllowedActionsChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public GameState LastState
        {
            get
            {
                return this.lastState;
            }

            private set
            {
                if (this.lastState != value)
                {
                    this.lastState = value;

                    if (this.LastStateChanged != null)
                    {
                        this.LastStateChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public void Play(decimal balance, decimal bet)
        {
            this.Player.Balance = balance;
            this.Player.Bet = bet;
            this.AllowedActions = GameAction.Deal;

            if (this.AllowedActionsChanged != null)
            {
                this.AllowedActionsChanged(this, EventArgs.Empty);
            }
        }

        public void Deal()
        {
            if ((this.AllowedActions & GameAction.Deal) != GameAction.Deal)
            {
                // TODO: Add a descriptive error message
                throw new InvalidOperationException();
            }

            this.LastState = GameState.Unknown;
            
            if (this.deck == null)
            {
                this.deck = new Deck();
            }
            else
            {
                this.deck.Populate();
            }

            this.deck.Shuffle();
            this.Dealer.Hand.Clear();
            this.Player.Hand.Clear();

            this.deck.Deal(this.Dealer.Hand);
            this.deck.Deal(this.Player.Hand);

            if (this.Player.Hand.SoftValue == 21)
            {
                if (this.Dealer.Hand.SoftValue == 21)
                {
                    this.LastState = GameState.Draw;
                }
                else
                {
                    this.Player.Balance += this.Player.Bet / 2;
                    this.LastState = GameState.PlayerWon;
                }

                this.Dealer.Hand.Show();
                this.AllowedActions = GameAction.Deal;
            }
            else if (this.Dealer.Hand.TotalValue == 21)
            {
                this.Player.Balance -= this.Player.Bet / 2;
                this.Dealer.Hand.Show();
                this.LastState = GameState.DealerWon;
                this.AllowedActions = GameAction.Deal;
            }
            else
            {
                // TODO: Add support of other actions
                this.AllowedActions = GameAction.Hit | GameAction.Stand;
            }
        }

        public void Hit()
        {
            if ((this.AllowedActions & GameAction.Hit) != GameAction.Hit)
            {
                // TODO: Add a descriptive error message
                throw new InvalidOperationException();
            }

            this.deck.GiveAdditionalCard(this.Player.Hand);

            if (this.Player.Hand.TotalValue > 21)
            {
                this.Player.Balance -= this.Player.Bet;
                this.Dealer.Hand.Show();
                this.LastState = GameState.DealerWon;
                this.AllowedActions = GameAction.Deal;
            }
        }

        public void Stand()
        {
            if ((this.AllowedActions & GameAction.Stand) != GameAction.Stand)
            {
                // TODO: Add a descriptive error message
                throw new InvalidOperationException();
            }

            while (this.Dealer.Hand.SoftValue < 17)
            {
                this.deck.GiveAdditionalCard(this.Dealer.Hand);
            }

            if (this.Dealer.Hand.TotalValue > 21 || this.Player.Hand.TotalValue > this.Dealer.Hand.TotalValue)
            {
                this.Player.Balance += this.Player.Bet;
                this.LastState = GameState.PlayerWon;
            }
            else if (this.Dealer.Hand.TotalValue == this.Player.Hand.TotalValue)
            {
                this.LastState = GameState.Draw;
            }
            else
            {
                this.Player.Balance -= this.Player.Bet;
                this.LastState = GameState.DealerWon;
            }

            this.Dealer.Hand.Show();
            this.AllowedActions = GameAction.Deal;
        }
    }
}
