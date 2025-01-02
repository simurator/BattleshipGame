using System;
using System.Collections.Generic;

namespace BattleshipGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to Battleship Game! ===\n");

            // Get game mode
            string mode = GetGameMode();

            // Get AI difficulty if PvAI mode
            int difficulty = 1;
            if (mode == "PvAI")
            {
                difficulty = GetAIDifficulty();
            }

            // Get board size
            int boardSize = GetBoardSize();

            // Define ship configuration - now properly structured for both players
            var shipTypes = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(4, 1), // One 4-mast ship
                new Tuple<int, int>(3, 2), // Two 3-mast ships
                new Tuple<int, int>(2, 3), // Three 2-mast ships
                new Tuple<int, int>(1, 4)  // Four 1-mast ships
            };

            try
            {
                Console.Clear();
                Console.WriteLine("=== Game Starting ===\n");

                // Create game using GameBuilder with updated parameters
                var game = new GameBuilder()
                    .SetBoardSize(boardSize, boardSize)
                    .SetMode(mode)
                    .SetShipTypes(shipTypes)
                    .SetDifficulty(difficulty)
                    .Build();

                // Create timer to track game duration
                var timer = new Timer();
                timer.Start();

                // Start the game with the new board system
                game.StartGame();

                // Stop timer when game ends
                timer.Stop();

                // Display final game statistics
                Console.Clear();
                Console.WriteLine("\n=== Game Statistics ===");
                Console.WriteLine($"Game Mode: {mode}");
                Console.WriteLine($"Board Size: {boardSize}x{boardSize}");
                Console.WriteLine($"Game Duration: {timer.GetElapsedTime():F1} seconds");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                Console.WriteLine("Please restart the game.");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static string GetGameMode()
        {
            while (true)
            {
                Console.WriteLine("Select game mode:");
                Console.WriteLine("1. Player vs AI");
                Console.WriteLine("2. Player vs Player");
                Console.Write("\nYour choice (1 or 2): ");

                string input = Console.ReadLine()?.Trim();
                if (input == "1") return "PvAI";
                if (input == "2") return "PvP";

                Console.Clear();
                Console.WriteLine("Invalid selection. Please choose 1 or 2.\n");
            }
        }

        private static int GetAIDifficulty()
        {
            while (true)
            {
                Console.WriteLine("\nSelect AI difficulty:");
                Console.WriteLine("1. Easy   - AI places ships randomly");
                Console.WriteLine("2. Medium - AI avoids board edges");
                Console.WriteLine("3. Hard   - AI uses advanced strategy");
                Console.Write("\nYour choice (1-3): ");

                if (int.TryParse(Console.ReadLine(), out int difficulty) &&
                    difficulty >= 1 && difficulty <= 3)
                {
                    return difficulty;
                }

                Console.Clear();
                Console.WriteLine("Invalid difficulty. Please choose a number between 1 and 3.\n");
            }
        }

        private static int GetBoardSize()
        {
            while (true)
            {
                Console.WriteLine("\nEnter board size:");
                Console.WriteLine("- Minimum size: 5x5");
                Console.WriteLine("- Maximum size: 20x20");
                Console.WriteLine("- Recommended: 10x10");
                Console.Write("\nYour choice (5-20): ");

                if (int.TryParse(Console.ReadLine(), out int size) &&
                    size >= 5 && size <= 20)
                {
                    return size;
                }

                Console.Clear();
                Console.WriteLine("Invalid board size. Please choose a number between 5 and 20.\n");
            }
        }
    }
}