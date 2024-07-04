using System;

namespace TicTacToeConsole;

public class TicTacToe
{
    public const char X = 'X';
    public const char O = 'O';
    public const char S = '_';
    
    private char[,] _board = null!;
    public bool Finished => Plays == 9 || Winner != S;
    public int Plays { get; private set; }
    public char Winner { get; private set; }
    public char ActualTurn { get; private set; }
    public char NextTurn { get; private set; }

    public TicTacToe()
    {
        Init();
    }

    public void NextPlay(int x, int y)
    {
        // Validate coordinates are in rage 0-2
        if (x < 0 || x > 2) throw new ArgumentOutOfRangeException(nameof(x), x, "X coordinate must be between 0 and 2");
        if (y < 0 || y > 2) throw new ArgumentOutOfRangeException(nameof(y), y, "Y coordinate must be between 0 and 2");

        // Validate game if not finished
        if (Finished) throw new InvalidOperationException("Game is already finished");

        // Make next play
        _board[x, y] = ActualTurn;
        Plays++;

        // Check if there is a winner
        if (Plays > 3)
        {
            // Check rows
            for (var i = 0; i < 3; i++)
                if (_board[i, 0] == _board[i, 1] && _board[i, 0] == _board[i, 2] && _board[i, 0] != S)
                    Winner = ActualTurn;

            // Check columns
            for (var i = 0; i < 3; i++)
                if (_board[0, i] == _board[1, i] && _board[0, i] == _board[2, i] && _board[0, i] != S)
                    Winner = ActualTurn;

            // Check diagonals
            if (_board[0, 0] == _board[1, 1] && _board[0, 0] == _board[2, 2] && _board[0, 0] != S)
                Winner = ActualTurn;

            if (_board[2, 0] == _board[1, 1] && _board[2, 0] == _board[0, 2] && _board[2, 0] != S)
                Winner = ActualTurn;
        }
        
        // Update next player
        (ActualTurn, NextTurn) = (NextTurn, ActualTurn);
    }

    public void Reset()
    {
        Init();
    }

    private void Init()
    {
        _board = new[,]
        {
            { S, S, S },
            { S, S, S },
            { S, S, S }
        };
        Plays = 0;
        Winner = S;
        ActualTurn = X;
        NextTurn = O;
    }

    public string BoardToString() =>
        $"""
         ---------
         | {_board[0,0]} {_board[0,1]} {_board[0,2]} |
         | {_board[1,0]} {_board[1,1]} {_board[1,2]} |
         | {_board[2,0]} {_board[2,1]} {_board[2,2]} |
         ---------
        """;
    
    public override string ToString() =>
        $"""
         Actual Turn: {ActualTurn}
         NextTurn: {NextTurn}
         Winner: {Winner}
         Plays: {Plays}
         {BoardToString()}
         """;
}