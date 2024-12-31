using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class SetupState : GameState
    {
        public override void NextState(Game game)
        {
            game.State = new AttackState();
            Console.WriteLine("Game state changed to AttackState.");
        }

        public override void Handle(Game game)
        {
            Console.WriteLine("Players are placing their ships.");
            foreach (var player in game.Players)
            {
                player.PlaceShips(game.Board);
            }
            NextState(game);
        }
    }
}
