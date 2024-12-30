using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class FourMastShip : CompositeShip
    {
        public FourMastShip()
        {
            AddComponent(new Ship(1));
            AddComponent(new Ship(1));
            AddComponent(new Ship(1));
            AddComponent(new Ship(1));
        }
    }
}
