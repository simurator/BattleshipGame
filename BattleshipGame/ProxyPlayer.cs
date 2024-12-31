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

        // Constructor that accepts a Player type for flexibility
        public ProxyPlayer(string name, int boardSize, int difficulty) : base(name, boardSize)
        {
            // Creating an AIPlayer or HumanPlayer depending on difficulty or other factors
            if (difficulty > 0)
            {
                RealPlayer = new AIPlayer(name, difficulty);  // If difficulty > 0, treat it as an AI
            }
            else
            {
                RealPlayer = new HumanPlayer(name, boardSize);  // Otherwise, treat it as a Human
            }
        }

        public override bool MakeMove(int x, int y, Board enemyBoard)
        {
            return RealPlayer.MakeMove(x, y, enemyBoard);  // Delegate the move to RealPlayer
        }

        public override void PlaceShips(Board board)
        {
            RealPlayer.PlaceShips(board);  // Delegate the ship placement to RealPlayer
        }
    }
}
