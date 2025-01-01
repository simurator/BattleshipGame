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

            // Initialize grid with tiles
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Grid.Add(new Tile(i, j));  // Create Tile for each position
                }
            }
        }

        // Registers a hit on the board
        public bool RegisterHit(int x, int y)
        {
            var hitCell = Grid.FirstOrDefault(cell => cell.X == x && cell.Y == y);
            if (hitCell != null)
            {
                hitCell.IsHit = true;
                Console.WriteLine($"Hit registered at ({x}, {y})!");
                DisplayBoard();
                return true;
            }

            Console.WriteLine($"Miss at ({x}, {y})!");
            DisplayBoard();
            return false;
        }


        // Places a ship on the board, ensuring no overlap and staying within bounds
        public PlacementStatus PlaceShip(CompositeShip ship, int x, int y, bool isHorizontal)
        {
            if (isHorizontal)
            {
                if (x + ship.Size > _size) return PlacementStatus.OutOfBounds;

                for (int i = 0; i < ship.Size; i++)
                {
                    var cell = Grid.FirstOrDefault(c => c.X == x + i && c.Y == y);
                    if (cell == null || cell.ContainsShipPart) return PlacementStatus.Overlap;
                }

                for (int i = 0; i < ship.Size; i++)
                {
                    var cell = Grid.First(c => c.X == x + i && c.Y == y);
                    cell.ContainsShipPart = true;
                    ship.Position.Add(Tuple.Create(x + i, y));
                }
            }
            else
            {
                if (y + ship.Size > _size) return PlacementStatus.OutOfBounds;

                for (int i = 0; i < ship.Size; i++)
                {
                    var cell = Grid.FirstOrDefault(c => c.X == x && c.Y == y + i);
                    if (cell == null || cell.ContainsShipPart) return PlacementStatus.Overlap;
                }

                for (int i = 0; i < ship.Size; i++)
                {
                    var cell = Grid.First(c => c.X == x && c.Y == y + i);
                    cell.ContainsShipPart = true;
                    ship.Position.Add(Tuple.Create(x, y + i));
                }
            }
            DisplayBoard();
            return PlacementStatus.Success;
        }

        public void DisplayBoard()
        {
            Console.WriteLine("Current Board State:");
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    var tile = GetTile(x, y);
                    if (tile.IsHit)
                    {
                        Console.Write(tile.ContainsShipPart ? "X " : "O "); // "X" for hit ship part, "O" for missed shot
                    }
                    else
                    {
                        Console.Write(tile.ContainsShipPart ? "S " : ". "); // "S" for unhit ship part, "." for empty water
                    }
                }
                Console.WriteLine(); // Move to the next row
            }
        }

        public Tile GetTile(int x, int y)
        {
            int index = y * GridSize + x;
            return Grid[index];
        }

    }
}

