using System;

namespace TicTacToe
{
    public class Board
    {
        private Player[,] grid;
        public Player CurrentPlayer
        {
            get;
            private set;
        }

        public Board()
        {
            Reset();
        }

        public void Reset()
        {
            grid = new Player[3, 3];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    grid[row, col] = Player.None;
                }
            }
            CurrentPlayer = Player.X;
        }

        public bool Select(int row, int column)
        {
            if (grid[row, column] == Player.None)
            {
                grid[row, column] = CurrentPlayer;
                CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
                return true;
            }
            return false;
        }

        public Player CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (grid[i, 0] != Player.None && grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2])
                    return grid[i, 0];
                if (grid[0, i] != Player.None && grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i])
                    return grid[0, i];
            }
            if (grid[0, 0] != Player.None && grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
                return grid[0, 0];
            if (grid[0, 2] != Player.None && grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
                return grid[0, 2];

            return Player.None;
        }

        public bool IsTie()
        {
            foreach (var cell in grid)
            {
                if (cell == Player.None)
                    return false;
            }
            return true;
        }
    }
}