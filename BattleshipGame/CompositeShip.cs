﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class CompositeShip : ShipComponent
    {
        private readonly List<ShipComponent> _components = new List<ShipComponent>();

        public override int Size
        {
            get => _components.Sum(component => component.Size);
            protected set => throw new NotImplementedException("CompositeShip Size is derived from its components and cannot be set directly.");
        }

        public override List<Tuple<int, int>> Position
        {
            get => _components.SelectMany(component => component.Position).ToList();
            protected set => throw new NotImplementedException("CompositeShip Position is derived from its components and cannot be set directly.");
        }

        public override int HitParts
        {
            get => _components.Sum(component => component.HitParts);
            protected set => throw new NotImplementedException("CompositeShip HitParts is derived from its components and cannot be set directly.");
        }


        public void AddComponent(ShipComponent component)
        {
            _components.Add(component);
        }

        public void RemoveComponent(ShipComponent component)
        {
            _components.Remove(component);
        }

        public override void TakeHit()
        {
            foreach (var component in _components)
            {
                if (!component.IsSunk())
                {
                    component.TakeHit();
                    break;
                }
            }
        }

        public override bool IsSunk()
        {
            return _components.All(component => component.IsSunk());
        }

        public List<ShipComponent> GetComponents()
        {
            return new List<ShipComponent>(_components);
        }
    }
}