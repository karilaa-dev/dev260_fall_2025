# Personal Pantry & Grocery Tracker

> A console-based inventory management tool for home cooks that tracks pantry stock, prevents duplicate grocery purchases, and verifies ingredients for meal planning.

---

## What I Built (Overview)

**Problem this solves:**  
_Explain the real-world task your app supports and why it's useful (2–4 sentences)._

**Your Answer:**

Home cooks often struggle with efficiently managing their kitchen inventory. Without a clear tracking system, people frequently purchase ingredients they already have (wasting money and space) or start cooking a meal only to realize they are missing a key ingredient (wasting time). This application solves both problems by providing a centralized pantry tracker with smart shopping lists and recipe validation.

**Core features:**  
_List the main features your application provides (Add, Search, List, Update, Delete, etc.)_

**Your Answer:**

- Pantry Inventory Management (CRUD):

  - Users can add, update (change quantity), and remove items from their virtual pantry.

  - Underlying DS: Dictionary<string, PantryItem> for fast O(1) lookups by name.

- Smart Shopping List:

  - Users can quickly add items to a shopping list. The system automatically rejects duplicates if the item is already on the list.

  - Underlying DS: HashSet<string> to enforce uniqueness.

- Recipe Validator:

  - Users can select a "Recipe" (a predefined list of ingredients) to see if they have all necessary items in stock.

  - Underlying DS: List<Recipe> for storing recipes, iterating over the Dictionary for ingredient checks.

- Search & Status:

  - View all items currently "Low Stock" or search for a specific item to check its expiration date.
  - View items expiring soon with configurable time window.

## How to Run

**Requirements:**  
_List required .NET version, OS requirements, and any dependencies._

**Your Answer:**

- .NET 9.0 SDK
- Works on Windows, macOS, and Linux

```bash
git clone <your-repo-url>
cd final_project
dotnet build
```

**Run:**  
_Provide the command to run your application._

**Your Answer:**

```bash
dotnet run
```

**Sample data (if applicable):**  
_Describe where sample data lives and how to load it (e.g., JSON file path, CSV import)._

**Your Answer:**

The application includes 5 predefined recipes (Scrambled Eggs, Pasta with Tomato Sauce, Grilled Cheese Sandwich, Garden Salad, and Pancakes). You can add pantry items through the interactive menu.

---

## Using the App (Quick Start)

**Typical workflow:**  
_Describe the typical user workflow in 2–4 steps._

**Your Answer:**

1. Add items to your pantry using option 1 (name, quantity, unit, category, expiration date)
2. Check recipe ingredients using option 10 to see what you can cook
3. Missing ingredients are automatically suggested to add to your shopping list
4. View low stock items (option 11) or expiring soon items (option 12) for alerts

**Input tips:**  
_Explain case sensitivity, required fields, and how common errors are handled gracefully._

**Your Answer:**

- Item names are case-insensitive ("Eggs", "eggs", "EGGS" are treated the same)
- Required fields: item name and quantity
- Optional fields: unit (defaults to "pieces"), category (defaults to "General"), expiration date, low stock threshold (defaults to 2)
- Invalid inputs are caught with helpful error messages; the app never crashes on bad input

---

## Data Structures (Brief Summary)

> Full rationale goes in **DESIGN.md**. Here, list only what you used and the feature it powers.

**Data structures used:**  
_List each data structure and briefly explain what feature it powers._

**Your Answer:**

- `Dictionary<string, PantryItem>` → Powers the pantry inventory with O(1) add/search/update/delete operations
- `HashSet<string>` → Powers the smart shopping list with automatic duplicate prevention
- `List<Recipe>` → Stores predefined recipes for ingredient validation

---

## Manual Testing Summary

> No unit tests required. Show how you verified correctness with 3–5 test scenarios.

**Test scenarios:**  
_Describe each test scenario with steps and expected results._

**Your Answer:**

**Scenario 1: Add and Search Item**

- Steps: Add "Eggs" with quantity 12, then search for "eggs" (lowercase)
- Expected result: Item is found with correct details
- Actual result: Works correctly; case-insensitive matching functions properly

**Scenario 2: Duplicate Prevention in Shopping List**

- Steps: Add "Milk" to shopping list, then try to add "milk" again
- Expected result: Second add is rejected with message about duplicate
- Actual result: HashSet correctly prevents duplicate entries

**Scenario 3: Recipe Ingredient Check**

- Steps: Add "bread", "cheese", "butter" to pantry, then check "Grilled Cheese Sandwich" recipe
- Expected result: All ingredients show as available
- Actual result: Recipe validation correctly identifies all ingredients as available

**Scenario 4: Low Stock Alert**

- Steps: Add "Salt" with quantity 1 and threshold 2, view low stock items
- Expected result: Salt appears in low stock list
- Actual result: Low stock detection works correctly

**Scenario 5: Invalid Input Handling**

- Steps: Try to add item with empty name, negative quantity, invalid date format
- Expected result: Graceful error messages, no crashes
- Actual result: All invalid inputs are caught with helpful error messages

---

## Known Limitations

**Limitations and edge cases:**  
_Describe any edge cases not handled, performance caveats, or known issues._

**Your Answer:**

- Data is not persisted between sessions (in-memory only)
- Cannot add custom recipes through the UI (predefined recipes only)
- Partial name matching not supported (must use exact item names)

## Comparers & String Handling

**Keys comparer:**  
_Describe what string comparer you used (e.g., StringComparer.OrdinalIgnoreCase) and why._

**Your Answer:**

Used `StringComparer.OrdinalIgnoreCase` for both Dictionary and HashSet. Basically means "Eggs" and "eggs" are the same thing. Makes the app way less annoying to use.

