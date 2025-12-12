using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    /// <summary>
    /// Core pantry tracking class that manages inventory, shopping lists, and recipe validation.
    /// 
    /// Data Structures Used:
    /// 1. Dictionary<string, PantryItem> - O(1) lookups for pantry inventory by item name
    /// 2. HashSet<string> - O(1) operations for shopping list with automatic duplicate prevention
    /// 3. List<Recipe> - O(n) iteration for recipe storage and ingredient validation
    /// </summary>
    public class PantryTracker
    {
        // Data Structure #1: Dictionary for O(1) pantry item lookups by name
        private readonly Dictionary<string, PantryItem> pantryInventory;
        
        // Data Structure #2: HashSet for O(1) shopping list operations with duplicate prevention
        private readonly HashSet<string> shoppingList;
        
        // Data Structure #3: List for storing recipes (iteration-based operations)
        private readonly List<Recipe> recipes;

        public PantryTracker()
        {
            // Use case-insensitive comparison for all string keys
            pantryInventory = new Dictionary<string, PantryItem>(StringComparer.OrdinalIgnoreCase);
            shoppingList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            recipes = new List<Recipe>();
            
            // Initialize with predefined recipes
            InitializeRecipes();
        }

        /// <summary>
        /// Gets the number of items in the pantry.
        /// </summary>
        public int PantryCount => pantryInventory.Count;

        /// <summary>
        /// Gets the number of items on the shopping list.
        /// </summary>
        public int ShoppingListCount => shoppingList.Count;

        /// <summary>
        /// Gets the number of available recipes.
        /// </summary>
        public int RecipeCount => recipes.Count;

        /// <summary>
        /// Adds a new item to the pantry inventory.
        /// Time Complexity: O(1) average for Dictionary.Add
        /// </summary>
        public bool AddItem(string name, int quantity, string unit, string category, DateTime? expirationDate = null, int lowStockThreshold = 2)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string normalizedName = NormalizeName(name);
            
            if (pantryInventory.ContainsKey(normalizedName))
                return false; // Item already exists

            var item = new PantryItem(normalizedName, quantity, unit, category, expirationDate, lowStockThreshold);
            pantryInventory.Add(normalizedName, item);
            return true;
        }

        /// <summary>
        /// Gets all items in the pantry, sorted alphabetically.
        /// Time Complexity: O(n log n) for sorting
        /// </summary>
        public List<PantryItem> GetAllItems()
        {
            return pantryInventory.Values
                .OrderBy(item => item.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        /// <summary>
        /// Searches for an item by name.
        /// Time Complexity: O(1) for Dictionary lookup
        /// </summary>
        public PantryItem? SearchItem(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string normalizedName = NormalizeName(name);
            
            if (pantryInventory.TryGetValue(normalizedName, out var item))
                return item;
            
            return null;
        }

        /// <summary>
        /// Checks if an item exists in the pantry.
        /// Time Complexity: O(1) for Dictionary.ContainsKey
        /// </summary>
        public bool HasItem(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;
            
            return pantryInventory.ContainsKey(NormalizeName(name));
        }

        /// <summary>
        /// Updates the quantity of an existing item.
        /// Time Complexity: O(1) for Dictionary lookup
        /// </summary>
        public bool UpdateItemQuantity(string name, int newQuantity)
        {
            if (string.IsNullOrWhiteSpace(name) || newQuantity < 0)
                return false;

            string normalizedName = NormalizeName(name);
            
            if (pantryInventory.TryGetValue(normalizedName, out var item))
            {
                item.Quantity = newQuantity;
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Increments the quantity of an existing item.
        /// Time Complexity: O(1) for Dictionary lookup
        /// </summary>
        public bool IncrementItemQuantity(string name, int amount)
        {
            if (string.IsNullOrWhiteSpace(name) || amount <= 0)
                return false;

            string normalizedName = NormalizeName(name);
            
            if (pantryInventory.TryGetValue(normalizedName, out var item))
            {
                item.Quantity += amount;
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Decrements the quantity of an existing item.
        /// Time Complexity: O(1) for Dictionary lookup
        /// </summary>
        public bool DecrementItemQuantity(string name, int amount)
        {
            if (string.IsNullOrWhiteSpace(name) || amount <= 0)
                return false;

            string normalizedName = NormalizeName(name);
            
            if (pantryInventory.TryGetValue(normalizedName, out var item))
            {
                item.Quantity = Math.Max(0, item.Quantity - amount);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Removes an item from the pantry.
        /// Time Complexity: O(1) for Dictionary.Remove
        /// </summary>
        public bool RemoveItem(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return pantryInventory.Remove(NormalizeName(name));
        }

        /// <summary>
        /// Gets all items that are at or below their low stock threshold.
        /// Time Complexity: O(n) for iteration
        /// </summary>
        public List<PantryItem> GetLowStockItems()
        {
            return pantryInventory.Values
                .Where(item => item.IsLowStock)
                .OrderBy(item => item.Quantity)
                .ThenBy(item => item.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        /// <summary>
        /// Gets all items expiring within the specified number of days.
        /// Time Complexity: O(n) for iteration
        /// </summary>
        public List<PantryItem> GetExpiringSoonItems(int days = 7)
        {
            return pantryInventory.Values
                .Where(item => item.IsExpiringSoon(days))
                .OrderBy(item => item.ExpirationDate)
                .ToList();
        }

        /// <summary>
        /// Adds an item to the shopping list. Prevents duplicates automatically.
        /// Time Complexity: O(1) for HashSet.Add
        /// </summary>
        public bool AddToShoppingList(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                return false;

            string normalizedName = NormalizeName(itemName);
            return shoppingList.Add(normalizedName);
        }

        /// <summary>
        /// Gets all items on the shopping list, sorted alphabetically.
        /// Time Complexity: O(n log n) for sorting
        /// </summary>
        public List<string> GetShoppingList()
        {
            return shoppingList
                .OrderBy(item => item, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        /// <summary>
        /// Checks if an item is on the shopping list.
        /// Time Complexity: O(1) for HashSet.Contains
        /// </summary>
        public bool IsOnShoppingList(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                return false;

            return shoppingList.Contains(NormalizeName(itemName));
        }

        /// <summary>
        /// Removes an item from the shopping list.
        /// Time Complexity: O(1) for HashSet.Remove
        /// </summary>
        public bool RemoveFromShoppingList(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                return false;

            return shoppingList.Remove(NormalizeName(itemName));
        }

        /// <summary>
        /// Clears all items from the shopping list.
        /// Time Complexity: O(n) for HashSet.Clear
        /// </summary>
        public void ClearShoppingList()
        {
            shoppingList.Clear();
        }

        /// <summary>
        /// Gets all available recipes.
        /// Time Complexity: O(n) for list iteration
        /// </summary>
        public List<Recipe> GetAllRecipes()
        {
            return recipes.ToList();
        }

        /// <summary>
        /// Gets a recipe by its index (1-based for user display).
        /// </summary>
        public Recipe? GetRecipeByIndex(int index)
        {
            int zeroBasedIndex = index - 1;
            if (zeroBasedIndex >= 0 && zeroBasedIndex < recipes.Count)
                return recipes[zeroBasedIndex];
            
            return null;
        }

        /// <summary>
        /// Checks which ingredients from a recipe are missing or low in the pantry.
        /// Time Complexity: O(m) where m is the number of ingredients (Dictionary lookups are O(1))
        /// </summary>
        public (List<string> missing, List<string> lowStock, List<string> available) CheckRecipeIngredients(Recipe recipe)
        {
            var missing = new List<string>();
            var lowStock = new List<string>();
            var available = new List<string>();

            foreach (string ingredient in recipe.Ingredients)
            {
                string normalizedIngredient = NormalizeName(ingredient);
                
                if (pantryInventory.TryGetValue(normalizedIngredient, out var item))
                {
                    if (item.Quantity == 0)
                    {
                        missing.Add(ingredient);
                    }
                    else if (item.IsLowStock)
                    {
                        lowStock.Add($"{ingredient} ({item.Quantity} {item.Unit})");
                    }
                    else
                    {
                        available.Add(ingredient);
                    }
                }
                else
                {
                    missing.Add(ingredient);
                }
            }

            return (missing, lowStock, available);
        }

        /// <summary>
        /// Checks if the pantry has all ingredients for a recipe.
        /// </summary>
        public bool CanMakeRecipe(Recipe recipe)
        {
            var (missing, _, _) = CheckRecipeIngredients(recipe);
            return missing.Count == 0;
        }

        /// <summary>
        /// Adds missing recipe ingredients to the shopping list.
        /// </summary>
        public int AddMissingIngredientsToShoppingList(Recipe recipe)
        {
            var (missing, _, _) = CheckRecipeIngredients(recipe);
            int addedCount = 0;
            
            foreach (string ingredient in missing)
            {
                if (AddToShoppingList(ingredient))
                    addedCount++;
            }
            
            return addedCount;
        }

        /// <summary>
        /// Initializes the recipe list with predefined recipes.
        /// </summary>
        private void InitializeRecipes()
        {
            recipes.Add(new Recipe(
                "Scrambled Eggs",
                new List<string> { "eggs", "butter", "milk", "salt", "pepper" },
                "Classic breakfast scrambled eggs"
            ));

            recipes.Add(new Recipe(
                "Pasta with Tomato Sauce",
                new List<string> { "pasta", "tomatoes", "garlic", "olive oil", "basil", "salt" },
                "Simple Italian pasta dish"
            ));

            recipes.Add(new Recipe(
                "Grilled Cheese Sandwich",
                new List<string> { "bread", "cheese", "butter" },
                "Quick and easy comfort food"
            ));

            recipes.Add(new Recipe(
                "Garden Salad",
                new List<string> { "lettuce", "tomatoes", "cucumber", "olive oil", "salt", "pepper" },
                "Fresh and healthy salad"
            ));

            recipes.Add(new Recipe(
                "Pancakes",
                new List<string> { "flour", "eggs", "milk", "sugar", "butter", "baking powder" },
                "Fluffy breakfast pancakes"
            ));
        }

        /// <summary>
        /// Normalizes item names for consistent storage and lookup.
        /// </summary>
        private static string NormalizeName(string name)
        {
            return name.Trim();
        }

        /// <summary>
        /// Gets statistics about the pantry.
        /// </summary>
        public (int totalItems, int lowStockCount, int expiringCount, int expiredCount) GetPantryStats()
        {
            int totalItems = pantryInventory.Count;
            int lowStockCount = pantryInventory.Values.Count(item => item.IsLowStock);
            int expiringCount = pantryInventory.Values.Count(item => item.IsExpiringSoon() && !item.IsExpired);
            int expiredCount = pantryInventory.Values.Count(item => item.IsExpired);
            
            return (totalItems, lowStockCount, expiringCount, expiredCount);
        }
    }
}
