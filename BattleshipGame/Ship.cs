﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Ship : ShipComponent
    {
        public override int Size { get; protected set; }
        public override List<Tuple<int, int>> Position { get; protected set; }
        public override int HitParts { get; protected set; } = 0;

        public Ship(int size)
        {
            Size = size;
            Position = new List<Tuple<int, int>>();
        }

        public override void TakeHit()
        {
            HitParts++;
        }

        public override bool IsSunk()
        {
            return HitParts >= Size;
        }
    }
}