**Normalization:**  
_Explain how you normalize strings (trim whitespace, consistent casing, duplicate checks)._

**Your Answer:**

All item names are trimmed of leading/trailing whitespace using `name.Trim()`. The case-insensitive comparer handles casing differences automatically without converting to lowercase in storage.

---

## Credits & AI Disclosure

**Resources:**  
_List any articles, documentation, or code snippets you referenced or adapted._

**Your Answer:**

- Microsoft .NET Documentation for Dictionary, HashSet, and List
- Course assignment examples for Navigator pattern and console UI design

- **AI usage (if any):**  
   _Describe what you asked AI tools, what code they influenced, and how you verified correctness._

  **Your Answer:**

  - Gemini to brainstorm ideas I can use for final project.
  - GitHub Copilot autocompletion, which allowed to write more code in the same amount of time, especially writing comments.
  - GitHub Copilot for code review in PR

## Challenges and Solutions

**Biggest challenge faced:**  
_Describe the most difficult part of the project - was it choosing the right data structures, implementing search functionality, handling edge cases, designing the user interface, or understanding a specific algorithm?_

**Your Answer:**

The most effort for me went into planning and implementing the initial code structure.

**How you solved it:**  
_Explain your solution approach and what helped you figure it out - research, consulting documentation, debugging with breakpoints, testing with simple examples, refactoring your design, etc._

**Your Answer:**

Dotnet documentation, google search (to be fair, AI overview usualy had the answer I needed)

**Most confusing concept:**  
_What was hardest to understand about data structures, algorithm complexity, key comparers, normalization, or organizing your code architecture?_

**Your Answer:**

Organizing code was definitely the hardest, because it impacts how easy and straightforward was writing the rest of the code.

## Code Quality

**What you're most proud of in your implementation:**  
_Highlight the best aspect of your code - maybe your data structure choices, clean architecture, efficient algorithms, intuitive user interface, thorough error handling, or elegant solution to a complex problem._

**Your Answer:**

I like how easy it is to read. Maybe I put too much effort into that, but not always you have time to to that. I also like how clean the main menu is, it's really nice looking.

**What you would improve if you had more time:**  
_Identify areas for potential improvement - perhaps adding more features, optimizing performance, improving error handling, adding data persistence, refactoring for better maintainability, or enhancing the user experience._

**Your Answer:**

Implementing persistent storage would definitely be the most important thing. And for the application I made, a GUI would definitely help.

## Real-World Applications

**How this relates to real-world systems:**  
_Describe how your implementation connects to actual software systems - e.g., inventory management, customer databases, e-commerce platforms, social networks, task managers, or other applications in the industry._

**Your Answer:**

Basically any app that tracks stuff uses similar ideas. Grocery stores, warehouses, even restaurant kitchens. The Dictionary is like a mini database with fast lookups, and the HashSet is how databases prevent duplicates.

**What you learned about data structures and algorithms:**  
_What insights did you gain about choosing appropriate data structures, performance tradeoffs, Big-O complexity in practice, the importance of good key design, or how data structures enable specific features?_

**Your Answer:**

Honestly, the biggest thing I learned is that picking the right data structure makes everything easier. Dictionary for fast lookups, HashSet for uniqueness.

---

## STUDY_NOTES

> This section documents my understanding and reflections on the project.

### Why These Data Structures

**Dictionary<string, PantryItem>:**
I chose Dictionary because the primary operation in a pantry app is looking up items by name. When a user wants to check if they have eggs, update the milk quantity, or remove expired bread, they think in terms of item names. Dictionary provides O(1) average lookup time using hashing, making these operations instant.

**HashSet<string>:**
The shopping list needs one key feature: no duplicates. If "milk" is already on the list, adding it again should be rejected. HashSet is the best for this. Its Add() method returns false if the item already exists, and all operations are O(1). This is simpler and faster than using a List and checking Contains() before each add.

**List<Recipe>:**
Recipes are accessed sequentially (displayed as a numbered menu) and by index (user picks a number). List handles both patterns well. Since we have only 5-20 recipes and don't need name-based lookup, the simplicity of List beats the overhead of a Dictionary.

### Complexity Notes (Big-O Summary)

| Operation | Data Structure | Time Complexity | Notes |
|-----------|---------------|-----------------|-------|
| Add item | Dictionary | O(1) average | Hash-based insertion |
| Search item | Dictionary | O(1) average | Direct key lookup |
| Update quantity | Dictionary | O(1) average | Lookup + property set |
| Delete item | Dictionary | O(1) average | Hash-based removal |
| View all items | Dictionary | O(n log n) | Sorting for display |
| Add to shopping list | HashSet | O(1) average | With duplicate check |
| Check recipe | List + Dictionary | O(m) | m = ingredients, each O(1) lookup |
| Low stock scan | Dictionary | O(n) | Must check all items |

### What I'd Do Next

If I had more time:

1. **Save to JSON** - So your pantry doesn't disappear when you close the app
2. **Fuzzy search** - Type "egg" and find "Eggs" or "Egg whites"
3. **Custom recipes** - Let users add their own recipes
4. **Quantities in recipes** - Know if you need "2 eggs" not just "eggs"
5. **Undo button** - Using a Stack to undo mistakes
6. **Filter by category** - See just your dairy or just your vegetables

---

## Submission Checklist

- [x] Public GitHub repository link submitted
- [x] README.md completed (this file)
- [x] DESIGN.md completed
- [x] Source code included and builds successfully
- [ ] (Optional) Slide deck or 5–10 minute demo video link (unlisted)

**Demo Video Link (optional):**
