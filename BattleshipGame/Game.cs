using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Game
    {
        public List<Board> Boards { get; private set; }
        public List<Player> Players { get; private set; }
        public GameState State { get; set; }
        public List<PlayerCommand> History { get; private set; }
        public int CurrentPlayerIndex { get; set; }

        public Game(int boardSize, List<Player> players, string mode)
        {
            Players = players;
            Boards = new List<Board>
            {
                new Board(boardSize, this),  // Przekazanie referencji this do Board
                new Board(boardSize, this)   // Przekazanie referencji this do Board
            };
            State = new SetupState();
            History = new List<PlayerCommand>();
            CurrentPlayerIndex = 0;
        }

        public void SwitchTurns()
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
            Console.WriteLine($"\nIt's now {Players[CurrentPlayerIndex].Name}'s turn.");
        }

        public void StartGame()
        {
            Console.WriteLine("Game starting...");
            while (!(State is EndGameState))
            {
                State.Handle(this);
            }
            State.Handle(this);  // Obsłuż stan końcowy
        }

        public void EndGame()
        {
            Console.WriteLine("Game has ended.");
        }

        public Board GetCurrentPlayerBoard()
        {
            return Boards[CurrentPlayerIndex];
        }

        public Board GetEnemyBoard()
        {
            return Boards[(CurrentPlayerIndex + 1) % Players.Count];
        }
    }
}
