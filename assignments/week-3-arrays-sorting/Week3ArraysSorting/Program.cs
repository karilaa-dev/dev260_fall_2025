using System;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Main program entry point for Assignment 2
    /// Provides menu system to choose between Board Game and Book Catalog features
    /// 
    /// Learning Focus: Arrays, algorithms, and clean CLI interaction
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Week 3: Arrays & Sorting Assignment ===");
            Console.WriteLine("by [Your Name Here]");
            Console.WriteLine();
            
            bool keepRunning = true;
            
            while (keepRunning)
            {
                // Clear console for clean display
                Console.Clear();
                
                DisplayMainMenu();
                
                // Get user input and validate it
                Console.Write("Enter your choice (1-3): ");
                string? choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        // Call board game functionality
                        PlayBoardGame();
                        break;
                        
                    case "2":
                        // Call book catalog functionality
                        AccessBookCatalog(args);
                        break;
                        
                    case "3":
                        // Exit gracefully
                        keepRunning = false;
                        Console.WriteLine("Thanks for testing my Arrays & Sorting assignment!");
                        break;
                        
                    default:
                        // Handle invalid input
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        /// <summary>
        /// Display the main menu options
        /// </summary>
        static void DisplayMainMenu()
        {
            Console.WriteLine("=== MAIN MENU ===");
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine();
            
            Console.WriteLine("1. Play Board Game (Part A)");
            Console.WriteLine("   → Experience multi-dimensional arrays with [The name of your game!]");
            Console.WriteLine();
            
            Console.WriteLine("2. Book Catalog Lookup (Part B)");
            Console.WriteLine("   → Search through sorted book titles using recursive algorithms");
            Console.WriteLine();
            
            Console.WriteLine("3. Exit Program");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Launch the board game component
        /// </summary>
        static void PlayBoardGame()
        {
            try
            {
                // TODO: Create and start your board game
                // Example structure:
                // BoardGame game = new BoardGame();
                // game.StartGame();
                
                Console.WriteLine("Starting Board Game...");
                Console.WriteLine("TODO: Implement board game integration here");
                
                // Placeholder for now
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting board game: {ex.Message}");
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }
        }
        
        /// <summary>
        /// Launch the book catalog component
        /// </summary>
        static void AccessBookCatalog(string[] args)
        {
            try
            {
                // Using a default text file for books
                string bookFilePath = "books.txt"; // Default
                
                Console.WriteLine("Starting Book Catalog...");
                Console.WriteLine($"TODO: Load books from {bookFilePath}");
                Console.WriteLine("TODO: Implement book catalog integration here");
                
                // TODO: Create and start your book catalog
                BookCatalog catalog = new BookCatalog();
                catalog.LoadBooks(bookFilePath);
                catalog.StartLookupSession();
                
                // Placeholder for now
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing book catalog: {ex.Message}");
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }
        }
    }
}