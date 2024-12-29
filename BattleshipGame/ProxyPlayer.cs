using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class ProxyPlayer : Player
    {
        private int Difficulty { get; set; }
        private Player RealPlayer;

        public ProxyPlayer(string name, int difficulty) : base(name)
        {
            Difficulty = difficulty;
            RealPlayer = new AIPlayer(name, difficulty);
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
