// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Konstantin Tarkus">
//   Copyright © 2013 Konstantin Tarkus (hello@tarkus.me)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BlackjackConsole
{
    using System;
    using System.Linq;
    using System.Text;

    using Blackjack;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Configure console window settings
            Console.Title = "♠ Blackjack Game" + new string(' ', 11) + "...by Konstantin Tarkus (hello@tarkus.me)";
            Console.BufferWidth = Console.WindowWidth = 70;
            Console.BufferHeight = Console.WindowHeight = 26;
            Console.CursorVisible = false;

            // Initialize and configure a new game
            var game = new Game();
            game.Player.BalanceChanged += OnBalanceChanged;
            game.LastStateChanged += OnLastStateChanged;
            game.AllowedActionsChanged += OnAllowedActionsChanged;
            game.Dealer.Hand.Changed += OnHandChanged;
            game.Player.Hand.Changed += OnHandChanged;
            game.Play(balance: 500, bet: 5);

            while (true)
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Add:
                    case ConsoleKey.UpArrow:
                        game.Player.Bet += 5;
                        break;
                    case ConsoleKey.Subtract:
                    case ConsoleKey.DownArrow:
                        game.Player.Bet -= 5;
                        break;
                    case ConsoleKey.Enter:
                        if ((game.AllowedActions & GameAction.Deal) == GameAction.Deal)
                        {
                            game.Deal();
                        }
                        else
                        {
                            game.Stand();
                        }

                        break;
                    case ConsoleKey.Spacebar:
                        if ((game.AllowedActions & GameAction.Deal) == GameAction.Deal)
                        {
                            game.Deal();
                        }
                        else
                        {
                            game.Hit();
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Renders information about player's account balance and current bet size.
        /// </summary>
        private static void OnBalanceChanged(object sender, EventArgs e)
        {
            var player = (Player)sender;
            string line = string.Format("BET: ${0} | BALANCE: ${1}", player.Bet, player.Balance).PadRight(45);

            Console.SetCursorPosition(2, 24);
            Console.Write(line);
        }

        private static void OnLastStateChanged(object sender, EventArgs e)
        {
            var game = (Game)sender;

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.SetCursorPosition(Console.BufferWidth - 30, 1);
            Console.Write((game.LastState == GameState.DealerWon ? "DEALER WON!" : "           ").PadLeft(28));

            Console.SetCursorPosition(Console.BufferWidth - 30, 13);
            Console.Write((game.LastState == GameState.PlayerWon ? "PLAYER WON!" : "           ").PadLeft(28));

            Console.ResetColor();
        }

        private static void OnAllowedActionsChanged(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            var game = (Game)sender;

            if ((game.AllowedActions & GameAction.Hit) == GameAction.Hit)
            {
                sb.Append("HIT (Spacebar)");
            }

            if ((game.AllowedActions & GameAction.Stand) == GameAction.Stand)
            {
                sb.Append((sb.Length > 0 ? ", " : string.Empty) + "STAND (Enter)");
            }

            if ((game.AllowedActions & GameAction.Deal) == GameAction.Deal)
            {
                sb.Append((sb.Length > 0 ? ", " : string.Empty) + "DEAL (Enter)");
            }

            Console.SetCursorPosition(Console.BufferWidth - 31, 24);
            Console.WriteLine(sb.ToString().PadLeft(29));
        }

        /// <summary>
        /// Renders a list of player's or dealer's cards and their value.
        /// </summary>
        private static void OnHandChanged(object sender, EventArgs e)
        {
            var hand = (Hand)sender;
            var offsetTop = hand.IsDealer ? 1 : 13;
            var name = hand.IsDealer ? "DEALER" : "PLAYER";
            var value = hand.IsDealer ? hand.FaceValue : hand.TotalValue;

            Console.SetCursorPosition(2, hand.IsDealer ? 1 : 13);
            Console.Write(string.Format("{0}'s HAND ({1}):", name, value).PadRight(25));

            for (var i = 0; i < hand.Cards.Count; i++)
            {
                var last = i == hand.Cards.Count - 1;
                Console.SetCursorPosition(2 + (i * 5), offsetTop + 2);
                Console.Write("┌────" + (last ? "─┐" : string.Empty).PadRight(Console.BufferWidth - 12 - (i * 5)));
                Console.SetCursorPosition(2 + (i * 5), offsetTop + 3);
                Console.WriteLine("│" + (hand.Cards[i].IsFaceUp ? hand.Cards[i].ToString() : "XXX").PadRight(4) + (last ? " │" : string.Empty).PadRight(Console.BufferWidth - 12 - (i * 5)));
                Console.SetCursorPosition(2 + (i * 5), offsetTop + 4);
                Console.WriteLine("│".PadRight(5) + (last ? " │" : string.Empty).PadRight(Console.BufferWidth - 12 - (i * 5)));
                Console.SetCursorPosition(2 + (i * 5), offsetTop + 5);
                Console.WriteLine("│".PadRight(5) + (last ? " │" : string.Empty).PadRight(Console.BufferWidth - 12 - (i * 5)));
                Console.SetCursorPosition(2 + (i * 5), offsetTop + 6);
                Console.WriteLine("│".PadRight(5) + (last ? " │" : string.Empty).PadRight(Console.BufferWidth - 12 - (i * 5)));
                Console.SetCursorPosition(2 + (i * 5), offsetTop + 7);
                Console.WriteLine("│".PadRight(5) + (last ? " │" : string.Empty).PadRight(Console.BufferWidth - 12 - (i * 5)));
                Console.SetCursorPosition(2 + (i * 5), offsetTop + 8);
                Console.WriteLine("└────" + (last ? "─┘" : string.Empty).PadRight(Console.BufferWidth - 12 - (i * 5)));
            }
        }
    }
}
