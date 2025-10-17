using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        // Book storage arrays - parallel arrays that stay synchronized
        private string[] originalTitles;    // Original book titles for display
        private string[] normalizedTitles;  // Normalized titles for sorting/searching
        
        // Multi-dimensional index for O(1) lookup by first two letters (A-Z x A-Z = 26x26)
        private int[,] startIndex;  // Starting position for each letter pair in sorted array
        private int[,] endIndex;    // Ending position for each letter pair in sorted array
        
        // Book count tracker
        private int bookCount;
        
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
                
                // Step 2 - Sort using recursive algorithm
                SortBooksRecursively();
                
                // Step 3 - Build multi-dimensional index
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
        /// </summary>
        public void StartLookupSession()
        {
            Console.Clear();
            Console.WriteLine("=== BOOK CATALOG LOOKUP (Part B) ===");
            Console.WriteLine();
            
            // Check if books are loaded
            if (bookCount == 0)
            {
                Console.WriteLine("No books loaded! Please load a book file first.");
                return;
            }
            
            DisplayLookupInstructions();
            
            bool keepLooking = true;
            
            while (keepLooking)
            {
                Console.WriteLine();
                Console.Write("Enter a book title (or 'exit'): ");
                string? query = Console.ReadLine();
                
                // Handle exit condition
                if (string.IsNullOrEmpty(query) || query.ToLowerInvariant() == "exit")
                {
                    keepLooking = false;
                    continue;
                }
                
                // Perform lookup
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
        /// Sort books using recursive QuickSort algorithm
        /// Uses median-of-three pivot strategy for better performance
        /// Time Complexity: O(n log n) average, O(n²) worst case
        /// Space Complexity: O(log n) due to recursion stack
        /// </summary>
        private void SortBooksRecursively()
        {
            Console.WriteLine("Sorting books using recursive QuickSort with median-of-three pivot...");
            
            if (bookCount > 1)
            {
                QuickSort(normalizedTitles, originalTitles, 0, bookCount - 1);
                Console.WriteLine("Books sorted successfully using QuickSort algorithm.");
            }
            else
            {
                Console.WriteLine("No sorting needed - 0 or 1 books loaded.");
            }
        }
        
        /// <summary>
        /// Build multi-dimensional index over sorted data
        /// Creates 26x26 index for fast O(1) lookup by first two letters
        /// </summary>
        private void BuildMultiDimensionalIndex()
        {
            Console.WriteLine("Building multi-dimensional index for fast lookups...");
            
            // Initialize all ranges as empty
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;
                    endIndex[i, j] = -1;
                }
            }
            
            // Scan sorted array and build ranges for each letter pair
            for (int bookIndex = 0; bookIndex < bookCount; bookIndex++)
            {
                string title = normalizedTitles[bookIndex];
                if (string.IsNullOrEmpty(title)) continue;
                
                // Get first two letters (or handle edge cases)
                int firstLetter = GetLetterIndex(title[0]);
                int secondLetter = title.Length > 1 ? GetLetterIndex(title[1]) : 0;
                
                // If this is the first book with this letter pair, set start
                if (startIndex[firstLetter, secondLetter] == -1)
                {
                    startIndex[firstLetter, secondLetter] = bookIndex;
                }
                
                // Always update end to current position + 1 (exclusive)
                endIndex[firstLetter, secondLetter] = bookIndex + 1;
            }
            
            Console.WriteLine("Multi-dimensional index built successfully.");
            Console.WriteLine("Index covers A-Z x A-Z letter pairs for O(1) range lookup.");
        }
        
        /// <summary>
        /// Perform lookup with exact match and suggestions
        /// Uses multi-dimensional index for O(1) range lookup and binary search
        /// </summary>
        /// <param name="query">User's search query</param>
        private void PerformLookup(string query)
        {
            string normalizedQuery = NormalizeTitle(query);
            
            if (string.IsNullOrEmpty(normalizedQuery))
            {
                Console.WriteLine("Empty search query. Please enter a valid book title.");
                return;
            }
            
            // Get first two letters for indexing
            int firstLetter = GetLetterIndex(normalizedQuery[0]);
            int secondLetter = normalizedQuery.Length > 1 ? GetLetterIndex(normalizedQuery[1]) : 0;
            
            // Get range from 2D index
            int rangeStart = startIndex[firstLetter, secondLetter];
            int rangeEnd = endIndex[firstLetter, secondLetter];
            
            Console.WriteLine();
            Console.WriteLine($"Searching for: '{query}'");
            
            // Check if range exists
            if (rangeStart == -1 || rangeStart >= rangeEnd)
            {
                Console.WriteLine("No exact match found.");
                Console.WriteLine("Suggestions:");
                ShowSuggestions(normalizedQuery, firstLetter, secondLetter);
            }
            else
            {
                // Binary search within the range
                int foundIndex = BinarySearchInRange(normalizedQuery, rangeStart, rangeEnd);
                
                if (foundIndex != -1)
                {
                    Console.WriteLine($"✓ Found: {originalTitles[foundIndex]}");
                }
                else
                {
                    Console.WriteLine("No exact match found.");
                    Console.WriteLine("Similar titles in this range:");
                    ShowNearbyTitles(normalizedQuery, rangeStart, rangeEnd);
                }
            }
        }
        
        /// <summary>
        /// Display lookup instructions
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
        
        /// <summary>
        /// QuickSort implementation with median-of-three pivot strategy
        /// Recursively sorts both normalized and original arrays in parallel
        /// </summary>
        private void QuickSort(string[] normalizedArray, string[] originalArray, int low, int high)
        {
            if (low < high)
            {
                // Partition the array and get pivot position
                int pivotIndex = Partition(normalizedArray, originalArray, low, high);
                
                // Recursively sort elements before and after partition
                QuickSort(normalizedArray, originalArray, low, pivotIndex - 1);
                QuickSort(normalizedArray, originalArray, pivotIndex + 1, high);
            }
        }
        
        /// <summary>
        /// Partition method for QuickSort using median-of-three pivot selection
        /// </summary>
        private int Partition(string[] normalizedArray, string[] originalArray, int low, int high)
        {
            // Median-of-three pivot selection for better performance
            int mid = low + (high - low) / 2;
            
            // Find median of low, mid, high
            if (string.Compare(normalizedArray[mid], normalizedArray[low], StringComparison.Ordinal) < 0)
                Swap(normalizedArray, originalArray, low, mid);
            if (string.Compare(normalizedArray[high], normalizedArray[low], StringComparison.Ordinal) < 0)
                Swap(normalizedArray, originalArray, low, high);
            if (string.Compare(normalizedArray[high], normalizedArray[mid], StringComparison.Ordinal) < 0)
                Swap(normalizedArray, originalArray, mid, high);
            
            // Use median as pivot
            string pivot = normalizedArray[mid];
            Swap(normalizedArray, originalArray, mid, high); // Move pivot to end
            
            int i = low - 1;
            
            for (int j = low; j < high; j++)
            {
                if (string.Compare(normalizedArray[j], pivot, StringComparison.Ordinal) <= 0)
                {
                    i++;
                    Swap(normalizedArray, originalArray, i, j);
                }
            }
            
            Swap(normalizedArray, originalArray, i + 1, high); // Move pivot to final position
            return i + 1;
        }
        
        /// <summary>
        /// Swap elements in both arrays to keep them synchronized
        /// </summary>
        private void Swap(string[] normalizedArray, string[] originalArray, int i, int j)
        {
            // Swap normalized titles
            string tempNormalized = normalizedArray[i];
            normalizedArray[i] = normalizedArray[j];
            normalizedArray[j] = tempNormalized;
            
            // Swap original titles
            string tempOriginal = originalArray[i];
            originalArray[i] = originalArray[j];
            originalArray[j] = tempOriginal;
        }
        
        /// <summary>
        /// Convert letter character to array index (A-Z -> 0-25)
        /// Non-letters are mapped to index 0
        /// </summary>
        private int GetLetterIndex(char letter)
        {
            if (letter >= 'A' && letter <= 'Z')
                return letter - 'A';
            if (letter >= 'a' && letter <= 'z')
                return letter - 'a';
            return 0; // Non-letters map to A (index 0)
        }
        
        /// <summary>
        /// Binary search within a specific range of the sorted array
        /// </summary>
        private int BinarySearchInRange(string query, int start, int end)
        {
            int left = start;
            int right = end - 1; // end is exclusive
            
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int comparison = string.Compare(normalizedTitles[mid], query, StringComparison.Ordinal);
                
                if (comparison == 0)
                    return mid;
                else if (comparison < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            
            return -1; // Not found
        }
        
        /// <summary>
        /// Show suggestions when no exact match is found
        /// Shows exactly 3 nearest books by prefix similarity
        /// </summary>
        private void ShowSuggestions(string query, int firstLetter, int secondLetter)
        {
            // Find 3 nearest by prefix across entire catalog
            List<string> suggestions = FindNearestByPrefix(query, 3);
            
            // Display exactly 3 suggestions
            if (suggestions.Count == 0)
            {
                Console.WriteLine("  No similar titles found.");
            }
            else
            {
                foreach (string suggestion in suggestions.Take(3))
                {
                    Console.WriteLine($"  • {suggestion}");
                }
            }
        }
        
        /// <summary>
        /// Check if a title is similar to the query by prefix
        /// </summary>
        private bool IsPrefixSimilar(string title, string query)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(query))
                return false;
            
            // Check if title starts with query or query starts with title
            if (title.StartsWith(query, StringComparison.Ordinal) || 
                query.StartsWith(title, StringComparison.Ordinal))
                return true;
            
            // Check if they share the first 2-3 characters
            int minLength = Math.Min(title.Length, query.Length);
            int commonChars = 0;
            
            for (int i = 0; i < Math.Min(minLength, 3); i++)
            {
                if (title[i] == query[i])
                    commonChars++;
            }
            
            return commonChars >= 2; // At least 2 common characters in first 3
        }
        
        /// <summary>
        /// Find nearest books by prefix similarity across entire catalog
        /// </summary>
        private List<string> FindNearestByPrefix(string query, int maxResults)
        {
            List<string> results = new List<string>();
            List<(string title, int similarity)> candidates = new List<(string, int)>();
            
            // Calculate similarity score for each book
            for (int i = 0; i < bookCount; i++)
            {
                int similarity = CalculatePrefixSimilarity(normalizedTitles[i], query);
                if (similarity > 0)
                {
                    candidates.Add((originalTitles[i], similarity));
                }
            }
            
            // Sort by similarity (descending) and take top results
            candidates.Sort((a, b) => b.similarity.CompareTo(a.similarity));
            
            foreach (var candidate in candidates.Take(maxResults))
            {
                results.Add(candidate.title);
            }
            
            // If no similar books found, return first few books as fallback
            if (results.Count == 0 && bookCount > 0)
            {
                for (int i = 0; i < Math.Min(maxResults, bookCount); i++)
                {
                    results.Add(originalTitles[i]);
                }
            }
            
            return results;
        }
        
        /// <summary>
        /// Calculate prefix similarity score between two strings
        /// Higher score means more similar
        /// </summary>
        private int CalculatePrefixSimilarity(string title, string query)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(query))
                return 0;
            
            int score = 0;
            int minLength = Math.Min(title.Length, query.Length);
            
            // Score based on common prefix length
            for (int i = 0; i < minLength; i++)
            {
                if (title[i] == query[i])
                {
                    score += (minLength - i); // Earlier matches get higher score
                }
                else
                {
                    break;
                }
            }
            
            // Bonus for exact prefix match
            if (title.StartsWith(query, StringComparison.Ordinal) || 
                query.StartsWith(title, StringComparison.Ordinal))
            {
                score += 10;
            }
            
            return score;
        }
        
        /// <summary>
        /// Show nearby titles when exact match is not found in range
        /// </summary>
        private void ShowNearbyTitles(string query, int start, int end)
        {
            List<string> nearby = new List<string>();
            
            // Find closest titles lexicographically
            for (int i = start; i < end && nearby.Count < 3; i++)
            {
                if (string.Compare(normalizedTitles[i], query, StringComparison.Ordinal) > 0)
                {
                    nearby.Add(originalTitles[i]);
                }
            }
            
            // If no titles after query, show some from the range
            if (nearby.Count == 0)
            {
                for (int i = start; i < Math.Min(end, start + 3); i++)
                {
                    nearby.Add(originalTitles[i]);
                }
            }
            
            foreach (string title in nearby)
            {
                Console.WriteLine($"  • {title}");
            }
        }
        
        }
}