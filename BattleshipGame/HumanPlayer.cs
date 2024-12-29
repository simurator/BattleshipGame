using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class HumanPlayer : Player
    {
        public HumanPlayer(string name) : base(name) { }

        public override void PlaceShips(Board board)
        {
            Console.WriteLine("Human player places ships manually.");

            foreach (var ship in Fleet.Ships)
            {
                bool placed = false;
                while (!placed)
                {
                    Console.WriteLine($"Placing ship of size {ship.Size}.");
                    Console.Write("Enter X coordinate: ");
                    int x = int.Parse(Console.ReadLine());
                    Console.Write("Enter Y coordinate: ");
                    int y = int.Parse(Console.ReadLine());
                    Console.Write("Place horizontally? (y/n): ");
                    bool isHorizontal = Console.ReadLine()?.ToLower() == "y";

                    placed = board.PlaceShip(ship, x, y, isHorizontal);
                    if (!placed)
                    {
                        Console.WriteLine("Invalid placement. Try again.");
                    }
                }
            }
        }
    }
}
