using System;
using System.Collections.Generic;

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
            // Calculate the index for the 1D list based on coordinates
            int index = Target.Item2 * EnemyBoard.GridSize + Target.Item1;

            // Save the previous state (hit or miss) of the tile
            PreviousState = EnemyBoard.Grid[index].IsHit;

            // Register the hit or miss
            Result = EnemyBoard.RegisterHit(Target.Item1, Target.Item2) ? "Hit" : "Miss";
        }

        public override void Undo()
        {
            // Calculate the index for the 1D list based on coordinates
            int index = Target.Item2 * EnemyBoard.GridSize + Target.Item1;

            // Restore the previous state of the tile
            EnemyBoard.Grid[index].IsHit = (bool)PreviousState;
        }
    }
}
