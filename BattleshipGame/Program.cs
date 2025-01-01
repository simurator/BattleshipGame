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
            .SetMode("Standard")
            .SetShipTypes(new List<Tuple<int, int>>
            {
                Tuple.Create(1, 4), // One 4-mast ship
                Tuple.Create(2, 3), // Two 3-mast ships
                Tuple.Create(3, 2), // Three 2-mast ships
                Tuple.Create(4, 1)  // Four 1-mast ships
            });

        var game = gameBuilder.Build();

        // Start the game setup
        game.StartGame();

        // Main game loop
        while (!(game.State is EndGameState))
        {
            game.State.Handle(game);
        }

        Console.WriteLine("Thank you for playing Battleship!");
    }
}
