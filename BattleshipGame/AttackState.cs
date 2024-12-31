using System;
using System.Linq;

namespace BattleshipGame
{
    internal class AttackState : GameState
    {
        public override void NextState(Game game)
        {
            // Check if any player has lost their fleet
            if (game.Players.Any(player => player.Fleet.IsDefeated()))
            {
                game.State = new EndGameState();  // Transition to EndGameState
                Console.WriteLine("Game state changed to EndGameState.");
            }
            else
            {
                game.SwitchTurns();  // Switch to the next player
                Console.WriteLine("Game state changed to SwitchTurns.");
            }
        }

        public override void Handle(Game game)
        {
            var currentPlayer = game.Players[game.CurrentPlayerIndex];
            Console.WriteLine($"{currentPlayer.Name}, it's your turn to attack.");

            int x = -1, y = -1;

            // Get valid input for coordinates
            GetValidCoordinates(game, ref x, ref y);

            // Access the enemy player and register the hit on their board
            var enemyPlayer = game.Players[(game.CurrentPlayerIndex + 1) % game.Players.Count];
            bool hit = enemyPlayer.Board.RegisterHit(x, y);

            // Handle the attack result
            if (hit)
            {
                Console.WriteLine($"{currentPlayer.Name} hit a ship at ({x}, {y})!");

                // Update the fleet to reflect the hit on the ship
                UpdateFleetWithHit(enemyPlayer.Fleet, x, y);
            }
            else
            {
                Console.WriteLine($"{currentPlayer.Name} missed at ({x}, {y}).");
            }

            // Transition to the next game state after the attack
            NextState(game);
        }

        // Method to handle coordinate input and ensure it's valid
        private void GetValidCoordinates(Game game, ref int x, ref int y)
        {
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    Console.Write("Enter X coordinate (0 to {0}): ", game.Board.Grid.Count - 1);
                    x = int.Parse(Console.ReadLine());

                    Console.Write("Enter Y coordinate (0 to {0}): ", game.Board.Grid.Count - 1);
                    y = int.Parse(Console.ReadLine());

                    if (x >= 0 && x < game.Board.Grid.Count && y >= 0 && y < game.Board.Grid.Count)
                    {
                        validInput = true;  // Valid input
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

        // Method to update the Fleet after a successful hit
        private void UpdateFleetWithHit(Fleet enemyFleet, int x, int y)
        {
            // Check if any ship in the fleet is hit and update it
            foreach (var ship in enemyFleet.Ships)
            {
                if (ship is CompositeShip compositeShip)
                {
                    // If it's a composite ship, update its components
                    foreach (var component in compositeShip.GetComponents())
                    {
                        component.TakeHit(x, y);  // Assuming that each component has a TakeHit method
                    }
                }
                else
                {
                    // For regular ships, just take the hit directly
                    ship.TakeHit(x, y);
                }
            }
        }
    }
}
