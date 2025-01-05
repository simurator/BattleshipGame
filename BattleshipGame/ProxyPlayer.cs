using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class ProxyPlayer : Player
    {
        public Player RealPlayer { get; private set; }  // Dodane public dla dostępu

        public ProxyPlayer(string name, int boardSize, int difficulty) : base(name)
        {
            RealPlayer = new AIPlayer(name, difficulty);
            Fleet = RealPlayer.Fleet;  // Synchronizacja flot
        }

        public override bool MakeMove(int x, int y, Board enemyBoard)
        {
            return RealPlayer.MakeMove(x, y, enemyBoard);
        }

        public override void PlaceShips(Board board)
        {
            RealPlayer.PlaceShips(board);
        }
    }
}
