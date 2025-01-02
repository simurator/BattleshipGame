using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class GameBuilder
    {
        private Tuple<int, int> BoardSize;
        private List<Tuple<int, int>> ShipTypes;
        private string Mode;
        private int Difficulty; // To store the difficulty for AI player

        public GameBuilder SetBoardSize(int width, int height)
        {
            BoardSize = new Tuple<int, int>(width, height);
            return this;
        }

        public GameBuilder SetMode(string mode)
        {
            Mode = mode;
            return this;
        }

        public GameBuilder SetShipTypes(List<Tuple<int, int>> types)
        {
            ShipTypes = new List<Tuple<int, int>>(types);
            return this;
        }

        public GameBuilder SetDifficulty(int difficulty)
        {
            Difficulty = difficulty;
            return this;
        }

        public Game Build()
        {
            if (BoardSize == null || ShipTypes == null || string.IsNullOrEmpty(Mode))
            {
                throw new InvalidOperationException("Game configuration is incomplete.");
            }

            // Create players
            var players = new List<Player>
            {
                new HumanPlayer("Player 1", BoardSize.Item1)
            };

            // Add second player based on mode
            if (Mode == "PvAI")
            {
                players.Add(new ProxyPlayer("AI Player", BoardSize.Item1, Difficulty));
            }
            else
            {
                players.Add(new HumanPlayer("Player 2", BoardSize.Item1));
            }

            // Add ships to players' fleets
            foreach (var player in players)
            {
                foreach (var shipType in ShipTypes)
                {
                    for (int i = 0; i < shipType.Item2; i++) // Item2 to liczba statków danego typu
                    {
                        CompositeShip ship = shipType.Item1 switch
                        {
                            1 => new OneMastShip(),
                            2 => new TwoMastShip(),
                            3 => new ThreeMastShip(),
                            4 => new FourMastShip(),
                            _ => throw new InvalidOperationException("Invalid ship type.")
                        };
                        player.Fleet.AddShip(ship);
                    }
                }
            }

            return new Game(BoardSize.Item1, players, Mode);
        }
    }
}