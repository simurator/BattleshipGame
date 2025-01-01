using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Player
    {
        public string Name { get; private set; }
        public Fleet Fleet { get; private set; }

        public Player(string name)
        {
            Name = name;
            Fleet = new Fleet();
        }

        public virtual bool MakeMove(int x, int y, Board board)
        {
            return board.RegisterHit(x, y);
        }

        public virtual void PlaceShips(Board board)
        {
            foreach (var ship in Fleet.Ships)
            {
                PlacementStatus placed= PlacementStatus.Success;
                while (placed==PlacementStatus.Success)
                {
                    Console.WriteLine($"Placing ship of size {ship.Size}.");
                    Console.Write("Enter X coordinate: ");
                    int x = int.Parse(Console.ReadLine());
                    Console.Write("Enter Y coordinate: ");
                    int y = int.Parse(Console.ReadLine());
                    Console.Write("Place horizontally? (y/n): ");
                    bool isHorizontal = Console.ReadLine()?.ToLower() == "y";

                    placed = board.PlaceShip(ship, x, y, isHorizontal);
                    if (placed==PlacementStatus.OutOfBounds|| placed==PlacementStatus.Overlap)
                    {
                        Console.WriteLine("Invalid placement. Try again.");
                    }
                }
            }
        }
    }
}
