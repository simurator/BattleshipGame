using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal enum PlacementStatus
{
    Success,
    OutOfBounds,
    Overlap
}

namespace BattleshipGame
{
    internal class Board
    {
        public List<List<Tile>> Grid { get; private set; }
        private int _size;

        public Board(int size)
        {
            _size = size;
            Grid = new List<List<Tile>>();

            for (int i = 0; i < size; i++)
            {
                var row = new List<Tile>();
                for (int j = 0; j < size; j++)
                {
                    row.Add(new Tile());
                }
                Grid.Add(row);
            }
        }

        

        public bool RegisterHit(int x, int y)
        {
            if (x < 0 || x >= _size || y < 0 || y >= _size) return false;

            var tile = Grid[y][x];
            if (tile.IsHit) return false;

            tile.IsHit = true;
            return tile.ContainsShipPart;
        }
        public PlacementStatus PlaceShip(CompositeShip ship, int x, int y, bool isHorizontal)
        {
            if (isHorizontal)
            {
                if (x + ship.Size > _size) return PlacementStatus.OutOfBounds;

                for (int i = 0; i < ship.Size; i++)
                {
                    if (Grid[y][x + i].ContainsShipPart) return PlacementStatus.Overlap;
                }

                for (int i = 0; i < ship.Size; i++)
                {
                    Grid[y][x + i].ContainsShipPart = true;
                    ship.Position.Add(Tuple.Create(x + i, y));
                }
            }
            else
            {
                if (y + ship.Size > _size) return PlacementStatus.OutOfBounds;

                for (int i = 0; i < ship.Size; i++)
                {
                    if (Grid[y + i][x].ContainsShipPart) return PlacementStatus.Overlap;
                }

                for (int i = 0; i < ship.Size; i++)
                {
                    Grid[y + i][x].ContainsShipPart = true;
                    ship.Position.Add(Tuple.Create(x, y + i));
                }
            }

            return PlacementStatus.Success;
        }
    }

}
