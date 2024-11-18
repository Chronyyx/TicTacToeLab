using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Board board;
        private int gamesPlayed = 0;
        private int gamesWon = 0;

        private string blankImagePath = "C:/Users/aidav/source/repos/TicTacToe/TicTacToe/images/blank.png";
        private string xImagePath = "C:/Users/aidav/source/repos/TicTacToe/TicTacToe/images/x.png";
        private string oImagePath = "C:/Users/aidav/source/repos/TicTacToe/TicTacToe/images/o.png";

        public MainWindow()
        {
            InitializeComponent();
            board = new Board();
            ResetGame();
        }

        private void Cell_Click(object sender, MouseButtonEventArgs e)
        {
            Image clickedCell = (Image)sender;
            int cellIndex = int.Parse(clickedCell.Tag.ToString());
            int row = cellIndex / 3;
            int column = cellIndex % 3;

            if (board.Select(row, column))
            {

                clickedCell.Source = new BitmapImage(new Uri(
                    board.CurrentPlayer == Player.O ? xImagePath : oImagePath));

                Player winner = board.CheckWin();
                if (winner != Player.None)
                {
                    MessageBox.Show($"{winner} wins!");
                    gamesPlayed++;

                    if (winner == Player.X)
                    {
                        gamesWon++;
                    }

                    UpdateNotificationArea();
                    ResetGame();
                    return;
                }

                if (board.IsTie())
                {
                    MessageBox.Show("It's a tie!");
                    gamesPlayed++;
                    UpdateNotificationArea();
                    ResetGame();
                    return;
                }

                UpdateNotificationArea();
            }
        }

        private void ResetGame()
        {
            board.Reset();
            foreach (Image cell in GameGrid.Children)
            {
                cell.Source = new BitmapImage(new Uri(blankImagePath));
            }
            UpdateNotificationArea();
        }

        private void UpdateNotificationArea()
        {
            GamesPlayedText.Text = $"Games Played: {gamesPlayed}";
            GamesWonText.Text = $"Games Won: {gamesWon}";
            double winRatio = gamesPlayed == 0 ? 0 : (double)gamesWon / gamesPlayed * 100;
            WinRatioText.Text = $"Win Ratio: {winRatio}%";
            TurnText.Text = $"Turn: Player {board.CurrentPlayer}";
        }
    }
}