using System;
using System.Collections.Generic;

namespace FinalProject
{
    /// <summary>
    /// Represents a recipe with a name and list of required ingredients.
    /// Used for validating if the pantry has all necessary items to cook a meal.
    /// </summary>
    public class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Creates a new recipe with the specified name and ingredients.
        /// </summary>
        public Recipe(string name, List<string> ingredients, string description = "")
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Ingredients = ingredients ?? new List<string>();
            Description = description ?? "";
        }

        /// <summary>
        /// Returns a formatted string representation of the recipe.
        /// </summary>
        public override string ToString()
        {
            return $"{Name} ({Ingredients.Count} ingredients)";
        }

        /// <summary>
        /// Returns a detailed multi-line display string for the recipe.
        /// </summary>
        public string ToDetailedString()
        {
            string ingredientList = string.Join(", ", Ingredients);
            return $"  Recipe: {Name}\n" +
                   $"  Description: {(string.IsNullOrEmpty(Description) ? "N/A" : Description)}\n" +
                   $"  Ingredients: {ingredientList}";
        }
    }
}
