using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal abstract class ShipComponent
    {
        public abstract int Size { get; protected set; }
        public abstract List<Tuple<int, int>> Position { get; protected set; }
        public abstract int HitParts { get; protected set; }

        public abstract void TakeHit();
        public abstract bool IsSunk();
    }
}
