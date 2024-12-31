using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class HumanPlayer : Player
    {
        public HumanPlayer(string name, int boardSize) : base(name, boardSize) { }

        public override void PlaceShips(Board board)
        {
            Console.WriteLine("Human player places ships manually.");
            Random random = new Random(); // For random number generation during retries

            foreach (var ship in Fleet.Ships)
            {
                bool placed = false;
                while (!placed)
                {
                    Console.WriteLine($"Placing ship of size {ship.Size}.");

                    // Input validation for X coordinate
                    int x = -1;
                    while (x < 0 || x >= board.Grid.Count)
                    {
                        Console.Write("Enter X coordinate (0 to {0}): ", board.Grid.Count - 1);
                        if (!int.TryParse(Console.ReadLine(), out x) || x < 0 || x >= board.Grid.Count)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid X coordinate within the board size.");
                        }
                    }

                    // Input validation for Y coordinate
                    int y = -1;
                    while (y < 0 || y >= board.Grid.Count)
                    {
                        Console.Write("Enter Y coordinate (0 to {0}): ", board.Grid.Count - 1);
                        if (!int.TryParse(Console.ReadLine(), out y) || y < 0 || y >= board.Grid.Count)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Y coordinate within the board size.");
                        }
                    }

                    // Ask for horizontal or vertical placement
                    bool isHorizontal = false;
                    bool validDirection = false;
                    while (!validDirection)
                    {
                        Console.Write("Place horizontally? (y/n): ");
                        string directionInput = Console.ReadLine()?.ToLower();
                        if (directionInput == "y")
                        {
                            isHorizontal = true;
                            validDirection = true;
                        }
                        else if (directionInput == "n")
                        {
                            isHorizontal = false;
                            validDirection = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 'y' for horizontal or 'n' for vertical.");
                        }
                    }

                    // Try placing the ship
                    PlacementStatus status = board.PlaceShip(ship, x, y, isHorizontal);

                    if (status == PlacementStatus.Success)
                    {
                        placed = true;
                    }
                    else
                    {
                        // Handle invalid placements
                        if (status == PlacementStatus.OutOfBounds)
                        {
                            Console.WriteLine("Invalid placement: Out of bounds. Try again.");
                        }
                        else if (status == PlacementStatus.Overlap)
                        {
                            Console.WriteLine("Invalid placement: Ship overlaps with another ship. Try again.");
                        }
                    }
                }
            }
        }
    }
}
