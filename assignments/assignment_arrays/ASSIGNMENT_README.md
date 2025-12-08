# Assignment 2: Arrays & Sorting - Student Skeleton

## 📋 Overview

This is your starting point for Assignment 2! The skeleton provides the basic structure so you can focus on implementing arrays, algorithms, and the core learning objectives.

## 🗂 Files Provided

### Core Implementation Files

- **`Program.cs`** - Main entry point with menu system (mostly complete)
- **`BoardGame.cs`** - Board game skeleton for Part A (needs implementation)
- **`BookCatalog.cs`** - Book catalog skeleton for Part B (needs implementation)

### Supporting Files

- **`Assignment2.csproj`** - .NET project file
- **`books.txt`** - Sample book data for testing

## 🎯 Your Tasks

### Part A: Board Game (40 points)

**File to work on:** `BoardGame.cs`

**What to implement:**

1. **Choose your game:** Tic-Tac-Toe (3×3) OR Connect Four (6×7) or Something of your own
2. **Multi-dimensional array:** `char[,] board`
3. **Game features:**
   - Initialize and render board to console
   - Get and validate player moves
   - Update board state
   - Detect win/draw conditions
   - Allow replay without restarting

**TODO markers to follow:**

- Look for `// TODO:` comments
- Each major method has guidance on what to implement
- Start with `InitializeNewGame()` and `RenderBoard()`

### Part B: Book Catalog (60 points)

**File to work on:** `BookCatalog.cs`

**What to implement:**

1. **File loading:** Read `books.txt` and normalize titles
2. **Recursive sorting:** Choose QuickSort OR MergeSort (your implementation)
3. **2D indexing:** Build `int[,]` index for first two letters (26×26)
4. **Fast lookup:** Use index + binary search for O(1) + O(log k) performance

**TODO markers to follow:**

- Each step has detailed guidance
- Choose your sorting algorithm and document it
- Focus on the multi-dimensional indexing requirement

## 🚀 Getting Started

### 1. Test the Skeleton

```bash
dotnet run
```

You should see the main menu with placeholder messages.

### 2. Choose Your Game (Part A)

Open `BoardGame.cs` and decide:

- **Tic-Tac-Toe:** Simpler logic, good for first attempt
- **Connect Four:** More challenging with gravity mechanics

### 3. Choose Your Sort Algorithm (Part B)

In `BookCatalog.cs`, decide:

- **QuickSort:** Generally faster, document your pivot strategy
- **MergeSort:** Guaranteed O(n log n), uses extra memory

### 4. Follow the TODO Comments

Each file has detailed TODO comments explaining:

- What to implement
- Why it's needed for the assignment
- Example code structure
- Requirements to meet

## 📝 Implementation Strategy

### Recommended Order:

1. **Start with Part A** - get familiar with multi-dimensional arrays
2. **Implement basic board rendering** - see your array in action
3. **Add game logic gradually** - moves, validation, win detection
4. **Move to Part B** - file loading and normalization
5. **Implement your chosen sorting algorithm**
6. **Build the 2D index structure**
7. **Add lookup functionality**

### Testing Tips:

- Test each component as you build it
- Use the provided `books.txt` for initial testing
- Add debug output to see your algorithms working
- Test edge cases (empty input, invalid moves, etc.)

## 🎓 Learning Focus

### Part A Teaches:

- **Multi-dimensional arrays** - `char[,]` declaration and manipulation
- **Console I/O** - rendering grids and getting user input
- **Game state management** - tracking turns, detecting wins
- **Input validation** - handling invalid moves gracefully

### Part B Teaches:

- **Recursive algorithms** - implementing QuickSort or MergeSort from scratch
- **Multi-dimensional indexing** - using `int[,]` for performance optimization
- **String processing** - normalization and comparison
- **Binary search** - finding items in sorted ranges
- **File I/O** - reading and processing text files

## 🔧 Development Tips

### For BoardGame.cs:

- Start with a simple console display
- Test move validation thoroughly
- Keep game logic in small, focused methods
- Use descriptive variable names for clarity

### For BookCatalog.cs:

- Test file loading first (print loaded titles)
- Implement sorting on a small array first
- Verify your 2D index with debug output
- Test lookup with known titles from your file

### For Both:

- Add plenty of console output to see what's happening
- Handle errors gracefully (file not found, invalid input)
- Keep methods small and focused
- Comment your logic, especially algorithmic parts

## 📋 Requirements Checklist

### Part A - Board Game

- [ ] Multi-dimensional array declared and used
- [ ] Clear console rendering of board
- [ ] Input validation for moves
- [ ] Win/draw detection logic
- [ ] Replay functionality
- [ ] Clean code structure and comments

### Part B - Book Catalog

- [ ] File loading with error handling
- [ ] Title normalization implemented
- [ ] Recursive sort algorithm (QuickSort OR MergeSort)
- [ ] 26×26 multi-dimensional index built correctly
- [ ] Fast lookup using index + binary search
- [ ] Helpful suggestions when no exact match
- [ ] CLI loop for interactive searching

## 🎯 Success Criteria

**You'll know you're succeeding when:**

- Your board game is playable and fun
- Your sorting algorithm correctly orders the book list
- Your lookup finds books instantly in large datasets
- Your code is well-commented and easy to follow
- You can explain how your multi-dimensional arrays improve performance

## 🆘 Getting Help

**If you're stuck:**

