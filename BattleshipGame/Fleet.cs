using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Fleet
    {
        public List<CompositeShip> Ships { get; private set; } = new List<CompositeShip>();

        public void AddShip(CompositeShip ship)
        {
            Ships.Add(ship);
        }

        public void RemoveShip(CompositeShip ship)
        {
            Ships.Remove(ship);
        }

        public bool IsDefeated()
        {
            // Debugowanie stanu floty
            Console.WriteLine($"Checking fleet status: {Ships.Count} ships total");
            foreach (var ship in Ships)
            {
                Console.WriteLine($"Ship status: {ship.HitParts}/{ship.Size} hits");
            }

            return Ships.All(ship => ship.IsSunk());
        }
    }
}
