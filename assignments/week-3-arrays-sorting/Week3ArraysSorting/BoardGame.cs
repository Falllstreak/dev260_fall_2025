using System;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Board Game implementation for Assignment 2 Part A
    /// Demonstrates multi-dimensional arrays with interactive gameplay
    /// 
    /// Learning Focus: 
    /// - Multi-dimensional array manipulation (char[,])
    /// - Console rendering and user input
    /// - Game state management and win detection
    /// 
    /// Choose ONE game to implement:
    /// - Tic-Tac-Toe (3x3 grid)
    /// - Connect Four (6x7 grid with gravity)
    /// - Or something else creative using a 2D array! (I need to be able to understand the rules from your instructions)
    /// </summary>
    public class BoardGame
    {
        // TODO: Choose your game and declare the appropriate board
        // Option 1: Tic-Tac-Toe
        private char[,] board = new char[3, 3];

        // Option 2: Connect Four  
        // private char[,] board = new char[6, 7]; // 6 rows, 7 columns
        
        // Option 3: Your own game
        // private char[,] board = new char[ROWS, COLUMNS]; // Define your own size
        
        // TODO: Add fields for game state
        private char currentPlayer = 'X';
        private bool gameOver = false;
        private string winner = "";

        /// <summary>
        /// Constructor - Initialize the board game
        /// TODO: Set up your chosen game
        /// </summary>
        public BoardGame()
        {
            // TODO: Initialize your board array
            // For Tic-Tac-Toe or Connect Four, fill with empty spaces or dots
            // ❌ ⭕ -> use for Tic-Tac-Toe if you'd like for each square/player and the white box from array example

            Console.WriteLine("BoardGame constructor - TODO: Initialize your chosen game");
        }
        
        /// <summary>
        /// Main game loop - handles the complete game session
        /// TODO: Implement the full game experience
        /// </summary>
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== BOARD GAME (Part A) ===");
            Console.WriteLine();
            
            // TODO: Display game instructions
            DisplayInstructions();
            
            // TODO: Implement main game loop
            bool playAgain = true;
            
            while (playAgain)
            {
                // TODO: Reset game state for new game
                InitializeNewGame();
                
                // TODO: Play one complete game
                PlayOneGame();
                
                // TODO: Ask if player wants to play again
                playAgain = AskPlayAgain();
            }
            
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Display game instructions and controls
        /// TODO: Customize for your chosen game
        /// </summary>
        private void DisplayInstructions()
        {
            Console.WriteLine("TODO: Add instructions for your chosen game");
            Console.WriteLine();
            
            // Example for Tic-Tac-Toe:
            Console.WriteLine("TIC-TAC-TOE RULES:");
            Console.WriteLine("- Players take turns placing X and O");
            Console.WriteLine("- Enter row and column (0-2) when prompted");
            Console.WriteLine("- First to get 3 in a row wins!");
            
            // Example for Connect Four:
            // Console.WriteLine("CONNECT FOUR RULES:");
            // Console.WriteLine("- Players take turns dropping tokens");
            // Console.WriteLine("- Enter column number (0-6) when prompted");
            // Console.WriteLine("- First to get 4 in a row wins!");
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Initialize/reset the game for a new round
        /// TODO: Reset board and game state
        /// </summary>
        private void InitializeNewGame()
        {
            // TODO: Clear the board array
            // TODO: Reset current player to 'X'
            // TODO: Reset game over flag
            // TODO: Clear winner
            
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    board[r, c] = ' '; // empty space
            
            currentPlayer = 'X';
            gameOver = false;
            winner = "";
            
            Console.WriteLine("TODO: Initialize new game state");
        }
        
        /// <summary>
        /// Play one complete game until win/draw/quit
        /// TODO: Implement the core game loop
        /// </summary>
        private void PlayOneGame()
        {
            // TODO: Game loop structure:
            while (!gameOver)
            {
                RenderBoard();
                GetPlayerMove();
                CheckWinCondition();

                if (!gameOver)
                    SwitchPlayer();
            }

            RenderBoard();

            if (winner == "Draw")
                Console.WriteLine("The game ended in a draw!");
            else
                Console.WriteLine($"Player {(winner == "X" ? "❌" : "⭕")} wins the game!");

            // Console.WriteLine("TODO: Implement main game loop");
            // Console.WriteLine("This should include:");
            // Console.WriteLine("1. Render board to console");
            // Console.WriteLine("2. Get and validate player input");
            // Console.WriteLine("3. Update board with move");
            // Console.WriteLine("4. Check for win/draw conditions");
            // Console.WriteLine("5. Switch to next player");
        }
        
        /// <summary>
        /// Render the current board state to console
        /// TODO: Create clear, readable board display
        /// </summary>
        private void RenderBoard()
        {
            // TODO: Display your multi-dimensional array as a visual board
            // Requirements:
            // - Clear, human-readable format
            // - Show current board state
            // - Include row/column labels for easy reference

            Console.Clear();
            Console.WriteLine("   0   1   2 ");
            Console.WriteLine("  -----------");

            for (int row = 0; row < 3; row++)
            {
                Console.Write($"{row} |");
                for (int col = 0; col < 3; col++)
                {
                    char cell = board[row, col];
                    string display = cell switch
                    {
                        'X' => "❌",
                        'O' => "⭕",
                        _ => "⬜"
                    };
                    Console.Write($" {display} |");
                }
                Console.WriteLine();
                Console.WriteLine("  -----------");
            }
            Console.WriteLine();
            
            // Console.WriteLine("TODO: Render board array to console");
        }
        
        /// <summary>
        /// Get and validate player move input
        /// TODO: Handle user input with validation
        /// </summary>
        private void GetPlayerMove()
        {
            // TODO: Prompt current player for their move
            // TODO: Validate input (in bounds, empty cell, etc.)
            // TODO: Keep asking until valid move is entered
            
            // Console.WriteLine("TODO: Get and validate player move");
            
            // Example input validation structure:
            bool validMove = false;
            while (!validMove)
            {
                Console.Write($"Player {currentPlayer}, enter your move: ");
                Console.Write("Enter row (0-2): ");
                string rowInput = Console.ReadLine();
                Console.Write("Enter column (0-2): ");
                string colInput = Console.ReadLine();

                // Try parsing the inputs
                if (int.TryParse(rowInput, out int row) &&
                    int.TryParse(colInput, out int col))
                {
                    // Check bounds
                    if (row >= 0 && row < 3 && col >= 0 && col < 3)
                    {
                        // Check if cell is empty
                        if (board[row, col] == ' ' || board[row, col] == '\0')
                        {
                            board[row, col] = currentPlayer; // Place X or O
                            validMove = true;
                        }
                        else
                        {
                            Console.WriteLine("That cell is already taken. Try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Row and column must be between 0 and 2. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter numeric values only.");
                }
                
                // Parse and validate input
                // Set validMove = true when valid move found
            }
        }
        
        /// <summary>
        /// Check if current board state has a winner or draw
        /// TODO: Implement win detection logic
        /// </summary>
        private void CheckWinCondition()
        {
            // TODO: Check for win conditions specific to your game
            
            // For Tic-Tac-Toe:
            // - Check all rows, columns, and diagonals for 3 in a row
            // - Check for draw (board full, no winner)

            // Check rows
            for (int r = 0; r < 3; r++)
            {
                if (board[r, 0] != ' ' &&
                    board[r, 0] == board[r, 1] &&
                    board[r, 1] == board[r, 2])
                {
                    winner = board[r, 0].ToString();
                    gameOver = true;
                    return;
                }
            }

            // Check columns
            for (int c = 0; c < 3; c++)
            {
                if (board[0, c] != ' ' &&
                    board[0, c] == board[1, c] &&
                    board[1, c] == board[2, c])
                {
                    winner = board[0, c].ToString();
                    gameOver = true;
                    return;
                }
            }

            // Check diagonals
            if (board[0, 0] != ' ' &&
                board[0, 0] == board[1, 1] &&
                board[1, 1] == board[2, 2])
            {
                winner = board[0, 0].ToString();
                gameOver = true;
                return;
            }

            if (board[0, 2] != ' ' &&
                board[0, 2] == board[1, 1] &&
                board[1, 1] == board[2, 0])
            {
                winner = board[0, 2].ToString();
                gameOver = true;
                return;
            }

            // Check for draw (board full, no winner)
            bool boardFull = true;
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (board[r, c] == ' ' || board[r, c] == '\0')
                    {
                        boardFull = false;
                        break;
                    }
                }
            }

            if (boardFull)
            {
                gameOver = true;
                winner = "Draw";
            }
            
            // For Connect Four:
            // - Check horizontal, vertical, and diagonal lines for 4 in a row
            // - Check for draw (top row full, no winner)
            
            Console.WriteLine("TODO: Implement win/draw detection");
        }
        
        /// <summary>
        /// Ask player if they want to play another game
        /// TODO: Simple yes/no prompt with validation
        /// </summary>
        private bool AskPlayAgain()
        {
            // TODO: Ask user if they want to play again
            // TODO: Validate input (y/n, yes/no, etc.)
            // TODO: Return true for play again, false to return to main menu
            
            while (true)
            {
                Console.Write("Do you want to play again? (y/n): ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y" || input == "yes")
                    return true;
                else if (input == "n" || input == "no")
                    return false;
                else
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }
        
        /// <summary>
        /// Switch to the next player's turn
        /// TODO: Toggle between X and O
        /// </summary>
        private void SwitchPlayer()
        {
            // TODO: Switch currentPlayer between 'X' and 'O'            
            Console.WriteLine("TODO: Switch to next player");
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }
        
        // TODO: Add helper methods as needed
        // Examples:
        // - IsValidMove(int row, int col)
        // - IsBoardFull()
        // - CheckRow(int row, char player)
        // - CheckColumn(int col, char player)
        // - CheckDiagonals(char player)
        // - DropToken(int column, char player) // For Connect Four
    }
}