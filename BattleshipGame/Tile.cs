using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Tile
    {
        public bool IsHit { get; set; } = false;
        public bool ContainsShipPart { get; set; } = false;
    }
}

