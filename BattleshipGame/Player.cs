using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Player
    {
        public string Name { get; private set; }
        public Fleet Fleet { get; private set; }
        public Board Board { get; private set; }

        public Player(string name, int boardSize)
        {
            Name = name;
            Fleet = new Fleet();
            Board = new Board(boardSize);
        }

        public virtual bool MakeMove(int x, int y, Board enemyBoard)
        {
            return enemyBoard.RegisterHit(x, y);
        }

        // Accept a Board parameter for consistency across subclasses
        public virtual void PlaceShips(Board board)
        {
            foreach (var ship in Fleet.Ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = new Random().Next(0, board.Grid.Count);
                    int y = new Random().Next(0, board.Grid.Count);
                    bool isHorizontal = new Random().Next(0, 2) == 0;

                    PlacementStatus status = board.PlaceShip(ship, x, y, isHorizontal);
                    if (status == PlacementStatus.Success)
                    {
                        placed = true;
                    }
                }
            }
        }
    }
}
