using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class EndGameState : GameState
    {
        public override void NextState(Game game)
        {
            Console.WriteLine("Game has ended. No further states.");
        }

        public override void Handle(Game game)
        {
            var winner = game.Players.Find(player => !player.Fleet.IsDefeated());
            Console.WriteLine(winner != null ? $"{winner.Name} wins!" : "It's a draw!");
            game.EndGame();
        }
    }
}
