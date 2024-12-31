using System;
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

        public override void TakeHit(int x, int y)
        {
            // Check if the hit coordinates match this part's position
            var hitPart = Position.FirstOrDefault(p => p.Item1 == x && p.Item2 == y);

            if (hitPart != null && !IsSunk())
            {
                // Register the hit on this part
                HitParts++;
                Console.WriteLine($"Ship part at ({x}, {y}) is hit!");
            }
        }

        public override bool IsSunk()
        {
            return HitParts >= Size;
        }
    }
}
