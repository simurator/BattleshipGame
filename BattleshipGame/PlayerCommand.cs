using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal abstract class PlayerCommand
    {
        public Player Player { get; protected set; }
        public Tuple<int, int> Target { get; protected set; }
        public string Result { get; protected set; }
        public string CommandType { get; protected set; }
        public object PreviousState { get; protected set; }

        public PlayerCommand(Player player, Tuple<int, int> target, string commandType)
        {
            Player = player;
            Target = target;
            CommandType = commandType;
        }

        public abstract void Execute();
        public abstract void Undo();
    }
}
