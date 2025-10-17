using System;
using System.Collections.Generic;
using System.IO;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Book Catalog implementation for Assignment 2 Part B
    /// Demonstrates recursive sorting and multi-dimensional indexing for fast lookups
    /// 
    /// Learning Focus:
    /// - Recursive sorting algorithms (QuickSort or MergeSort)
    /// - Multi-dimensional array indexing for performance
    /// - String normalization and binary search
    /// - File I/O and CLI interaction
    /// </summary>
    public class BookCatalog
    {
        #region Data Structures
        
        // Book storage arrays - parallel arrays that stay synchronized
        private string[] originalTitles;    // Original book titles for display
        private string[] normalizedTitles;  // Normalized titles for sorting/searching
        
        // Multi-dimensional index for O(1) lookup by first two letters (A-Z x A-Z = 26x26)
        private int[,] startIndex;  // Starting position for each letter pair in sorted array
        private int[,] endIndex;    // Ending position for each letter pair in sorted array
        
        // Book count tracker
        private int bookCount;
        
        #endregion
        
        /// <summary>
        /// Constructor - Initialize the book catalog
        /// Sets up data structures for book storage and multi-dimensional indexing
        /// </summary>
        public BookCatalog()
        {
            // Initialize arrays (will be resized when books are loaded)
            originalTitles = Array.Empty<string>();
            normalizedTitles = Array.Empty<string>();
            
            // Initialize multi-dimensional index arrays (26x26 for A-Z x A-Z)
            startIndex = new int[26, 26];
            endIndex = new int[26, 26];
            
            // Initialize all index ranges as empty (-1 indicates no books for that letter pair)
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;  // -1 means no books start with this letter pair
                    endIndex[i, j] = -1;    // -1 means no books end with this letter pair
                }
            }
            
            // Reset book count
            bookCount = 0;
            
            Console.WriteLine("BookCatalog initialized - Ready to load books and build index");
        }
        
        /// <summary>
        /// Load books from file and build sorted index
        /// </summary>
        /// <param name="filePath">Path to books.txt file</param>
        public void LoadBooks(string filePath)
        {
            try
            {
                Console.WriteLine($"Loading books from: {filePath}");
                
                // Step 1 - Load books from file
                LoadBooksFromFile(filePath);
                
                // TODO: Step 2 - Sort using recursive algorithm
                SortBooksRecursively();
                
                // TODO: Step 3 - Build multi-dimensional index
                BuildMultiDimensionalIndex();
                
                Console.WriteLine($"Successfully loaded and indexed {bookCount} books.");
                Console.WriteLine("Index built for A-Z x A-Z (26x26) letter pairs.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Start interactive lookup session
        /// TODO: Implement the CLI loop
        /// </summary>
        public void StartLookupSession()
        {
            Console.Clear();
            Console.WriteLine("=== BOOK CATALOG LOOKUP (Part B) ===");
            Console.WriteLine();
            
            // TODO: Check if books are loaded
            if (bookCount == 0)
            {
                Console.WriteLine("No books loaded! Please load a book file first.");
                return;
            }
            
            DisplayLookupInstructions();
            
            // TODO: Implement lookup loop
            bool keepLooking = true;
            
            while (keepLooking)
            {
                Console.WriteLine();
                Console.Write("Enter a book title (or 'exit'): ");
                string? query = Console.ReadLine();
                
                // TODO: Handle exit condition
                if (string.IsNullOrEmpty(query) || query.ToLowerInvariant() == "exit")
                {
                    keepLooking = false;
                    continue;
                }
                
                // TODO: Perform lookup
                PerformLookup(query);
            }
            
            Console.WriteLine("Returning to main menu...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Load book titles from text file
        /// </summary>
        /// <param name="filePath">Path to the books file</param>
        private void LoadBooksFromFile(string filePath)
        {
            // Check if file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Book file not found: {filePath}");
            }
            
            Console.WriteLine($"Reading book titles from: {filePath}");
            
            try
            {
                // Read all lines from file
                string[] lines = File.ReadAllLines(filePath);
                
                // Filter out empty lines and whitespace-only lines
                var validLines = new List<string>();
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (!string.IsNullOrEmpty(trimmedLine))
                    {
                        validLines.Add(trimmedLine);
                    }
                }
                
                // Initialize arrays with the correct size
                bookCount = validLines.Count;
                originalTitles = new string[bookCount];
                normalizedTitles = new string[bookCount];
                
                // Store both original and normalized versions
                for (int i = 0; i < bookCount; i++)
                {
                    originalTitles[i] = validLines[i]; // Keep original formatting for display
                    normalizedTitles[i] = NormalizeTitle(originalTitles[i]); // Normalized for sorting/indexing
                }
                
                Console.WriteLine($"Successfully loaded {bookCount} book titles.");
            }
            catch (IOException ex)
            {
                throw new IOException($"Error reading file '{filePath}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error loading books from '{filePath}': {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Normalize book title for consistent sorting and indexing
        /// </summary>
        /// <param name="title">Original book title</param>
        /// <returns>Normalized title for sorting/indexing</returns>
        private string NormalizeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return "";
            }
            
            // Step 1: Trim whitespace and convert to uppercase
            string normalized = title.Trim().ToUpperInvariant();
            
            // Step 2: Optional - Remove leading articles for better sorting
            // This helps group books by their main title rather than article
            string[] articles = { "THE ", "A ", "AN " };
            
            foreach (string article in articles)
            {
                if (normalized.StartsWith(article))
                {
                    normalized = normalized.Substring(article.Length).Trim();
                    break; // Only remove the first article found
                }
            }
            
            // Step 3: Handle edge case where title was only articles
            if (string.IsNullOrEmpty(normalized))
            {
                return title.Trim().ToUpperInvariant(); // Return original if normalization results in empty
            }
            
            return normalized;
        }
        
        /// <summary>
        /// Sort books using recursive algorithm (QuickSort OR MergeSort)
        /// TODO: Choose ONE recursive sorting algorithm to implement
        /// </summary>
        private void SortBooksRecursively()
        {
            Console.WriteLine("TODO: Implement recursive sorting algorithm");
            Console.WriteLine("Choose ONE to implement:");
            Console.WriteLine("1. QuickSort - Choose pivot strategy and document it");
            Console.WriteLine("2. MergeSort - Implement recursive split/merge");
            Console.WriteLine();
            Console.WriteLine("Requirements:");
            Console.WriteLine("- Must be YOUR recursive implementation");
            Console.WriteLine("- Cannot use Array.Sort() or LINQ");
            Console.WriteLine("- Sort both arrays in parallel (original and normalized)");
            Console.WriteLine("- Document Big-O time/space complexity in README");
            
            // TODO: Call your chosen sorting algorithm
            // Example: QuickSort(normalizedTitles, originalTitles, 0, bookCount - 1);
            // Example: MergeSort(normalizedTitles, originalTitles, 0, bookCount - 1);
        }
        
        /// <summary>
        /// Build multi-dimensional index over sorted data
        /// TODO: Create 26x26 index for first two letters
        /// </summary>
        private void BuildMultiDimensionalIndex()
        {
            Console.WriteLine("TODO: Build multi-dimensional index");
            Console.WriteLine("Requirements:");
            Console.WriteLine("- Create int[,] startIndex and int[,] endIndex arrays (26x26)");
            Console.WriteLine("- Map A-Z to indices 0-25");
            Console.WriteLine("- Handle non-letter starts (map to index 0 or create 27th bucket)");
            Console.WriteLine("- Scan sorted array once to record [start,end) ranges");
            Console.WriteLine("- Empty ranges should have start > end or start = -1");
            
            // TODO: Initialize index arrays
            // TODO: Scan sorted titles and record boundaries for each letter pair
            
            // Example structure:
            // // Scan sorted array and build ranges
            // for (int bookIndex = 0; bookIndex < bookCount; bookIndex++)
            // {
            //     // Get first two letters and update index ranges
            // }
        }
        
        /// <summary>
        /// Perform lookup with exact match and suggestions
        /// TODO: Implement indexed lookup with binary search
        /// </summary>
        /// <param name="query">User's search query</param>
        private void PerformLookup(string query)
        {
            // TODO: Normalize query same way as indexing
            string normalizedQuery = NormalizeTitle(query);
            
            Console.WriteLine($"TODO: Perform lookup for '{query}'");
            Console.WriteLine("Requirements:");
            Console.WriteLine("1. Get first 1-2 letters of normalized query");
            Console.WriteLine("2. Look up [start,end) range from 2D index in O(1)");
            Console.WriteLine("3. If empty range, show suggestions from nearby ranges");
            Console.WriteLine("4. If non-empty range, binary search within slice");
            Console.WriteLine("5. Show exact match or helpful suggestions");
            Console.WriteLine("6. Always display original titles (not normalized)");
            
            // TODO: Extract first two letters for indexing
            // TODO: Get start/end range from 2D index
            // TODO: If range is empty, find suggestions
            // TODO: If range exists, binary search for exact match
            // TODO: Display results using original titles
        }
        
        /// <summary>
        /// Display lookup instructions
        /// TODO: Customize instructions for your implementation
        /// </summary>
        private void DisplayLookupInstructions()
        {
            Console.WriteLine("BOOK LOOKUP INSTRUCTIONS:");
            Console.WriteLine("- Enter any book title to search");
            Console.WriteLine("- Exact matches will be shown if found");
            Console.WriteLine("- Suggestions provided for partial/no matches");
            Console.WriteLine("- Type 'exit' to return to main menu");
            Console.WriteLine();
            Console.WriteLine($"Catalog contains {bookCount} books, sorted and indexed for fast lookup.");
        }
        
        // TODO: Add your sorting algorithm methods
        // Choose ONE to implement:
        
        /// <summary>
        /// QuickSort implementation (Option 1)
        /// TODO: Implement if you choose QuickSort
        /// </summary>
        // private void QuickSort(string[] normalizedArray, string[] originalArray, int low, int high)
        // {
        //     // TODO: Implement recursive QuickSort
        //     // TODO: Choose and document pivot strategy
        //     // TODO: Ensure both arrays stay synchronized
        // }
        
        /// <summary>
        /// MergeSort implementation (Option 2)  
        /// TODO: Implement if you choose MergeSort
        /// </summary>
        // private void MergeSort(string[] normalizedArray, string[] originalArray, int left, int right)
        // {
        //     // TODO: Implement recursive MergeSort
        //     // TODO: Handle O(n) extra space requirement
        //     // TODO: Ensure both arrays stay synchronized
        // }
        
        // TODO: Add helper methods as needed
        // Examples:
        // - GetLetterIndex(char letter) - Convert A-Z to 0-25
        // - BinarySearchInRange(string query, int start, int end)
        // - FindSuggestions(string query, int maxSuggestions)
        // - SwapElements(int index1, int index2) - For QuickSort
        // - MergeArrays(...) - For MergeSort
    }
}