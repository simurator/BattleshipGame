using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class AIPlayer : Player
    {
        private Random random;
        private int _difficulty;

        public AIPlayer(string name, int difficulty) : base(name)
        {
            random = new Random();
            _difficulty = difficulty;
        }

        public override bool MakeMove(int x, int y, Board enemyBoard)
        {
            // AI wybiera losowe współrzędne, które nie były jeszcze atakowane
            do
            {
                x = random.Next(0, enemyBoard.GridSize);
                y = random.Next(0, enemyBoard.GridSize);
            } while (enemyBoard.GetTile(x, y).IsHit);

            return enemyBoard.RegisterHit(x, y);
        }

        public override void PlaceShips(Board board)
        {
            Console.WriteLine($"\nAI player {Name} is placing ships...");

            foreach (var ship in Fleet.Ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = random.Next(0, board.GridSize);
                    int y = random.Next(0, board.GridSize);
                    bool isHorizontal = random.Next(2) == 0;

                    PlacementStatus status = board.PlaceShip(ship, x, y, isHorizontal);
                    if (status == PlacementStatus.Success)
                    {
                        placed = true;
                        Console.WriteLine($"AI placed {ship.Size}-mast ship at ({x}, {y})");
                    }
                }
            }

            Console.WriteLine("AI finished placing ships.");
            board.DisplayBoard();
        }
    }
}
