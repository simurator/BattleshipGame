﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class OneMastShip : CompositeShip
    {
        public OneMastShip()
        {
            AddComponent(new Ship(1));
        }
    }
}
