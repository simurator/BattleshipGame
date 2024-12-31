using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Game
    {
        public Board Board { get; private set; }
        public List<Player> Players { get; private set; }
        public GameState State { get; set; }
        public List<PlayerCommand> History { get; private set; }
        public int CurrentPlayerIndex { get; private set; }

        public Game(Board board, List<Player> players, string mode)
        {
            Board = board;
            Players = players;
            State = new SetupState();
            History = new List<PlayerCommand>();
            CurrentPlayerIndex = 0;
        }

        public void StartGame()
        {
            Console.WriteLine("Game starting...");
            State.Handle(this);
        }

        public void SwitchTurns()
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
            Console.WriteLine($"It's now {Players[CurrentPlayerIndex].Name}'s turn.");
        }

        public void EndGame()
        {
            Console.WriteLine("Game has ended.");
        }
    }
}
