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
        private Game _game; // Referencja do Game

        public Board(int gridSize, Game game)
        {
            _size = gridSize;
            _game = game;
            Grid = new List<Tile>();

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Grid.Add(new Tile(x, y));
                }
            }
        }

        public bool RegisterHit(int x, int y)
        {
            var hitCell = GetTile(x, y);

            if (hitCell == null || hitCell.IsHit)
            {
                return false;
            }

            hitCell.IsHit = true;
            bool wasHit = hitCell.ContainsShipPart;

            if (wasHit)
            {
                Console.WriteLine($"\nHIT at ({x}, {y})!");

                // Sprawdź, czy jakiś statek został zatopiony
                var fleet = _game.Players[(_game.CurrentPlayerIndex + 1) % _game.Players.Count].Fleet;
                foreach (var ship in fleet.Ships)
                {
                    if (ship.Position.Any(pos => pos.Item1 == x && pos.Item2 == y))
                    {
                        ship.TakeHit(x, y);
                        if (ship.IsSunk())
                        {
                            Console.WriteLine("\n*** SHIP SUNK! ***");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine($"\nMISS at ({x}, {y}).");
            }

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
        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= _size || y < 0 || y >= _size)
            {
                return null;
            }

            // Konwersja współrzędnych 2D na indeks w liście 1D
            int index = y * _size + x;

            if (index >= 0 && index < Grid.Count)
            {
                return Grid[index];
            }

            return null;
        }
    }
}