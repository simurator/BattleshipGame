using System;
using System.Linq;



    

namespace BattleshipGame
    {
        internal class AttackState : GameState
        {
            public override void NextState(Game game)
            {
                // Check if any player has lost their fleet
                if (game.Players.Any(player => player.Fleet.IsDefeated()))
                {
                    game.State = new EndGameState();  // Transition to EndGameState
                    Console.WriteLine("Game state changed to EndGameState.");
                }
                else
                {
                    game.SwitchTurns();  // Switch to the next player
                    Console.WriteLine("Game state changed to SwitchTurns.");
                }
            }

        public override void Handle(Game game)
        {
            var currentPlayer = game.Players[game.CurrentPlayerIndex];
            Console.WriteLine($"{currentPlayer.Name}, it's your turn to attack.");

            int x = -1, y = -1;

            // Pobierz poprawne współrzędne
            GetValidCoordinates(game, ref x, ref y);

            // Zarejestruj trafienie na planszy, przekazując obiekt game
            bool hit = game.Board.RegisterHit(x, y);

            // Obsłuż wynik ataku
            if (hit)
            {
                Console.WriteLine($"{currentPlayer.Name} hit a ship at ({x}, {y})!");

                // Zaktualizuj flotę przeciwnika po trafieniu
                UpdateFleetWithHit(game, x, y);
            }
            else
            {
                Console.WriteLine($"{currentPlayer.Name} missed at ({x}, {y}).");
            }

            // Przejdź do następnego stanu gry po ataku
            NextState(game);
        }


        // Method to handle coordinate input and ensure it's valid
        private void GetValidCoordinates(Game game, ref int x, ref int y)
            {
                bool validInput = false;

                while (!validInput)
                {
                    try
                    {
                        Console.Write("Enter X coordinate (0 to {0}): ", game.Board.Grid.Count - 1);
                        x = int.Parse(Console.ReadLine());

                        Console.Write("Enter Y coordinate (0 to {0}): ", game.Board.Grid.Count - 1);
                        y = int.Parse(Console.ReadLine());

                        if (x >= 0 && x < game.Board.Grid.Count && y >= 0 && y < game.Board.Grid.Count)
                        {
                            validInput = true;  // Valid input
                        }
                        else
                        {
                            Console.WriteLine("Coordinates are out of bounds. Try again.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter valid integers for coordinates.");
                    }
                }
            }

            // Method to update the Fleet after a successful hit
            private void UpdateFleetWithHit(Game game, int x, int y)
            {
                // Check which player's fleet is affected by the hit
                var enemyPlayer = game.Players[(game.CurrentPlayerIndex + 1) % game.Players.Count];

                foreach (var ship in enemyPlayer.Fleet.Ships)
                {
                    if (ship.Position.Any(pos => pos.Item1 == x && pos.Item2 == y))
                    {
                        ship.TakeHit(x, y);
                        break;
                    }
                }
            }
        }
    }

