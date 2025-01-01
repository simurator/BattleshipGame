using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal abstract class ShipComponent
    {
        public abstract int Size { get; set; }
        public abstract List<Tuple<int, int>> Position { get; set; }
        public abstract int HitParts { get; set; }

        public virtual void TakeHit(int x, int y) { }
        public abstract bool IsSunk();

        public virtual void AddComponent(ShipComponent component)
        {
            throw new NotSupportedException("This component does not support adding sub-components.");
        }
    }
}
