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

        public AIPlayer(string name, int difficulty) : base(name)
        {
            Difficulty = difficulty;
        }

        public override void PlaceShips(Board board)
        {
            Console.WriteLine($"AI player places ships with difficulty {Difficulty}.");
            Random random = new Random();

            foreach (var ship in Fleet.Ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = random.Next(0, board.Grid.Count);
                    int y = random.Next(0, board.Grid.Count);
                    bool isHorizontal = random.Next(0, 2) == 0;

                    placed = board.PlaceShip(ship, x, y, isHorizontal);
                }
            }
        }

        public override bool MakeMove(int x, int y, Board enemyBoard)
        {
            Console.WriteLine($"AI player makes a move with difficulty {Difficulty}.");
            return base.MakeMove(x, y, enemyBoard);
        }
    }
}
