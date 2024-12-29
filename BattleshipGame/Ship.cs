using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Ship
    {
        public int Size { get; private set; }
        public List<Tuple<int, int>> Position { get; private set; }
        public int HitParts { get; private set; } = 0;

        public Ship(int size)
        {
            Size = size;
            Position = new List<Tuple<int, int>>();
        }

        public void TakeHit()
        {
            HitParts++;
        }

        public bool IsSunk()
        {
            return HitParts >= Size;
        }
    }
}
