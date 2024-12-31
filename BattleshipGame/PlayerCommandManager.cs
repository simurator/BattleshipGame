using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class PlayerCommandManager
    {
        private List<PlayerCommand> CommandHistory = new List<PlayerCommand>();
        private List<PlayerCommand> RedoStack = new List<PlayerCommand>();

        public void ExecuteCommand(PlayerCommand command)
        {
            command.Execute();
            CommandHistory.Add(command);
            RedoStack.Clear();
        }

        public void UndoLastCommand()
        {
            if (CommandHistory.Count > 0)
            {
                var command = CommandHistory[^1];
                command.Undo();
                CommandHistory.RemoveAt(CommandHistory.Count - 1);
                RedoStack.Add(command);
            }
        }

        public void RedoLastCommand()
        {
            if (RedoStack.Count > 0)
            {
                var command = RedoStack[^1];
                command.Execute();
                RedoStack.RemoveAt(RedoStack.Count - 1);
                CommandHistory.Add(command);
            }
        }
    }
}
