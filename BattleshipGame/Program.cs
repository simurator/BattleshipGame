using BattleshipGame;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Battleship Game!");

        // Configure the game using GameBuilder
        var gameBuilder = new GameBuilder()
            .SetBoardSize(10, 10) // 10x10 board
            .SetMode("Standard") // Game mode
            .SetShipTypes(new List<Tuple<int, int>>
            {
                Tuple.Create(1, 4), // Four one-mast ships
                Tuple.Create(2, 3), // Three two-mast ships
                Tuple.Create(3, 2), // Two three-mast ships
                Tuple.Create(4, 1)  // One four-mast ship
            });

        Game game = gameBuilder.Build();

        // Place ships for each player
        foreach (var player in game.Players)
        {
            Console.WriteLine($"{player.Name}, place your ships on the board.");
            player.PlaceShips(game.Board);
        }

        // Start the game
        game.StartGame();

        while (!(game.State is EndGameState))
        {
            game.State.Handle(game);
        }

        Console.WriteLine("Thank you for playing Battleship!");
    }
}
