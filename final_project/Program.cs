using System;

namespace FinalProject
{
    /// <summary>
    /// Main entry point for the Personal Pantry & Grocery Tracker application.
    /// This application demonstrates Dictionary, HashSet, and List data structures
    /// for efficient inventory management and recipe validation.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Personal Pantry & Grocery Tracker ===");
            Console.WriteLine("Track your pantry, manage shopping lists, and validate recipes.\n");

            try
            {
                // Initialize the pantry tracker system
                var pantryTracker = new PantryTracker();
                
                // Start the interactive navigator
                var navigator = new PantryNavigator(pantryTracker);
                navigator.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
