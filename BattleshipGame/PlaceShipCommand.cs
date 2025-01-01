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

            // Attempt to place the ship and check if the placement is valid
            var placementStatus = PlayerBoard.PlaceShip(Ship, Target.Item1, Target.Item2, IsHorizontal);

            if (placementStatus == PlacementStatus.Success)
            {
                Console.WriteLine($"Ship placed successfully at {Target.Item1}, {Target.Item2}.");
            }
            else
            {
                Console.WriteLine("Failed to place ship: " + placementStatus);
                Ship.Position.Clear(); // Clear the position if placement fails
            }
        }

        public override void Undo()
        {
            // Reset the ship positions back to their previous state in case of undo
            foreach (var position in Ship.Position)
            {
                var tile = PlayerBoard.GetTile(position.Item1, position.Item2);
                tile.ContainsShipPart = false;  // Undo the ship placement on the board
            }

            Ship.Position.Clear();
            Ship.Position.AddRange((List<Tuple<int, int>>)PreviousState);  // Restore the previous positions
        }
    }
}
