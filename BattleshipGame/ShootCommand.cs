using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class ShootCommand : PlayerCommand
    {
        private Board EnemyBoard;

        public ShootCommand(Player player, Tuple<int, int> target, Board enemyBoard)
            : base(player, target, "Shoot")
        {
            EnemyBoard = enemyBoard;
        }

        public override void Execute()
        {
            PreviousState = EnemyBoard.Grid[Target.Item2][Target.Item1].IsHit;
            Result = EnemyBoard.RegisterHit(Target.Item1, Target.Item2) ? "Hit" : "Miss";
        }

        public override void Undo()
        {
            EnemyBoard.Grid[Target.Item2][Target.Item1].IsHit = (bool)PreviousState;
        }
    }
}
