using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    /// <summary>
    /// Interactive menu navigator for the Personal Pantry & Grocery Tracker application.
    /// Provides a user-friendly console interface for all pantry operations.
    /// </summary>
    public class PantryNavigator
    {
        private readonly PantryTracker pantryTracker;
        private bool isRunning;

        public PantryNavigator(PantryTracker pantryTracker)
        {
            this.pantryTracker = pantryTracker ?? throw new ArgumentNullException(nameof(pantryTracker));
            this.isRunning = true;
        }

        /// <summary>
        /// Main application loop that handles user input and coordinates operations.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Welcome to your Personal Pantry & Grocery Tracker!");
            Console.WriteLine("Manage your kitchen inventory, shopping list, and meal planning.\n");

            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.Trim() ?? "";

                try
                {
                    ProcessCommand(choice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                if (isRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void DisplayMainMenu()
        {
            var stats = pantryTracker.GetPantryStats();
            
            Console.WriteLine("┌─────────────────────────────────────────────────────┐");
            Console.WriteLine("│       Personal Pantry & Grocery Tracker             │");
            Console.WriteLine("├─────────────────────────────────────────────────────┤");
            Console.WriteLine("│  --- Pantry Management ---                          │");
            Console.WriteLine("│   1. Add Item              2. View All Items        │");
            Console.WriteLine("│   3. Update Quantity       4. Remove Item           │");
            Console.WriteLine("│   5. Search Item                                    │");
            Console.WriteLine("│  --- Shopping List ---                              │");
            Console.WriteLine("│   6. Add to Shopping List  7. View Shopping List    │");
            Console.WriteLine("│   8. Remove from List      9. Clear Shopping List   │");
            Console.WriteLine("│  --- Recipes ---                                    │");
            Console.WriteLine("│  10. Check Recipe Ingredients                       │");
            Console.WriteLine("│  --- Status & Reports ---                           │");
            Console.WriteLine("│  11. View Low Stock Items  12. View Expiring Soon   │");
            Console.WriteLine("│  13. Pantry Statistics                              │");
            Console.WriteLine("│   0. Exit                                           │");
            Console.WriteLine("└─────────────────────────────────────────────────────┘");
            Console.WriteLine($"Pantry: {stats.totalItems} items | Shopping List: {pantryTracker.ShoppingListCount} items");
            
            if (stats.lowStockCount > 0 || stats.expiringCount > 0)
            {
                Console.WriteLine($"Alerts: {stats.lowStockCount} low stock, {stats.expiringCount} expiring soon");
            }
            
            Console.Write("\nEnter your choice: ");
        }

        private void ProcessCommand(string input)
        {
            switch (input)
            {
                case "1":
                    HandleAddItem();
                    break;
                case "2":
                    HandleViewAllItems();
                    break;
                case "3":
                    HandleUpdateQuantity();
                    break;
                case "4":
                    HandleRemoveItem();
                    break;
                case "5":
                    HandleSearchItem();
                    break;
                case "6":
                    HandleAddToShoppingList();
                    break;
                case "7":
                    HandleViewShoppingList();
                    break;
                case "8":
                    HandleRemoveFromShoppingList();
                    break;
                case "9":
                    HandleClearShoppingList();
                    break;
                case "10":
                    HandleCheckRecipe();
                    break;
                case "11":
                    HandleViewLowStock();
                    break;
                case "12":
                    HandleViewExpiringSoon();
                    break;
                case "13":
                    HandleViewStatistics();
                    break;
                case "0":
                case "exit":
                case "quit":
                    isRunning = false;
                    ShowGoodbye();
                    break;
                default:
                    Console.WriteLine($"\nInvalid choice: '{input}'. Please enter a number 0-13.");
                    break;
            }
        }

        private void HandleAddItem()
        {
            Console.WriteLine("\n=== Add New Pantry Item ===\n");

            // Get item name
            Console.Write("Item name: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Item name cannot be empty.");
                return;
            }

            if (pantryTracker.HasItem(name))
            {
                Console.WriteLine($"Error: '{name}' already exists in the pantry. Use Update to change quantity.");
                return;
            }

            // Get quantity
            Console.Write("Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine("Error: Please enter a valid non-negative number for quantity.");
                return;
            }

            // Get unit
            Console.Write("Unit (e.g., pieces, oz, cups, lbs) [default: pieces]: ");
            string unit = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(unit))
                unit = "pieces";

            // Get category
            Console.Write("Category (e.g., Dairy, Produce, Meat, Grains) [default: General]: ");
            string category = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(category))
                category = "General";

            // Get expiration date (optional)
            Console.Write("Expiration date (MM/DD/YYYY) [press Enter to skip]: ");
            string expInput = Console.ReadLine()?.Trim() ?? "";
            DateTime? expirationDate = null;
            if (!string.IsNullOrWhiteSpace(expInput))
            {
                if (DateTime.TryParse(expInput, out DateTime expDate))
                {
                    expirationDate = expDate;
                }
                else
                {
                    Console.WriteLine("Warning: Invalid date format. Skipping expiration date.");
                }
            }

            // Get low stock threshold
            Console.Write("Low stock threshold [default: 2]: ");
            string thresholdInput = Console.ReadLine()?.Trim() ?? "";
            int threshold = 2;
            if (!string.IsNullOrWhiteSpace(thresholdInput))
            {
                if (!int.TryParse(thresholdInput, out threshold) || threshold < 0)
                {
                    Console.WriteLine("Warning: Invalid threshold. Using default value of 2.");
                    threshold = 2;
                }
            }

            // Add the item
            if (pantryTracker.AddItem(name, quantity, unit, category, expirationDate, threshold))
            {
                Console.WriteLine($"\nSuccess! '{name}' has been added to your pantry.");
                var item = pantryTracker.SearchItem(name);
                if (item != null)
                {
                    Console.WriteLine(item.ToDetailedString());
                }
            }
            else
            {
                Console.WriteLine("\nError: Failed to add item to the pantry.");
            }
        }

        private void HandleViewAllItems()
        {
            Console.WriteLine("\n=== Pantry Inventory ===\n");

            var items = pantryTracker.GetAllItems();

            if (items.Count == 0)
            {
                Console.WriteLine("Your pantry is empty. Add some items to get started!");
                return;
            }

            Console.WriteLine($"Total items: {items.Count}\n");
            Console.WriteLine(new string('-', 70));
            Console.WriteLine($"{"Name",-20} {"Qty",-8} {"Unit",-10} {"Category",-12} {"Expires",-12} {"Status",-8}");
            Console.WriteLine(new string('-', 70));

            foreach (var item in items)
            {
                string expDate = item.ExpirationDate.HasValue 
                    ? item.ExpirationDate.Value.ToString("MM/dd/yyyy") 
                    : "N/A";
                
                string status = "";
                if (item.IsExpired) status = "EXPIRED";
                else if (item.IsLowStock) status = "LOW";
                else status = "OK";

                Console.WriteLine($"{item.Name,-20} {item.Quantity,-8} {item.Unit,-10} {item.Category,-12} {expDate,-12} {status,-8}");
            }
            
            Console.WriteLine(new string('-', 70));
        }

        private void HandleUpdateQuantity()
        {
            Console.WriteLine("\n=== Update Item Quantity ===\n");

            Console.Write("Item name: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Item name cannot be empty.");
                return;
            }

            var item = pantryTracker.SearchItem(name);
            if (item == null)
            {
                Console.WriteLine($"Error: '{name}' not found in the pantry.");
                return;
            }

            Console.WriteLine($"\nCurrent: {item.Name} - {item.Quantity} {item.Unit}");
            Console.WriteLine("\nOptions:");
            Console.WriteLine("  1. Set new quantity");
            Console.WriteLine("  2. Add to quantity (+)");
            Console.WriteLine("  3. Subtract from quantity (-)");
            Console.Write("\nChoose option (1-3): ");

            string option = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter amount: ");
            if (!int.TryParse(Console.ReadLine(), out int amount) || amount < 0)
            {
                Console.WriteLine("Error: Please enter a valid non-negative number.");
                return;
            }

            bool success = option switch
            {
                "1" => pantryTracker.UpdateItemQuantity(name, amount),
                "2" => pantryTracker.IncrementItemQuantity(name, amount),
                "3" => pantryTracker.DecrementItemQuantity(name, amount),
                _ => false
            };

            if (success)
            {
                var updated = pantryTracker.SearchItem(name);
                Console.WriteLine($"\nSuccess! Updated: {updated?.Name} - {updated?.Quantity} {updated?.Unit}");
            }
            else
            {
                Console.WriteLine("\nError: Failed to update quantity.");
            }
        }

        private void HandleRemoveItem()
        {
            Console.WriteLine("\n=== Remove Item from Pantry ===\n");

            Console.Write("Item name to remove: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Item name cannot be empty.");
                return;
            }

            var item = pantryTracker.SearchItem(name);
            if (item == null)
            {
                Console.WriteLine($"Error: '{name}' not found in the pantry.");
                return;
            }

            Console.WriteLine($"\nItem to remove: {item}");
            Console.Write("Are you sure? (y/n): ");
            string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (confirm == "y" || confirm == "yes")
            {
                if (pantryTracker.RemoveItem(name))
                {
                    Console.WriteLine($"\nSuccess! '{name}' has been removed from your pantry.");
                }
                else
                {
                    Console.WriteLine("\nError: Failed to remove item.");
                }
            }
            else
            {
                Console.WriteLine("\nCancelled. Item was not removed.");
            }
        }

        private void HandleSearchItem()
        {
            Console.WriteLine("\n=== Search Pantry ===\n");

            Console.Write("Enter item name to search: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Search term cannot be empty.");
                return;
            }

            var item = pantryTracker.SearchItem(name);
            if (item != null)
            {
                Console.WriteLine("\nItem found!\n");
                Console.WriteLine(item.ToDetailedString());
            }
            else
            {
                Console.WriteLine($"\nNo item named '{name}' found in your pantry.");
                
                // Suggest adding to shopping list
                Console.Write("Would you like to add it to your shopping list? (y/n): ");
                string addToList = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (addToList == "y" || addToList == "yes")
                {
                    if (pantryTracker.AddToShoppingList(name))
                    {
                        Console.WriteLine($"'{name}' added to shopping list.");
                    }
                    else
                    {
                        Console.WriteLine($"'{name}' is already on your shopping list.");
                    }
                }
            }
        }

        private void HandleAddToShoppingList()
        {
            Console.WriteLine("\n=== Add to Shopping List ===\n");

            Console.Write("Item to add: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Item name cannot be empty.");
                return;
            }

            if (pantryTracker.AddToShoppingList(name))
            {
                Console.WriteLine($"\nSuccess! '{name}' added to your shopping list.");
            }
            else
            {
                Console.WriteLine($"\n'{name}' is already on your shopping list (duplicates prevented).");
            }
        }

        private void HandleViewShoppingList()
        {
            Console.WriteLine("\n=== Shopping List ===\n");

            var items = pantryTracker.GetShoppingList();

            if (items.Count == 0)
            {
                Console.WriteLine("Your shopping list is empty.");
                return;
            }

            Console.WriteLine($"Items to buy: {items.Count}\n");
            int index = 1;
            foreach (var item in items)
            {
                string inPantry = pantryTracker.HasItem(item) ? " (already in pantry)" : "";
                Console.WriteLine($"  {index}. {item}{inPantry}");
                index++;
            }
        }

        private void HandleRemoveFromShoppingList()
        {
            Console.WriteLine("\n=== Remove from Shopping List ===\n");

            var items = pantryTracker.GetShoppingList();
            if (items.Count == 0)
            {
                Console.WriteLine("Your shopping list is empty.");
                return;
            }

            Console.WriteLine("Current shopping list:");
            int index = 1;
            foreach (var item in items)
            {
                Console.WriteLine($"  {index}. {item}");
                index++;
            }

            Console.Write("\nEnter item name to remove: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Item name cannot be empty.");
                return;
            }

            if (pantryTracker.RemoveFromShoppingList(name))
            {
                Console.WriteLine($"\nSuccess! '{name}' removed from your shopping list.");
            }
            else
            {
                Console.WriteLine($"\nError: '{name}' not found on your shopping list.");
            }
        }

        private void HandleClearShoppingList()
        {
            Console.WriteLine("\n=== Clear Shopping List ===\n");

            if (pantryTracker.ShoppingListCount == 0)
            {
                Console.WriteLine("Your shopping list is already empty.");
                return;
            }

            Console.WriteLine($"This will remove all {pantryTracker.ShoppingListCount} items from your shopping list.");
            Console.Write("Are you sure? (y/n): ");
            string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (confirm == "y" || confirm == "yes")
            {
                pantryTracker.ClearShoppingList();
                Console.WriteLine("\nSuccess! Shopping list has been cleared.");
            }
            else
            {
                Console.WriteLine("\nCancelled. Shopping list was not cleared.");
            }
        }

        private void HandleCheckRecipe()
        {
            Console.WriteLine("\n=== Check Recipe Ingredients ===\n");

            var recipes = pantryTracker.GetAllRecipes();

            Console.WriteLine("Available recipes:\n");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {recipes[i].Name} - {recipes[i].Description}");
            }

            Console.Write("\nSelect recipe number: ");
            if (!int.TryParse(Console.ReadLine(), out int recipeNum) || recipeNum < 1 || recipeNum > recipes.Count)
            {
                Console.WriteLine("Error: Please enter a valid recipe number.");
                return;
            }

            var recipe = pantryTracker.GetRecipeByIndex(recipeNum);
            if (recipe == null)
            {
                Console.WriteLine("Error: Recipe not found.");
                return;
            }

            Console.WriteLine($"\n--- {recipe.Name} ---");
            Console.WriteLine($"Ingredients needed: {string.Join(", ", recipe.Ingredients)}\n");

            var (missing, lowStock, available) = pantryTracker.CheckRecipeIngredients(recipe);

            Console.WriteLine("Ingredient Check Results:");
            Console.WriteLine(new string('-', 40));

            if (available.Count > 0)
            {
                Console.WriteLine($"\nAvailable ({available.Count}):");
                foreach (var item in available)
                {
                    Console.WriteLine($"  [OK] {item}");
                }
            }

            if (lowStock.Count > 0)
            {
                Console.WriteLine($"\nLow Stock ({lowStock.Count}):");
                foreach (var item in lowStock)
                {
                    Console.WriteLine($"  [!] {item}");
                }
            }

            if (missing.Count > 0)
            {
                Console.WriteLine($"\nMissing ({missing.Count}):");
                foreach (var item in missing)
                {
                    Console.WriteLine($"  [X] {item}");
                }

                Console.Write("\nAdd missing ingredients to shopping list? (y/n): ");
                string addToList = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (addToList == "y" || addToList == "yes")
                {
                    int added = pantryTracker.AddMissingIngredientsToShoppingList(recipe);
                    Console.WriteLine($"\nAdded {added} item(s) to your shopping list.");
                }
            }
            else
            {
                Console.WriteLine($"\nYou have all ingredients to make {recipe.Name}!");
            }
        }

        private void HandleViewLowStock()
        {
            Console.WriteLine("\n=== Low Stock Items ===\n");

            var items = pantryTracker.GetLowStockItems();

            if (items.Count == 0)
            {
                Console.WriteLine("No items are currently low in stock. Your pantry is well-stocked!");
                return;
            }

            Console.WriteLine($"Items at or below low stock threshold: {items.Count}\n");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Name",-20} {"Quantity",-12} {"Threshold",-12}");
            Console.WriteLine(new string('-', 50));

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Name,-20} {item.Quantity} {item.Unit,-8} {item.LowStockThreshold,-12}");
            }

            Console.WriteLine(new string('-', 50));

            Console.Write("\nAdd all low stock items to shopping list? (y/n): ");
            string addToList = Console.ReadLine()?.Trim().ToLower() ?? "";
            if (addToList == "y" || addToList == "yes")
            {
                int added = 0;
                foreach (var item in items)
                {
                    if (pantryTracker.AddToShoppingList(item.Name))
                        added++;
                }
                Console.WriteLine($"\nAdded {added} item(s) to your shopping list.");
            }
        }

        private void HandleViewExpiringSoon()
        {
            Console.WriteLine("\n=== Items Expiring Soon ===\n");

            Console.Write("Show items expiring within how many days? [default: 7]: ");
            string daysInput = Console.ReadLine()?.Trim() ?? "";
            int days = 7;
            if (!string.IsNullOrWhiteSpace(daysInput))
            {
                if (!int.TryParse(daysInput, out days) || days < 0)
                {
                    Console.WriteLine("Warning: Invalid number. Using default of 7 days.");
                    days = 7;
                }
            }

            var items = pantryTracker.GetExpiringSoonItems(days);

            if (items.Count == 0)
            {
                Console.WriteLine($"No items are expiring within the next {days} days.");
                return;
            }

            Console.WriteLine($"\nItems expiring within {days} days: {items.Count}\n");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Name",-20} {"Quantity",-12} {"Expires",-15} {"Status",-10}");
            Console.WriteLine(new string('-', 60));

            foreach (var item in items)
            {
                string expDate = item.ExpirationDate?.ToString("MM/dd/yyyy") ?? "N/A";
                string status = item.IsExpired ? "EXPIRED!" : "Expiring";
                Console.WriteLine($"{item.Name,-20} {item.Quantity} {item.Unit,-8} {expDate,-15} {status,-10}");
            }

            Console.WriteLine(new string('-', 60));
        }

        private void HandleViewStatistics()
        {
            Console.WriteLine("\n=== Pantry Statistics ===\n");

            var stats = pantryTracker.GetPantryStats();

            Console.WriteLine("Inventory Summary:");
            Console.WriteLine($"  Total items in pantry: {stats.totalItems}");
            Console.WriteLine($"  Low stock items: {stats.lowStockCount}");
            Console.WriteLine($"  Expiring soon: {stats.expiringCount}");
            Console.WriteLine($"  Expired items: {stats.expiredCount}");

            Console.WriteLine($"\nShopping List: {pantryTracker.ShoppingListCount} items");
            Console.WriteLine($"Available Recipes: {pantryTracker.RecipeCount}");

            Console.WriteLine("\nData Structure Performance:");
            Console.WriteLine("  Pantry Inventory (Dictionary<string, PantryItem>):");
            Console.WriteLine("    - Add/Search/Update/Delete: O(1) average");
            Console.WriteLine("  Shopping List (HashSet<string>):");
            Console.WriteLine("    - Add/Contains/Remove: O(1) average");
            Console.WriteLine("    - Duplicate prevention: automatic");
            Console.WriteLine("  Recipes (List<Recipe>):");
            Console.WriteLine("    - Iteration for ingredient check: O(n)");
        }

        private void ShowGoodbye()
        {
            Console.WriteLine("\n=== Thank you for using Personal Pantry & Grocery Tracker! ===\n");

            var stats = pantryTracker.GetPantryStats();
            Console.WriteLine("Session Summary:");
            Console.WriteLine($"  Pantry items: {stats.totalItems}");
            Console.WriteLine($"  Shopping list: {pantryTracker.ShoppingListCount} items");

            if (stats.lowStockCount > 0)
            {
                Console.WriteLine($"\nReminder: You have {stats.lowStockCount} low stock item(s)!");
            }
            if (stats.expiringCount > 0)
            {
                Console.WriteLine($"Reminder: You have {stats.expiringCount} item(s) expiring soon!");
            }

            Console.WriteLine("\nHappy cooking! Goodbye!");
        }
    }
}
