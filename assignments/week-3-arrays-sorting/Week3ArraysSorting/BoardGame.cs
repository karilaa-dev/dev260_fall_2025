using System;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Connect Four implementation for Assignment 2 Part A
    /// Demonstrates multi-dimensional arrays with interactive gameplay
    /// 
    /// Learning Focus: 
    /// - Multi-dimensional array manipulation (char[,])
    /// - Console rendering and user input
    /// - Game state management and win detection
    /// - Gravity simulation for token dropping
    /// </summary>
    public class BoardGame
    {
        // Connect Four board: 6 rows, 7 columns
        private char[,] board = new char[6, 7];
        
        // Game state fields
        private char currentPlayer = 'X';
        private bool gameOver = false;
        private string winner = "";
        private const int GameRows = 6;
        private const int GameCols = 7;
        private const int GameWinLength = 4;

        /// <summary>
        /// Constructor - Initialize the Connect Four game
        /// </summary>
        public BoardGame()
        {
            InitializeBoard();
        }
        
        /// <summary>
        /// Initialize the board with empty spaces
        /// </summary>
        private void InitializeBoard()
        {
            for (int row = 0; row < GameRows; row++)
            {
                for (int col = 0; col < GameCols; col++)
                {
                    board[row, col] = '.';
                }
            }
        }
        
        /// <summary>
        /// Main game loop - handles the complete game session
        /// </summary>
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== BOARD GAME (Part A) ===");
            Console.WriteLine();
            
            DisplayInstructions();
            
            bool playAgain = true;
            
            while (playAgain)
            {
                InitializeNewGame();
                
                PlayOneGame();
                
                playAgain = AskPlayAgain();
            }
            
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Display game instructions and controls
        /// </summary>
        private void DisplayInstructions()
        {
            Console.WriteLine("CONNECT FOUR RULES:");
            Console.WriteLine("- Players take turns dropping tokens (X and O)");
            Console.WriteLine("- Enter column number (1-7) when prompted");
            Console.WriteLine("- Tokens fall to the lowest empty slot in the column");
            Console.WriteLine("- First to get 4 tokens in a row wins!");
            Console.WriteLine("- Winning lines can be horizontal, vertical, or diagonal");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Initialize/reset the game for a new round
        /// </summary>
        private void InitializeNewGame()
        {
            InitializeBoard();
            currentPlayer = 'X';
            gameOver = false;
            winner = "";
        }
        
        /// <summary>
        /// Play one complete game until win/draw/quit
        /// </summary>
        private void PlayOneGame()
        {
            while (!gameOver)
            {
                RenderBoard();
                GetPlayerMove();
                CheckWinCondition();
                
                if (!gameOver)
                {
                    SwitchPlayer();
                }
            }
            
            // Final board display and result
            RenderBoard();
            DisplayGameResult();
        }
        
        /// <summary>
        /// Render the current board state to console
        /// </summary>
        private void RenderBoard()
        {
            Console.WriteLine();
            Console.WriteLine("   1 2 3 4 5 6 7");
            Console.WriteLine(" +---------------+");
            
            for (int row = 0; row < GameRows; row++)
            {
                Console.Write($"{row + 1}|");
                for (int col = 0; col < GameCols; col++)
                {
                    Console.Write($" {board[row, col]}");
                }
                Console.WriteLine(" |");
            }
            
            Console.WriteLine(" +---------------+");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Get and validate player move input
        /// </summary>
        private void GetPlayerMove()
        {
            bool validMove = false;
            
            while (!validMove)
            {
                Console.Write($"Player {currentPlayer}, enter column (1-7): ");
                string? input = Console.ReadLine();
                
                if (int.TryParse(input, out int userInput))
                {
                    if (userInput >= 1 && userInput <= 7)
                    {
                        int column = userInput - 1; // Convert to 0-based index
                        if (DropToken(column, currentPlayer))
                        {
                            validMove = true;
                        }
                        else
                        {
                            Console.WriteLine("Column is full! Choose another column.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Column must be between 1 and 7!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                }
            }
        }
        
        /// <summary>
        /// Drop a token in the specified column with gravity
        /// Returns true if successful, false if column is full
        /// </summary>
        private bool DropToken(int column, char player)
        {
            // Start from bottom row and find first empty slot
            for (int row = GameRows - 1; row >= 0; row--)
            {
                if (board[row, column] == '.')
                {
                    board[row, column] = player;
                    return true;
                }
            }
            return false; // Column is full
        }
        
        /// <summary>
        /// Check if current board state has a winner or draw
        /// </summary>
        private void CheckWinCondition()
        {
            // Check for win conditions
            if (CheckHorizontalWin() || CheckVerticalWin() || 
                CheckDiagonalWin() || CheckAntiDiagonalWin())
            {
                gameOver = true;
                winner = currentPlayer.ToString();
                return;
            }
            
            // Check for draw (top row is full)
            if (IsBoardFull())
            {
                gameOver = true;
                winner = "Draw";
            }
        }
        
        /// <summary>
        /// Check for horizontal win (4 in a row)
        /// </summary>
        private bool CheckHorizontalWin()
        {
            for (int row = 0; row < GameRows; row++)
            {
                for (int col = 0; col <= GameCols - GameWinLength; col++)
                {
                    if (CheckLine(row, col, 0, 1))
                        return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Check for vertical win (4 in a column)
        /// </summary>
        private bool CheckVerticalWin()
        {
            for (int row = 0; row <= GameRows - GameWinLength; row++)
            {
                for (int col = 0; col < GameCols; col++)
                {
                    if (CheckLine(row, col, 1, 0))
                        return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Check for diagonal win (top-left to bottom-right)
        /// </summary>
        private bool CheckDiagonalWin()
        {
            for (int row = 0; row <= GameRows - GameWinLength; row++)
            {
                for (int col = 0; col <= GameCols - GameWinLength; col++)
                {
                    if (CheckLine(row, col, 1, 1))
                        return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Check for anti-diagonal win (top-right to bottom-left)
        /// </summary>
        private bool CheckAntiDiagonalWin()
        {
            for (int row = 0; row <= GameRows - GameWinLength; row++)
            {
                for (int col = GameWinLength - 1; col < GameCols; col++)
                {
                    if (CheckLine(row, col, 1, -1))
                        return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Check if there are 4 consecutive tokens in a line
        /// </summary>
        private bool CheckLine(int startRow, int startCol, int rowDelta, int colDelta)
        {
            char firstToken = board[startRow, startCol];
            if (firstToken == '.') return false;
            
            for (int i = 1; i < GameWinLength; i++)
            {
                int row = startRow + i * rowDelta;
                int col = startCol + i * colDelta;
                
                if (board[row, col] != firstToken)
                    return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Check if the board is full (draw condition)
        /// </summary>
        private bool IsBoardFull()
        {
            for (int col = 0; col < GameCols; col++)
            {
                if (board[0, col] == '.')
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// Ask player if they want to play another game
        /// </summary>
        private bool AskPlayAgain()
        {
            while (true)
            {
                Console.Write("Play again? (y/n): ");
                string? input = Console.ReadLine()?.ToLower();
                
                if (input.ToLower() == "y")
                    return true;
                else if (input == "n")
                    return false;
                else
                    Console.WriteLine("Please enter 'y' or 'n'.");
            }
        }
        
        /// <summary>
        /// Display the game result
        /// </summary>
        private void DisplayGameResult()
        {
            Console.WriteLine();
            if (winner == "Draw")
            {
                Console.WriteLine("It's a draw! The board is full.");
            }
            else
            {
                Console.WriteLine($"Player {winner} wins! Congratulations!");
            }
            Console.WriteLine();
        }
        
        /// <summary>
        /// Switch to the next player's turn
        /// </summary>
        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }
}