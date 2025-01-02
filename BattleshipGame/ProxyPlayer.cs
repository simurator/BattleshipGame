using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class ProxyPlayer : Player
    {
        private Player RealPlayer;

        public ProxyPlayer(string name, int boardSize, int difficulty) : base(name)
        {
            // Create the appropriate player type
            if (difficulty > 0)
            {
                RealPlayer = new AIPlayer(name, difficulty);
            }
            else
            {
                RealPlayer = new HumanPlayer(name, boardSize);
            }

            // Copy the fleet from the base Player to RealPlayer
            foreach (var ship in Fleet.Ships)
            {
                RealPlayer.Fleet.AddShip(ship);
            }
        }

        public override bool MakeMove(int x, int y, Board enemyBoard)
        {
            return RealPlayer.MakeMove(x, y, enemyBoard);
        }

        public override void PlaceShips(Board board)
        {
            RealPlayer.PlaceShips(board);
        }

        // Override property to delegate to RealPlayer's Fleet
        public new Fleet Fleet
        {
            get { return RealPlayer.Fleet; }
        }
    }
}
