using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Fleet
    {
        public List<Ship> Ships { get; private set; } = new List<Ship>();

        public void AddShip(Ship ship)
        {
            Ships.Add(ship);
        }

        public void RemoveShip(Ship ship)
        {
            Ships.Remove(ship);
        }

        public bool IsDefeated()
        {
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
