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
            if (Ships.Count == 0)
            {
                return false;
            }

            foreach (var ship in Ships)
            {
                if (!ship.IsSunk())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
