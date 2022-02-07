using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #region private fields
        /// <summary>
        /// Holds the current state of each cell
        /// </summary>
        private MarkType[] marks;

        /// <summary>
        /// Holds the list of buttons
        /// </summary>
        private Button[] buttons;

        /// <summary>
        /// True if currently player 1's turn
        /// </summary>
        private bool player1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool gameOver;
        #endregion

        /// <summary>
        /// Starts a new game and clears the grid
        /// </summary>
        private void NewGame()
        {
            ///Create a new blank array of 9 MarkType objects
            marks = new MarkType[9];
            for (int i = 0; i < marks.Length; i++)
            {
                marks[i] = MarkType.Blank;
            }

            // Set turn to player 1
            player1Turn = true;

            // The game is not over
            gameOver = false;

            container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                /// <summary>
                /// Set content and colors to defaults
                /// </summary>
                button.Content = string.Empty;
                button.Foreground = Brushes.Blue;
                button.Background = Brushes.White;
            });

            buttons = container.Children.Cast<Button>().ToList().ToArray();
        }

        /// <summary>
        /// Handles a Button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The click event</param>
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = row + (column * 3);

            // If the cell has already been selected, do nothing
            if (marks[index] != MarkType.Blank)
            {
                return;
            }

            // If player 1 is selecting, set to X, else set to O
            marks[index] = player1Turn ? MarkType.X : MarkType.O;
            
            // Toggle mark colors
            button.Foreground = player1Turn ? Brushes.Blue : Brushes.Red;

            // Set the text of the button
            button.Content = player1Turn ? "X" : "O";

            // Change player turn
            player1Turn = !player1Turn;

            CheckWin();
        }

        /// <summary>
        /// Checks if either player has won
        /// </summary>
        private void CheckWin()
        {
            // Check rows and columns
            for (int i = 0; i < 3; i+=3)
            {
                int row = i * 3;

                // Check rows
                if (marks[row] != MarkType.Blank && marks[row] == marks[row + 1] && marks[row] == marks[row + 2])
                {
                    buttons[row].Background = buttons[row + 1].Background = buttons[row + 2].Background = Brushes.LightBlue;
                    gameOver = true;
                }

                // Check cols
                if (marks[i] != MarkType.Blank && marks[i] == marks[i + 3] && marks[i] == marks[i + 6])
                {
                    buttons[i].Background = buttons[i + 3].Background = buttons[i + 6].Background = Brushes.LightBlue;
                    gameOver = true;
                }

            }

            //Check diags
            if (marks[0] != MarkType.Blank && marks[0] == marks[4] && marks[0] == marks[8])
            {
                buttons[0].Background = buttons[4].Background = buttons[8].Background =  Brushes.LightBlue;
                gameOver = true;
            }
            if (marks[2] != MarkType.Blank && marks[2] == marks[4] && marks[2] == marks[6])
            {
                buttons[2].Background = buttons[4].Background = buttons[6].Background = Brushes.LightBlue;
                gameOver = true;
            }

            // Check for full board
            if (!marks.Any(result => result == MarkType.Blank))
            {
                container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
                gameOver = true;
            }

        }
    }
}
