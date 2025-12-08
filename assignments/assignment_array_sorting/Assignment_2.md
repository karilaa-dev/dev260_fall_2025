# Assignment 2: Arrays & Sorting - Implementation Details

## Part A: Board Game Implementation

### Game Choice: Connect Four
- **Board Size:** 6 rows × 7 columns (`char[,] board = new char[6, 7]`)

### How to Play
1. Run `dotnet run` and select option "1. Board Game"
2. Players alternate between X and O tokens
3. Enter column number (1-7) to drop your token
4. Tokens fall to the lowest available position (gravity)
5. First to connect 4 tokens horizontally, vertically, or diagonally wins
6. Choose to play again or return to menu after each game

### Reset Functionality
- Automatic prompt to play again after each game ends
- Board completely reset to empty state for new games
- No application restart required

### 2D Array Usage

**Array Declaration:**
```csharp
char[,] board = new char[6, 7];  // 6 rows, 7 columns
```

**Coordinate System:**
- `board[row, col]` where `row` is 0-5, `col` is 0-6
- Row 0 = top (displayed as row 1), Row 5 = bottom (displayed as row 6)
- Column 0 = left (displayed as column 1), Column 6 = right (displayed as column 7)

**Key Operations:**

**Gravity Token Placement:**
```csharp
for (int row = 5; row >= 0; row--)  // Start from bottom
{
    if (board[row, col - 1] == ' ')
    {
        board[row, col - 1] = currentPlayer;
        break;
    }
}
```

**Win Detection (Horizontal):**
```csharp
for (int row = 0; row < 6; row++)
{
    for (int col = 0; col <= 3; col++)
    {
        if (board[row, col] != ' ' &&
            board[row, col] == board[row, col + 1] &&
            board[row, col] == board[row, col + 2] &&
            board[row, col] == board[row, col + 3])
        {
            return true;  // Win found
        }
    }
}
```

**Board Rendering:**
```csharp
Console.WriteLine("  1 2 3 4 5 6 7");
for (int row = 0; row < 6; row++)
{
    Console.Write($"{row + 1}|");
    for (int col = 0; col < 7; col++)
    {
        Console.Write($"{board[row, col]}|");
    }
    Console.WriteLine();
}
```

## Part B: Book Catalog

### Recursive Sort Implementation: QuickSort
- **Algorithm Choice:** Recursive QuickSort with median-of-three pivot selection
- **Pivot Strategy:** Median of first, middle, and last elements for better partitioning
- **Partitioning:** Two-pointer approach with in-place element swapping

### Title Normalization Rules
1. **Leading Article Removal:** Strips "A ", "AN ", "THE " (case-insensitive)
2. **Whitespace Trimming:** Removes leading/trailing spaces
3. **Case Preservation:** Maintains original capitalization for display
4. **Examples:**
   - "THE Great Gatsby" → "Great Gatsby"
   - "A Tale of Two Cities" → "Tale of Two Cities"
   - "AN American Tragedy" → "American Tragedy"

### 2D Index System (26×26 Letter Pairs)
**Index Structure:**
```csharp
Book[,] letterPairIndex = new Book[26, 26];  // A-Z × A-Z
```

**Indexing Logic:**
- **First Dimension:** First letter of normalized title (0-25 for A-Z)
- **Second Dimension:** Second letter of normalized title (0-25 for A-Z)
- **Storage:** Each cell stores ONE book matching that letter pair
- **Collision Handling:** First book found occupies the cell, others ignored

**Index Population Process:**
1. Iterate through sorted book array
2. Extract first two letters of each normalized title
3. Convert letters to array indices (A=0, B=1, ..., Z=25)
4. Store book in `letterPairIndex[first, second]` if cell is empty

**Fast Lookup Algorithm:**
1. **Index Check:** O(1) lookup in 2D array for exact letter pair match
2. **Binary Search:** O(log n) search within sorted array for prefix matches
3. **Fallback:** Linear scan for edge cases (single-letter titles, etc.)

### Performance Analysis

**Sorting Complexity:**
- **QuickSort Average Case:** O(n log n)
- **QuickSort Worst Case:** O(n²) (rare with median-of-three pivot)
- **Space Complexity:** O(log n) for recursion stack

**Lookup Complexity:**
- **Index Access:** O(1) - Direct array access
- **Binary Search:** O(log n) - Within sorted subset
- **Combined Fast Lookup:** O(1) + O(log n) = O(log n)
- **Fallback Linear Search:** O(n) - Worst case scenario

**Overall Performance:**
- **Index Building:** O(n) - Single pass through sorted array
- **Best Case Lookup:** O(1) - Exact match in index
- **Typical Lookup:** O(log n) - Index + binary search combination
- **Memory Usage:** O(n²) for books + O(1) for 26×26 index

### Suggestion System
- **Exact Match:** Returns immediately if found in index or binary search
- **Nearest 3 Suggestions:** Uses prefix similarity to find closest matches
- **Fallback Behavior:** Shows available books when no matches found