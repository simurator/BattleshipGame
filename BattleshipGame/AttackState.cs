using System;
using System.Linq;





namespace BattleshipGame
{
    internal class AttackState : GameState
    {
        public override void Handle(Game game)
        {
            var currentPlayer = game.Players[game.CurrentPlayerIndex];
            var enemyBoard = game.GetEnemyBoard();

            Console.Clear();
            Console.WriteLine($"\n=== {currentPlayer.Name}'s Turn ===");
            Console.WriteLine("\nEnemy's board (X = hit ship, O = miss, . = unknown):");
            DisplayBoardForAttack(enemyBoard);

            bool isAI = currentPlayer is AIPlayer || (currentPlayer is ProxyPlayer && ((ProxyPlayer)currentPlayer).RealPlayer is AIPlayer);

            if (isAI)
            {
                // AI wykonuje ruch
                currentPlayer.MakeMove(0, 0, enemyBoard); // współrzędne są ignorowane dla AI
            }
            else
            {
                // Ludzki gracz wykonuje ruch
                int x = -1, y = -1;
                GetValidCoordinates(game, ref x, ref y);
                currentPlayer.MakeMove(x, y, enemyBoard);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();

            // Sprawdź czy flota przeciwnika została zniszczona
            var enemyPlayer = game.Players[(game.CurrentPlayerIndex + 1) % game.Players.Count];
            if (enemyPlayer.Fleet.IsDefeated())
            {
                Console.WriteLine($"\n=== GAME OVER ===");
                Console.WriteLine($"{currentPlayer.Name} has won the game!");
                game.State = new EndGameState();
            }
            else
            {
                game.SwitchTurns();
            }
        }

        public override void NextState(Game game)
        {
            var enemyPlayer = game.Players[(game.CurrentPlayerIndex + 1) % game.Players.Count];

            // Check if all enemy ships are sunk
            if (enemyPlayer.Fleet.IsDefeated())
            {
                var winner = game.Players[game.CurrentPlayerIndex];
                Console.WriteLine($"\n=== GAME OVER ===");
                Console.WriteLine($"{winner.Name} has won the game!");
                game.State = new EndGameState();
                return;
            }

            game.SwitchTurns();
        }

        private void DisplayBoardForAttack(Board board)
        {
            // Display column numbers
            Console.Write("  ");
            for (int x = 0; x < board.GridSize; x++)
            {
                Console.Write($"{x} ");
            }
            Console.WriteLine();

            // Display board with row numbers
            for (int y = 0; y < board.GridSize; y++)
            {
                Console.Write($"{y} ");
                for (int x = 0; x < board.GridSize; x++)
                {
                    var tile = board.GetTile(x, y);
                    if (tile.IsHit)
                    {
                        Console.Write(tile.ContainsShipPart ? "X " : "O "); // X for hit ship, O for miss
                    }
                    else
                    {
                        Console.Write(". "); // Hidden/unknown cells
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private bool UpdateFleetWithHit(Game game, int x, int y)
        {
            var enemyPlayer = game.Players[(game.CurrentPlayerIndex + 1) % game.Players.Count];

            foreach (var ship in enemyPlayer.Fleet.Ships)
            {
                if (ship.Position.Any(pos => pos.Item1 == x && pos.Item2 == y))
                {
                    ship.TakeHit(x, y);
                    return ship.IsSunk(); // Return true if the ship was sunk by this hit
                }
            }

            return false;
        }

        private void GetValidCoordinates(Game game, ref int x, ref int y)
        {
            var enemyBoard = game.GetEnemyBoard();
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    Console.Write($"\nEnter X coordinate (0-{enemyBoard.GridSize - 1}): ");
                    string xInput = Console.ReadLine();

                    Console.Write($"Enter Y coordinate (0-{enemyBoard.GridSize - 1}): ");
                    string yInput = Console.ReadLine();

                    // Validate the input is numeric
                    if (!int.TryParse(xInput, out x) || !int.TryParse(yInput, out y))
                    {
                        Console.WriteLine("Invalid input! Please enter numbers only.");
                        continue;
                    }

                    // Check if coordinates are within bounds
                    if (x < 0 || x >= enemyBoard.GridSize || y < 0 || y >= enemyBoard.GridSize)
                    {
                        Console.WriteLine($"Coordinates must be between 0 and {enemyBoard.GridSize - 1}!");
                        continue;
                    }

                    // Check if the position was already attacked
                    var tile = enemyBoard.GetTile(x, y);
                    if (tile.IsHit)
                    {
                        Console.WriteLine("This position has already been attacked! Choose different coordinates.");
                        continue;
                    }

                    validInput = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input! Please try again.");
                }
            }
        }
    }
}

