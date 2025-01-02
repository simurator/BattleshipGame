using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class AIPlayer : Player
    {
        private int Difficulty;
        private Random random;

        public AIPlayer(string name, int difficulty) : base(name)
        {
            Difficulty = difficulty;
            random = new Random();
            // Initialize fleet here instead of relying on inheritance
            Fleet = new Fleet();
        }

        public override void PlaceShips(Board board)
        {
            Console.WriteLine($"\nAI player {Name} is placing ships...");

            foreach (var ship in Fleet.Ships)
            {
                bool placed = false;
                int attempts = 0;
                const int maxAttempts = 100; // Zabezpieczenie przed nieskończoną pętlą

                while (!placed && attempts < maxAttempts)
                {
                    attempts++;

                    // Wybierz losowe współrzędne i orientację
                    int x = random.Next(0, board.GridSize);
                    int y = random.Next(0, board.GridSize);
                    bool isHorizontal = random.Next(2) == 0;

                    // Próbuj umieścić statek
                    PlacementStatus status = board.PlaceShip(ship, x, y, isHorizontal);

                    if (status == PlacementStatus.Success)
                    {
                        Console.WriteLine($"AI placed ship of size {ship.Size} at ({x}, {y})");
                        placed = true;
                    }
                }

                if (!placed)
                {
                    Console.WriteLine($"Warning: AI failed to place ship of size {ship.Size} after {maxAttempts} attempts");
                }
            }

            Console.WriteLine("AI finished placing ships.");
            board.DisplayBoard();
        }
    }
}
