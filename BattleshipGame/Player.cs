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

        public Player(string name)
        {
            Name = name;
            Fleet = new Fleet();
        }

        public virtual bool MakeMove(int x, int y, Board enemyBoard)
        {
            return enemyBoard.RegisterHit(x, y);
        }

        public virtual void PlaceShips(Board board)
        {
            // Logic to place ships, to be implemented by derived classes or overridden
        }
    }
}
