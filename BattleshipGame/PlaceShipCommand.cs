using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class PlaceShipCommand : PlayerCommand
    {
        private Board PlayerBoard;
        private CompositeShip Ship;
        private bool IsHorizontal;

        public PlaceShipCommand(Player player, Tuple<int, int> target, Board playerBoard, CompositeShip ship, bool isHorizontal)
            : base(player, target, "PlaceShip")
        {
            PlayerBoard = playerBoard;
            Ship = ship;
            IsHorizontal = isHorizontal;
        }

        public override void Execute()
        {
            PreviousState = new List<Tuple<int, int>>(Ship.Position);
            PlayerBoard.PlaceShip(Ship, Target.Item1, Target.Item2, IsHorizontal);
        }

        public override void Undo()
        {
            foreach (var position in Ship.Position)
            {
                PlayerBoard.Grid[position.Item2][position.Item1].ContainsShipPart = false;
            }
            Ship.Position.Clear();
            Ship.Position.AddRange((List<Tuple<int, int>>)PreviousState);
        }
    }
}
