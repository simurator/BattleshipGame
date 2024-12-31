using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class AIPlayer : Player
    {
        private int Difficulty;

        public AIPlayer(string name, int difficulty) : base(name, 10)  // Default 10x10 board size
        {
            Difficulty = difficulty;
        }

        // Override PlaceShips to respect board parameter and difficulty
        public override void PlaceShips(Board board)
        {
            Console.WriteLine($"AI player places ships with difficulty {Difficulty}.");
            Random random = new Random();

            foreach (var ship in Fleet.Ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = -1, y = -1;
                    bool isHorizontal = false;

                    // Implementing different strategies based on difficulty
                    if (Difficulty == 1)  // Easy difficulty: Random placement
                    {
                        x = random.Next(0, board.Grid.Count);
                        y = random.Next(0, board.Grid.Count);
                        isHorizontal = random.Next(0, 2) == 0;
                    }
                    else if (Difficulty == 2)  // Medium difficulty: Avoid edges/corners
                    {
                        x = random.Next(1, board.Grid.Count - 1);  // Avoid edges
                        y = random.Next(1, board.Grid.Count - 1);
                        isHorizontal = random.Next(0, 2) == 0;
                    }
                    else if (Difficulty == 3)  // Hard difficulty: Smarter placement
                    {
                        x = random.Next(2, board.Grid.Count - 2);  // Avoid too close to edges
                        y = random.Next(2, board.Grid.Count - 2);
                        isHorizontal = random.Next(0, 2) == 0;
                    }

                    // Attempt to place the ship
                    PlacementStatus status = board.PlaceShip(ship, x, y, isHorizontal);
                    if (status == PlacementStatus.Success)
                    {
                        placed = true;
                    }
                    else if (status == PlacementStatus.OutOfBounds)
                    {
                        Console.WriteLine($"AI: Failed to place ship. The position ({x}, {y}) is out of bounds.");
                    }
                    else if (status == PlacementStatus.Overlap)
                    {
                        Console.WriteLine($"AI: Failed to place ship. The position ({x}, {y}) overlaps with another ship.");
                    }
                }
            }
        }

        // Optionally implement a smarter move-making strategy for different difficulty levels
        public override bool MakeMove(int x, int y, Board enemyBoard)
        {
            Console.WriteLine($"AI player makes a move with difficulty {Difficulty}.");
            return base.MakeMove(x, y, enemyBoard);
        }
    }
}
