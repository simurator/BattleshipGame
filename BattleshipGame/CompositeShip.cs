using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class CompositeShip : ShipComponent
    {
        private readonly List<ShipComponent> _components = new List<ShipComponent>();

        

        public override List<Tuple<int, int>> Position
        {
            get => _components.SelectMany(component => component.Position).ToList();
            set => throw new NotImplementedException();
        }

        public override int Size
        {
            get => _components.Sum(component => component.Size);
            set => throw new NotImplementedException("CompositeShip Size is derived from its components and cannot be set directly.");
        }

        public override int HitParts
        {
            get => _components.Sum(component => component.HitParts);
            set => throw new NotImplementedException("CompositeShip HitParts is derived from its components and cannot be set directly.");
        }



        public override void TakeHit(int x, int y)
        {
            // Sprawdź, czy dany punkt na statku został trafiony
            var hitPart = Position.FirstOrDefault(p => p.Item1 == x && p.Item2 == y);

            if (hitPart != null && !IsSunk())  // Jeśli część statku została trafiona
            {
                HitParts++;  // Zwiększ licznik trafionych części
                Console.WriteLine($"Ship part at ({x}, {y}) is hit!");

                // Sprawdź, czy statek został zatopiony
                if (IsSunk())
                {
                    Console.WriteLine("The ship has been sunk!");
                }
            }
        }

        public override bool IsSunk()
        {
            return _components.All(component => component.IsSunk());
        }
        public override void AddComponent(ShipComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component), "Component cannot be null.");
            }
            _components.Add(component);
        }

    }


}
