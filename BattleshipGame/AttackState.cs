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

            Console.WriteLine($"\n{currentPlayer.Name}'s turn to attack!");
            Console.WriteLine("\nEnemy's board:");
            DisplayBoardForAttack(enemyBoard);

            int x = -1, y = -1;
            GetValidCoordinates(game, ref x, ref y);

            bool hit = enemyBoard.RegisterHit(x, y);

            if (hit)
            {
                Console.WriteLine($"\n{currentPlayer.Name} hit a ship at ({x}, {y})!");
                UpdateFleetWithHit(game, x, y);
            }
            else
            {
                Console.WriteLine($"\n{currentPlayer.Name} missed at ({x}, {y}).");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();

            NextState(game);
        }

        public override void NextState(Game game)
        {
            // Sprawdź czy któryś gracz przegrał
            var enemyPlayer = game.Players[(game.CurrentPlayerIndex + 1) % game.Players.Count];
            if (enemyPlayer.Fleet.IsDefeated())
            {
                game.State = new EndGameState();
                Console.WriteLine($"{game.Players[game.CurrentPlayerIndex].Name} wins!");
            }
            else
            {
                // Przełącz na następnego gracza
                game.SwitchTurns();
            }
        }

        private void DisplayBoardForAttack(Board board)
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");
            for (int y = 0; y < board.GridSize; y++)
            {
                Console.Write($"{y} ");
                for (int x = 0; x < board.GridSize; x++)
                {
                    var tile = board.GetTile(x, y);
                    if (tile.IsHit)
                    {
                        Console.Write(tile.ContainsShipPart ? "X " : "O ");
                    }
                    else
                    {
                        Console.Write(". "); // Ukrywa statki przeciwnika
                    }
                }
                Console.WriteLine();
            }
        }

        private void UpdateFleetWithHit(Game game, int x, int y)
        {
            var enemyPlayer = game.Players[(game.CurrentPlayerIndex + 1) % game.Players.Count];

            foreach (var ship in enemyPlayer.Fleet.Ships)
            {
                if (ship.Position.Any(pos => pos.Item1 == x && pos.Item2 == y))
                {
                    ship.TakeHit(x, y);
                    if (ship.IsSunk())
                    {
                        Console.WriteLine("Ship sunk!");
                    }
                    break;
                }
            }
        }


        // Method to handle coordinate input and ensure it's valid
        private void GetValidCoordinates(Game game, ref int x, ref int y)
        {
            var enemyBoard = game.GetEnemyBoard();
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    Console.Write($"Enter X coordinate (0 to {enemyBoard.GridSize - 1}): ");
                    x = int.Parse(Console.ReadLine());

                    Console.Write($"Enter Y coordinate (0 to {enemyBoard.GridSize - 1}): ");
                    y = int.Parse(Console.ReadLine());

                    // Sprawdź czy koordynaty są w granicach planszy
                    if (x >= 0 && x < enemyBoard.GridSize && y >= 0 && y < enemyBoard.GridSize)
                    {
                        // Sprawdź czy pole nie było już atakowane
                        var tile = enemyBoard.GetTile(x, y);
                        if (!tile.IsHit)
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine("This position has already been attacked. Choose different coordinates.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Coordinates are out of bounds. Try again.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter valid integers for coordinates.");
                }
            }
        }
    }
}

