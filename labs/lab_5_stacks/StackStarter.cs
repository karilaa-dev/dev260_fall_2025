using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Stack<T> Essential Operations:
- new Stack<string>()           // Create empty stack
- stack.Push(item)              // Add item to top (LIFO)
- stack.Pop()                   // Remove and return top item
- stack.Peek()                  // Look at top item (don't remove)
- stack.Clear()                 // Remove all items
- stack.Count                   // Get number of items

Safety Rules:
- ALWAYS check stack.Count > 0 before Pop() or Peek()
- Empty stack Pop() throws InvalidOperationException
- Empty stack Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (stack.Count > 0) { ... }
- LIFO order: Last item pushed is first item popped
- Enumeration: foreach gives top-to-bottom order

Helpful icons!:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display out
- ℹ️ Information
- 📊 Stats
- 📝 Write
*/

namespace StackLab
{
    /// <summary>
    /// Student skeleton version - follow along with instructor to build this out!
    /// Uncomment the class name and Main method when ready to use this version.
    /// </summary>
    class Program
    {

        // Step 1 - Declare two stacks for action history and undo functionality
        static Stack<string> actionHistory = new Stack<string>();
        static Stack<string> undoHistory = new Stack<string>();

        // Step 2 - Add a counter for total operations
        static int totalOperations = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Interactive Stack Demo ===");
            Console.WriteLine("Building an action history system with undo/redo\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "push":
                        HandlePush();
                        break;
                    case "2":
                    case "pop":
                        HandlePop();
                        break;
                    case "3":
                    case "peek":
                    case "top":
                        HandlePeek();
                        break;
                    case "4":
                    case "display":
                        HandleDisplay();
                        break;
                    case "5":
                    case "clear":
                        HandleClear();
                        break;
                    case "6":
                    case "undo":
                        HandleUndo();
                        break;
                    case "7":
                    case "redo":
                        HandleRedo();
                        break;
                    case "8":
                    case "stats":
                        ShowStatistics();
                        break;
                    case "9":
                    case "exit":
                        running = false;
                        ShowSessionSummary();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("┌─ Stack Operations Menu ─────────────────────────┐");
            Console.WriteLine("│ 1. Push      │ 2. Pop       │ 3. Peek/Top    │");
            Console.WriteLine("│ 4. Display   │ 5. Clear     │ 6. Undo        │");
            Console.WriteLine("│ 7. Redo      │ 8. Stats     │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            // Step 3 - add stack size and total operations to our display
            Console.WriteLine($"Stack Size: {actionHistory.Count} | Total Operations: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        // Step 4 - Implement HandlePush method
        static void HandlePush()
        {
            Console.Write("Enter action to add: ");
            string input = Console.ReadLine()?.Trim() ?? "";
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Action cannot be empty!\n");
                return;
            }
            
            actionHistory.Push(input);
            undoHistory.Clear();
            totalOperations++;
            Console.WriteLine($"Added: \"{input}\"\n");
        }

        // Step 5 - Implement HandlePop method
        static void HandlePop()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("No actions to remove!\n");
                return;
            }
            
            string popped = actionHistory.Pop();
            undoHistory.Push(popped);
            totalOperations++;
            Console.WriteLine($"Removed: \"{popped}\"");
            
            if (actionHistory.Count > 0)
            {
                Console.WriteLine($"New top action: \"{actionHistory.Peek()}\"");
            }
            else
            {
                Console.WriteLine("Stack is now empty");
            }
            Console.WriteLine();
        }

        // Step 6 - Implement HandlePeek method
        static void HandlePeek()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("No actions to peek at!\n");
                return;
            }
            
            string topAction = actionHistory.Peek();
            Console.WriteLine($"Top action: \"{topAction}\"\n");
        }

        // Step 7 - Implement HandleDisplay method
        static void HandleDisplay()
        {
            Console.WriteLine("Action History (LIFO Order):");
            Console.WriteLine("─────────────────────────────");
            
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("No actions in history\n");
                return;
            }
            
            int position = 1;
            foreach (string action in actionHistory)
            {
                string marker = (position == 1) ? "← TOP" : "";
                Console.WriteLine($"{position,2}. \"{action}\" {marker}");
                position++;
            }
            
            Console.WriteLine($"─────────────────────────────");
            Console.WriteLine($"Total actions: {actionHistory.Count}\n");
        }

        // Step 8 - Implement HandleClear method
        static void HandleClear()
        {
            if (actionHistory.Count == 0 && undoHistory.Count == 0)
            {
                Console.WriteLine("Nothing to clear - stacks are already empty\n");
                return;
            }
            
            int actionCount = actionHistory.Count;
            int undoCount = undoHistory.Count;
            
            actionHistory.Clear();
            undoHistory.Clear();
            totalOperations++;
            
            Console.WriteLine($"Cleared {actionCount} actions and {undoCount} undo items\n");
        }

        // Step 9 - Implement HandleUndo method (Advanced)
        static void HandleUndo()
        {
            if (undoHistory.Count == 0)
            {
                Console.WriteLine("Nothing to undo - no actions to restore\n");
                return;
            }
            
            string restored = undoHistory.Pop();
            actionHistory.Push(restored);
            totalOperations++;
            Console.WriteLine($"Restored: \"{restored}\"\n");
        }

        // Step 10 - Implement HandleRedo method (Advanced)
        static void HandleRedo()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("Nothing to redo - no actions to re-remove\n");
                return;
            }
            
            string redone = actionHistory.Pop();
            undoHistory.Push(redone);
            totalOperations++;
            Console.WriteLine($"Redone: \"{redone}\"\n");
        }

        // Step 11 - Implement ShowStatistics method
        static void ShowStatistics()
        {
            Console.WriteLine("Session Statistics:");
            Console.WriteLine("────────────────────");
            Console.WriteLine($"Current stack size: {actionHistory.Count}");
            Console.WriteLine($"Undo stack size: {undoHistory.Count}");
            Console.WriteLine($"Total operations: {totalOperations}");
            Console.WriteLine($"Stack is empty: {(actionHistory.Count == 0 ? "Yes" : "No")}");
            
            if (actionHistory.Count > 0)
            {
                Console.WriteLine($"Current top action: \"{actionHistory.Peek()}\"");
            }
            else
            {
                Console.WriteLine("Current top action: None");
            }
            Console.WriteLine();
        }

        // Step 12 - Implement ShowSessionSummary method
        static void ShowSessionSummary()
        {
            Console.WriteLine("\nSession Summary");
            Console.WriteLine("==================");
            Console.WriteLine($"Total operations performed: {totalOperations}");
            Console.WriteLine($"Final stack size: {actionHistory.Count}");
            
            if (actionHistory.Count > 0)
            {
                Console.WriteLine("\nRemaining actions:");
                int position = 1;
                foreach (string action in actionHistory)
                {
                    Console.WriteLine($"  {position}. \"{action}\"");
                    position++;
                }
            }
            else
            {
                Console.WriteLine("No actions remaining in stack");
            }
            
            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
