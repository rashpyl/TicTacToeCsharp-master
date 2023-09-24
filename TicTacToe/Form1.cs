using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public enum Player
        {
            X, O, None
        }

        Player currentPlayer;
        Random random = new Random();
        int humanWinCount = 0;
        int CPUWinCount = 0;
        List<Button> buttons;
        int rowsAndColumns = 5; // Default grid size
        int buttonSize;

        private Dictionary<string, int> memoizationTable = new Dictionary<string, int>();

        private Player[,] gameBoard;

        int moves = 0;

        public Form1(int gridRowsAndColumns)
        {
            // Set the grid size based on the input parameter
            rowsAndColumns = gridRowsAndColumns;

            gameBoard = new Player[rowsAndColumns, rowsAndColumns];

            InitializeComponent();

            // Initialize the game board layout
            AdjustLayout();

            // Adjust the form size based on the grid size
            AdjustFormSize();

            // Start a new game
            RestartGame(); 
        }

        private void PlayerClickButton(object sender, EventArgs e)
        {
            if (currentPlayer == Player.X)
            {

                var button = (Button)sender;

                button.Text = currentPlayer.ToString();

                // Disable the button after a move
                button.Enabled = false;

                // Change button color for visual feedback
                button.BackColor = Color.PowderBlue;

                // Update the game board matrix with the human's move
                int rowIndex = button.TabIndex / rowsAndColumns;
                int colIndex = button.TabIndex % rowsAndColumns;
                gameBoard[rowIndex, colIndex] = currentPlayer;

                // Increment the moves count
                moves++;

                // Remove the button from the available moves
                buttons.Remove(button);

                // Check if the game has ended
                CheckGame();

                currentPlayer = Player.O;

                // Start the CPU's turn timer
                CPUTimer.Start();
            }
        }

        private void RestartGame(object sender, EventArgs e)
        {

            // Restart the game when the restart button is clicked
            RestartGame(); 
        }

        private int GetRandomMove()
        {
            List<int> availableMoves = new List<int>();

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Enabled)
                {
                    availableMoves.Add(i);
                }
            }

            // Select a random move from the available moves
            int randomIndex = random.Next(availableMoves.Count);
            return availableMoves[randomIndex];
        }

        private int GetBestMove()
        {
            int bestMove = -1;

            if (moves < (rowsAndColumns * rowsAndColumns) - 9)
            {
                bestMove = GetRandomMove();
            }

            else
            {
                int bestScore = int.MinValue;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Enabled)
                    {
                        int rowIndex = buttons[i].TabIndex / rowsAndColumns;
                        int colIndex = buttons[i].TabIndex % rowsAndColumns;

                        // Make a copy of the game board
                        Player[,] board = CloneGameBoard(gameBoard);

                        // Update the copy with the potential move
                        board[rowIndex, colIndex] = currentPlayer;

                        // Calculate the score using MiniMax on the copy
                        int score = MiniMax(board, moves + 1, false);

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = i;
                        }
                    }
                }
            }
            // Update the gameBoard with the best move
            if (bestMove != -1)
            {
                int rowIndex = buttons[bestMove].TabIndex / rowsAndColumns;
                int colIndex = buttons[bestMove].TabIndex % rowsAndColumns;
                gameBoard[rowIndex, colIndex] = Player.O; // Assuming AI is Player.O
            }

            return bestMove;
        }

        private int MiniMax(Player[,] currentBoard, int depth, bool isMaximizing)
        {
            // Convert the current board state into a string representation
            string boardKey = BoardToString(currentBoard);

            // Check if the score for this board state is already cached
            if (memoizationTable.ContainsKey(boardKey))
            {
                return memoizationTable[boardKey];
            }

            // Check for a win for Player.O (CPU)
            if (CheckWin(currentBoard, Player.O))
            {
                memoizationTable[boardKey] = rowsAndColumns * rowsAndColumns + 1;
                return memoizationTable[boardKey];
            }

            // Check for a win for Player.X (human)
            if (CheckWin(currentBoard, Player.X))
            {
                memoizationTable[boardKey] = -(rowsAndColumns * rowsAndColumns + 1);
                return memoizationTable[boardKey];
            }
            else if (depth == rowsAndColumns * rowsAndColumns)
            {
                memoizationTable[boardKey] = 0;
                return memoizationTable[boardKey];
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < rowsAndColumns; i++)
                {
                    for (int j = 0; j < rowsAndColumns; j++)
                    {
                        if (currentBoard[i, j] == Player.None)
                        {
                            currentBoard[i, j] = Player.O;
                            int score = MiniMax(currentBoard, depth + 1, false);
                            currentBoard[i, j] = Player.None;
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                memoizationTable[boardKey] = bestScore;
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < rowsAndColumns; i++)
                {
                    for (int j = 0; j < rowsAndColumns; j++)
                    {
                        if (currentBoard[i, j] == Player.None)
                        {
                            currentBoard[i, j] = Player.X;
                            int score = MiniMax(currentBoard, depth + 1, true);
                            currentBoard[i, j] = Player.None;
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                memoizationTable[boardKey] = bestScore;
                return bestScore;
            }
        }

        // Helper function to convert the board state into a string for caching
        private string BoardToString(Player[,] board)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < rowsAndColumns; i++)
            {
                for (int j = 0; j < rowsAndColumns; j++)
                {
                    builder.Append((int)board[i, j]); // Convert Player enum to int
                }
            }
            return builder.ToString();
        }

        private void CPUmove(object sender, EventArgs e)
        {
            if (buttons.Count > 0 && currentPlayer == Player.O)
            {
                // Use the GetBestMove function to find the best move for the CPU
                int bestMoveIndex = GetBestMove();

                // Disable the selected button to prevent further moves on it
                buttons[bestMoveIndex].Enabled = false;

                // Set the text of the selected button to 'O' (CPU's symbol)
                buttons[bestMoveIndex].Text = currentPlayer.ToString();

                // Change the background color of the selected button for visual feedback
                buttons[bestMoveIndex].BackColor = Color.DarkOrchid;

                // Remove the selected button from the list of available moves
                buttons.RemoveAt(bestMoveIndex);

                currentPlayer = Player.X;

                moves++;

                // Check if the game has ended after the CPU's move
                CheckGame();
            }
        }

        // This method creates a clone (copy) of a 2D game board representing the state of a Tic-Tac-Toe game.

        private Player[,] CloneGameBoard(Player[,] originalBoard)
        {
            // Get the number of rows and columns in the original game board.
            int rows = originalBoard.GetLength(0);
            int cols = originalBoard.GetLength(1);

            // Create a new 2D array to store the cloned game board with the same dimensions.
            Player[,] clonedBoard = new Player[rows, cols];

            // Iterate through each cell of the original board and copy the player's mark to the cloned board.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    clonedBoard[i, j] = originalBoard[i, j];
                }
            }

            // Return the cloned game board, which now contains an identical copy of the original game state.
            return clonedBoard;
        }


        private void CheckGame()
        {
            // Check if the human player (X) has won
            if (CheckWin(Player.X.ToString()))
            {
                CPUTimer.Stop(); // Stop the CPU's turn timer
                MessageBox.Show("Human Wins!"); // Display a message indicating human victory
                humanWinCount++; // Increment the human win count
                label1.Text = "Human Wins- " + humanWinCount; // Update the win count label

                // Restart the game after a win
                RestartGame();
            }

            // Check if the CPU player (O) has won
            else if (CheckWin(Player.O.ToString()))
            {
                CPUTimer.Stop(); // Stop the CPU's turn timer
                MessageBox.Show("CPU Wins!"); // Display a message indicating CPU victory
                CPUWinCount++; // Increment the CPU win count
                label2.Text = "CPU Wins- " + CPUWinCount; // Update the win count label

                // Restart the game after a win
                RestartGame();
            }

            // Check if the game has ended in a tie (no more available moves)
            else if (buttons.Count == 0)
            {
                // Display a message indicating a tie game
                MessageBox.Show("Tie game!"); 

                // Restart the game after a tie
                RestartGame();
            }
        }

        private void AdjustLayout()
        {
            // Calculate the button size and maximum overall size (width or height, whichever is smaller)
            var maxOverallSize = Math.Min(ClientSize.Width, ClientSize.Height);

            // Determine the size of each button
            buttonSize = maxOverallSize / rowsAndColumns; 

            // Create and position buttons based on the grid size
            buttons = new List<Button>();
            for (int i = 0; i < rowsAndColumns; i++)
            {
                for (int j = 0; j < rowsAndColumns; j++)
                {
                    var button = new Button();

                    // Set button position
                    button.Location = new Point(j * buttonSize, i * buttonSize);
                    // Assign a unique name to the button
                    button.Name = $"button{i * rowsAndColumns + j + 1}";
                    // Set button size
                    button.Size = new Size(buttonSize, buttonSize);
                    // Set button tab index
                    button.TabIndex = i * rowsAndColumns + j;
                    // Enable visual styling for the button
                    button.UseVisualStyleBackColor = true;
                    // Set button font size
                    button.Font = new Font("Microsoft Sans Serif", 18F);

                    // Attach the event handler for button clicks
                    button.Click += PlayerClickButton;
                    // Add the button to the list of buttons
                    buttons.Add(button);
                    // Add the button to the form's controls
                    Controls.Add(button);
                }
            }

            // Update the positions of labels and the Restart button
            label1.Location = new Point(10, maxOverallSize + 20); // Position of the first label
            label2.Location = new Point(maxOverallSize - label2.Width - 10, maxOverallSize + 20); // Position of the second label

            buttonRestart.Location = new Point((maxOverallSize - buttonRestart.Width) / 2, maxOverallSize + 60); // Position of the Restart button

            // Update the form size to fit the grid and additional UI elements
            Size = new Size(maxOverallSize, maxOverallSize + 130);
        }

        private bool CheckWin(Player[,] board, Player player)
        {
            // Check rows
            for (int row = 0; row < rowsAndColumns; row++)
            {
                int count = 0;
                for (int col = 0; col < rowsAndColumns; col++)
                {
                    if (board[row, col] == player)
                    {
                        count++;
                        if (count == rowsAndColumns)
                            return true;
                    }
                }
            }

            // Check columns
            for (int col = 0; col < rowsAndColumns; col++)
            {
                int count = 0;
                for (int row = 0; row < rowsAndColumns; row++)
                {
                    if (board[row, col] == player)
                    {
                        count++;
                        if (count == rowsAndColumns)
                            return true;
                    }
                }
            }

            // Check diagonals
            int diagonal1Count = 0;
            int diagonal2Count = 0;
            for (int i = 0; i < rowsAndColumns; i++)
            {
                if (board[i, i] == player)
                {
                    diagonal1Count++;
                    if (diagonal1Count == rowsAndColumns)
                        return true;
                }

                if (board[i, rowsAndColumns - i - 1] == player)
                {
                    diagonal2Count++;
                    if (diagonal2Count == rowsAndColumns)
                        return true;
                }
            }

            return false;
        }

        // Check for a win for the specified player symbol
        private bool CheckWin(string player)
        {
            // Check rows
            for (int row = 0; row < rowsAndColumns; row++)
            {
                int count = 0;
                for (int col = 0; col < rowsAndColumns; col++)
                {
                    var button = GetButton(row, col);
                    if (button != null && button.Text == player)
                    {
                        count++;
                        if (count == rowsAndColumns)
                            return true;
                    }
                }
            }

            // Check columns
            for (int col = 0; col < rowsAndColumns; col++)
            {
                int count = 0;
                for (int row = 0; row < rowsAndColumns; row++)
                {
                    var button = GetButton(row, col);
                    if (button != null && button.Text == player)
                    {
                        count++;
                        if (count == rowsAndColumns)
                            return true;
                    }
                }
            }

            // Check diagonals
            int diagonal1Count = 0;
            int diagonal2Count = 0;
            for (int i = 0; i < rowsAndColumns; i++)
            {
                var button1 = GetButton(i, i);
                var button2 = GetButton(i, rowsAndColumns - i - 1);

                if (button1 != null && button1.Text == player)
                {
                    diagonal1Count++;
                    if (diagonal1Count == rowsAndColumns)
                        return true;
                }

                if (button2 != null && button2.Text == player)
                {
                    diagonal2Count++;
                    if (diagonal2Count == rowsAndColumns)
                        return true;
                }
            }

            return false;
        }

        private Button GetButton(int row, int col)
        {
            string buttonName = "button" + (row * rowsAndColumns + col + 1);
            return Controls.OfType<Button>().FirstOrDefault(btn => btn.Name == buttonName);
        }

        private void AdjustFormSize()
        {
            int formWidth = buttonSize * rowsAndColumns + 50; // Add some padding
            int formHeight = buttonSize * rowsAndColumns + 100 + buttonRestart.Height; // Add some padding
            Size = new Size(formWidth, formHeight);
        }

        private void RestartGame()
        {
            buttons = Controls.OfType<Button>().ToList();

            buttons.Remove(buttonRestart);

            moves = 0;

            foreach (Button button in buttons)
            {
                if (button.Name != "buttonRestart")
                {
                    button.Enabled = true;
                    button.Text = "?";
                    button.BackColor = DefaultBackColor;
                }
            }

            // Clear the game board matrix
            for (int i = 0; i < rowsAndColumns; i++)
            {
                for (int j = 0; j < rowsAndColumns; j++)
                {
                    gameBoard[i, j] = Player.None;
                }
            }
        }
    }
}
