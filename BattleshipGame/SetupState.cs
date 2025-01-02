namespace BattleshipGame
{
    internal class SetupState : GameState
    {
        private int currentSetupPlayer = 0;

        public override void Handle(Game game)
        {
            if (currentSetupPlayer < game.Players.Count)
            {
                Console.WriteLine($"\n{game.Players[currentSetupPlayer].Name}'s turn to place ships:");
                game.Players[currentSetupPlayer].PlaceShips(game.Boards[currentSetupPlayer]);
                currentSetupPlayer++;

                if (currentSetupPlayer < game.Players.Count)
                {
                    Console.WriteLine("\nPress any key to continue to next player's setup...");
                    Console.ReadKey();
                    Console.Clear();
                    Handle(game);  // Rekurencyjnie obsłuż następnego gracza
                }
                else
                {
                    Console.WriteLine("\nAll ships placed. Press any key to start the battle!");
                    Console.ReadKey();
                    Console.Clear();
                    NextState(game);
                }
            }
        }

        public override void NextState(Game game)
        {
            if (currentSetupPlayer >= game.Players.Count)
            {
                game.State = new AttackState();
                game.CurrentPlayerIndex = 0;  // Zaczynamy od pierwszego gracza
                Console.WriteLine("Moving to attack phase!");
            }
        }
    }
}