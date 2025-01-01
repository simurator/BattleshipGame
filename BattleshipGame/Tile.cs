using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHit { get; set; } = false;
        public bool ContainsShipPart { get; set; } = false;

        // Możesz dodać konstruktor, jeśli chcesz od razu ustawiać współrzędne
        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }


    }

}

