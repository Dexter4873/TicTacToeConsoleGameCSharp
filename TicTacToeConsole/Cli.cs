using System;
using System.Text.RegularExpressions;

namespace TicTacToeConsole;

public class Cli
{
    private readonly TicTacToe _game = new();
    private readonly Regex _regex = new("^[1-3] [1-3]$");

    public void Start()
    {
        Console.WriteLine("TicTacToe Game Started");
        Console.WriteLine(
            "Enter where you want to put your play in the order: (x) (y). Remember coordinates is between 1 - 3");

        while (!_game.Finished)
        {
            Console.WriteLine(_game.BoardToString());
            string line;
            bool validCords;

            do
            {
                Console.Write($"Turns {_game.ActualTurn}: ");
                line = Console.ReadLine()!;
                validCords = _regex.IsMatch(line);
                if (!validCords) Console.WriteLine("Coordinates you enter are not valid");
            } while (!validCords);
            
            var cords = line.Split(" ");
            _game.NextPlay(int.Parse(cords[0]) - 1, int.Parse(cords[1]) - 1);
        }

        Console.WriteLine(_game.BoardToString());
        Console.WriteLine(_game.Winner != TicTacToe.S ? $"{_game.Winner} wins!!!" : "Draw Game!!!");
    }
}