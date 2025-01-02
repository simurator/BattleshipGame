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
        public List<Tile> Grid { get; set; }
        private int _size;
        public int GridSize => _size;

        public Board(int gridSize)
        {
            _size = gridSize;
            Grid = new List<Tile>();

            // Initialize grid with tiles - FIXED coordinate system
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Grid.Add(new Tile(x, y));
                }
            }
        }

        // Get tile using correct coordinate system
        public Tile GetTile(int x, int y)
        {
            return Grid.FirstOrDefault(tile => tile.X == x && tile.Y == y);
        }

        public bool RegisterHit(int x, int y)
        {
            var hitCell = GetTile(x, y);

            if (hitCell == null)
            {
                Console.WriteLine("Invalid coordinates!");
                return false;
            }

            if (hitCell.IsHit)
            {
                Console.WriteLine($"Position ({x}, {y}) was already hit!");
                return false;
            }

            hitCell.IsHit = true;
            bool wasHit = hitCell.ContainsShipPart;

            Console.WriteLine(wasHit ?
                $"Hit! Ship was hit at ({x}, {y})!" :
                $"Miss! No ship at ({x}, {y})!");

            DisplayBoard();
            return wasHit;
        }

        public PlacementStatus PlaceShip(CompositeShip ship, int x, int y, bool isHorizontal)
        {
            if (isHorizontal)
            {
                if (x + ship.Size > _size) return PlacementStatus.OutOfBounds;

                // Check for overlap
                for (int i = 0; i < ship.Size; i++)
                {
                    var cell = GetTile(x + i, y);
                    if (cell == null || cell.ContainsShipPart) return PlacementStatus.Overlap;
                }

                // Place ship
                for (int i = 0; i < ship.Size; i++)
                {
                    var cell = GetTile(x + i, y);
                    cell.ContainsShipPart = true;
                    ship.Position.Add(Tuple.Create(x + i, y));
                }
            }
            else
            {
                if (y + ship.Size > _size) return PlacementStatus.OutOfBounds;

                // Check for overlap
                for (int i = 0; i < ship.Size; i++)
                {
                    var cell = GetTile(x, y + i);
                    if (cell == null || cell.ContainsShipPart) return PlacementStatus.Overlap;
                }

                // Place ship
                for (int i = 0; i < ship.Size; i++)
                {
                    var cell = GetTile(x, y + i);
                    cell.ContainsShipPart = true;
                    ship.Position.Add(Tuple.Create(x, y + i));
                }
            }

            DisplayBoard();
            return PlacementStatus.Success;
        }

        public void DisplayBoard()
        {
            Console.WriteLine("\nCurrent Board State:");
            // Display column numbers
            Console.Write("  ");
            for (int x = 0; x < _size; x++)
            {
                Console.Write($"{x} ");
            }
            Console.WriteLine();

            // Display board with row numbers
            for (int y = 0; y < _size; y++)
            {
                Console.Write($"{y} ");
                for (int x = 0; x < _size; x++)
                {
                    var tile = GetTile(x, y);
                    if (tile.IsHit)
                    {
                        Console.Write(tile.ContainsShipPart ? "X " : "O "); // X for hit ship, O for miss
                    }
                    else
                    {
                        Console.Write(tile.ContainsShipPart ? "S " : ". "); // S for ship, . for water
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}