1. Read the TODO comments carefully - they contain implementation hints
2. Review the assignment outline for requirements clarity
3. Test individual methods in isolation
4. Use console output to debug your algorithms
5. Ask for help in class or office hours

**Common gotchas:**

- Multi-dimensional array syntax: `array[row, col]` not `array[row][col]`
- Keep original and normalized titles synchronized during sorting
- Initialize 2D index arrays properly (handle empty ranges)
- Binary search requires sorted data and proper bounds checking

---

**Good luck! Focus on one TODO at a time, test frequently, and remember that the learning is in the implementation process.**

---

## 📚 Study Guide & Supplemental Resources

_Additional resources to reinforce your understanding and help you succeed on this assignment._

### 🎯 Core Concepts Review

#### Multi-Dimensional Arrays in C#

**Essential Reading:**

- [Microsoft Docs: Multidimensional Arrays](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays) - Official C# documentation
- [C# Arrays Tutorial](https://www.tutorialspoint.com/csharp/csharp_multi_dimensional_arrays.htm) - Clear examples with syntax

**Video Resources:**

- [Multi-Dimensional Arrays Explained](https://www.youtube.com/watch?v=J1aQ9JN4vZY&t=40s) - Visual demonstration of 2D array concepts
- [Working with 2D Arrays in C#](https://www.youtube.com/watch?v=G1kYoPr1Ru8) - Practical examples and common patterns

**Practice Problems:**

- Try implementing simple 2D array exercises (matrix addition, finding max/min in grid)
- Practice array bounds checking and iteration patterns

#### Sorting Algorithms Deep Dive

**QuickSort Resources:**

- [HackerEarth QuickSort](https://www.hackerearth.com/practice/algorithms/sorting/quick-sort/tutorial/) - Interactive visualization of QuickSort algorithm
- [GeeksforGeeks QuickSort](https://www.geeksforgeeks.org/quick-sort/) - Implementation details and complexity analysis
- [Khan Academy: QuickSort](https://www.khanacademy.org/computing/computer-science/algorithms/quick-sort/a/overview-of-quicksort) - Step-by-step explanation

**MergeSort Resources:**

- [HackerEarth MergeSort](https://www.hackerearth.com/practice/algorithms/sorting/merge-sort/tutorial/) - Interactive visualization of MergeSort algorithm
- [GeeksforGeeks MergeSort](https://www.geeksforgeeks.org/merge-sort/) - Implementation and analysis
- [MIT OpenCourseWare: Merge Sort](https://ocw.mit.edu/courses/introduction-to-algorithms/) - Academic perspective

**Video Tutorials:**

- [Sorting Algorithms Explained Visually](https://www.youtube.com/watch?v=RfXt_qHDEPw) - Clear explanations of different sorting approaches
- [Sorting Algorithms Comparison](https://www.youtube.com/watch?v=qvPX_myS-gE) - When to use which algorithm

#### Recursion Mastery

**Understanding Recursion:**

- [Recursion in C# - Step by Step](https://www.geeksforgeeks.org/c-sharp/recursion-in-c-sharp/) - Practical examples
- [Thinking Recursively](https://www.cs.utah.edu/~germain/PPS/Topics/recursion.html) - Conceptual understanding

**Practice Exercises:**

- Start with simple recursive problems (factorial, Fibonacci)
- Work up to recursive array processing
- Practice writing recursive sorting algorithms

### 📖 Data Structures & Search (Part B)

#### File I/O and String Processing

**File Operations:**

- [File and Stream I/O in C#](https://docs.microsoft.com/en-us/dotnet/standard/io/) - Official Microsoft documentation
- [Working with Text Files](https://www.tutorialspoint.com/csharp/csharp_text_files.htm) - Practical examples

**String Manipulation:**

- [C# String Methods](https://docs.microsoft.com/en-us/dotnet/api/system.string) - Complete reference

#### Search Algorithms

**Binary Search:**

- [Binary Search Visualization](https://www.cs.usfca.edu/~galles/visualization/Search.html) - Interactive demonstration
- [Binary Search Implementation](https://www.geeksforgeeks.org/binary-search/) - Code examples and analysis

**Indexing Strategies:**

- [Hash Tables vs 2D Arrays](https://www.geeksforgeeks.org/dsa/comparison-of-an-array-and-hash-table-in-terms-of-storage-structure-and-access-time-complexity/) - When to use which data structure
- [Space-Time Tradeoffs](https://www.geeksforgeeks.org/dsa/time-space-trade-off-in-algorithms/) - Understanding performance implications

### 🧠 Algorithm Analysis

#### Big-O Notation

**Essential Resources:**

- [Big-O Cheat Sheet](https://www.bigocheatsheet.com/) - Quick reference for common algorithms
- [Big-O Notation Explained](https://www.freecodecamp.org/news/big-o-notation/) - Practical understanding
- [Algorithm Complexity Analysis](https://www.khanacademy.org/computing/computer-science/algorithms/asymptotic-notation/a/asymptotic-notation) - Academic foundation

**Video Explanations:**

- [Big-O in 6 Minutes](https://www.youtube.com/watch?v=XMUe3zFhM5c) - Quick overview
- [Algorithm Analysis Deep Dive](https://www.youtube.com/watch?v=Ye6puk0LhwU&list=PL3fg3zQpW0k4TYTBwPFrGkXDJ1Xh4IHyv) - Detailed explanation

---

_Remember: The goal isn't just to complete the assignment, but to deeply understand these fundamental computer science concepts that you'll use throughout your career!_
