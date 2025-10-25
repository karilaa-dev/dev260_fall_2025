using System;
using System.Collections.Generic;
using System.Diagnostics;
using Week4DoublyLinkedLists.Core;

namespace Week4DoublyLinkedLists.Applications
{
    /// <summary>
    /// Performance analyzer comparing doubly linked lists with other data structures
    /// Demonstrates the trade-offs and use cases for different data structures
    /// </summary>
    public class PerformanceAnalyzer
    {
        private const int SMALL_SIZE = 1000;
        private const int MEDIUM_SIZE = 10000;
        private const int LARGE_SIZE = 100000;
        
        /// <summary>
        /// Run comprehensive performance comparison
        /// </summary>
        public void RunComparison()
        {
            Console.WriteLine("=== PERFORMANCE COMPARISON ===");
            Console.WriteLine();
            Console.WriteLine("Comparing Doubly Linked Lists with Arrays and Lists");
            Console.WriteLine("This will help you understand when to use each data structure.");
            Console.WriteLine();
            
            bool keepRunning = true;
            
            while (keepRunning)
            {
                DisplayComparisonMenu();
                
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();
                
                switch (choice.ToLower())
                {
                    case "1":
                        CompareInsertionPerformance();
                        break;
                    case "2":
                        CompareDeletionPerformance();
                        break;
                    case "3":
                        CompareSearchPerformance();
                        break;
                    case "4":
                        CompareMemoryUsage();
                        break;
                    case "5":
                        CompareTraversalPerformance();
                        break;
                    case "6":
                        RunCompleteAnalysis();
                        break;
                    case "7":
                        ShowBigOComparison();
                        break;
                    case "8":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                
                if (keepRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                }
            }
        }
        
        /// <summary>
        /// Display the comparison menu
        /// </summary>
        private void DisplayComparisonMenu()
        {
            Console.WriteLine("Choose a performance test:");
            Console.WriteLine("1. Insertion Performance");
            Console.WriteLine("2. Deletion Performance");
            Console.WriteLine("3. Search Performance");
            Console.WriteLine("4. Memory Usage Analysis");
            Console.WriteLine("5. Traversal Performance");
            Console.WriteLine("6. Complete Analysis (All Tests)");
            Console.WriteLine("7. Big-O Complexity Comparison");
            Console.WriteLine("8. Return to Main Menu");
            Console.WriteLine();
            Console.Write("Enter your choice (1-8): ");
        }
        
        /// <summary>
        /// Compare insertion performance across data structures
        /// </summary>
        private void CompareInsertionPerformance()
        {
            Console.WriteLine("=== INSERTION PERFORMANCE TEST ===");
            Console.WriteLine();
            
            int[] sizes = { SMALL_SIZE, MEDIUM_SIZE, LARGE_SIZE };
            
            foreach (int size in sizes)
            {
                Console.WriteLine($"Testing with {size:N0} elements:");
                Console.WriteLine();
                
                // Test insertions at different positions
                TestInsertionAtBeginning(size);
                TestInsertionAtEnd(size);
                TestInsertionAtMiddle(size);
                
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Test insertion at beginning of data structures
        /// </summary>
        /// <param name="size">Number of elements to insert</param>
        private void TestInsertionAtBeginning(int size)
        {
            Console.WriteLine("Insertion at Beginning:");
            
            // Doubly Linked List
            var sw = Stopwatch.StartNew();
            var dll = new DoublyLinkedList<int>();
            for (int i = 0; i < size; i++)
            {
                dll.AddFirst(i);
            }
            sw.Stop();
            Console.WriteLine($"  Doubly Linked List: {sw.ElapsedMilliseconds}ms");
            
            // List<T> (ArrayList equivalent)
            sw.Restart();
            var list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                list.Insert(0, i);
            }
            sw.Stop();
            Console.WriteLine($"  List<T> (Insert at 0): {sw.ElapsedMilliseconds}ms");
            
            // Array simulation (worst case - need to shift all elements)
            sw.Restart();
            var array = new int[size];
            for (int i = 0; i < size; i++)
            {
                // Simulate array insertion by copying elements
                for (int j = i; j > 0; j--)
                {
                    array[j] = array[j - 1];
                }
                array[0] = i;
            }
            sw.Stop();
            Console.WriteLine($"  Array (with shifting): {sw.ElapsedMilliseconds}ms");
        }
        
        /// <summary>
        /// Test insertion at end of data structures
        /// </summary>
        /// <param name="size">Number of elements to insert</param>
        private void TestInsertionAtEnd(int size)
        {
            Console.WriteLine("Insertion at End:");
            
            // Doubly Linked List
            var sw = Stopwatch.StartNew();
            var dll = new DoublyLinkedList<int>();
            for (int i = 0; i < size; i++)
            {
                dll.AddLast(i);
            }
            sw.Stop();
            Console.WriteLine($"  Doubly Linked List: {sw.ElapsedMilliseconds}ms");
            
            // List<T>
            sw.Restart();
            var list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                list.Add(i);
            }
            sw.Stop();
            Console.WriteLine($"  List<T> (Add): {sw.ElapsedMilliseconds}ms");
            
            // Array (if we knew the size beforehand)
            sw.Restart();
            var array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i;
            }
            sw.Stop();
            Console.WriteLine($"  Array (direct access): {sw.ElapsedMilliseconds}ms");
        }
        
        /// <summary>
        /// Test insertion at middle of data structures
        /// </summary>
        /// <param name="size">Number of elements to insert</param>
        private void TestInsertionAtMiddle(int size)
        {
            Console.WriteLine("Insertion at Middle:");
            
            // Prepare data structures with some initial data
            var dll = new DoublyLinkedList<int>();
            var list = new List<int>();
            for (int i = 0; i < size / 2; i++)
            {
                dll.Add(i);
                list.Add(i);
            }
            
            // Test middle insertions
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100; i++) // Fewer iterations due to O(n) cost
            {
                dll.Insert(dll.Count / 2, i);
            }
            sw.Stop();
            Console.WriteLine($"  Doubly Linked List: {sw.ElapsedMilliseconds}ms (100 insertions)");
            
            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                list.Insert(list.Count / 2, i);
            }
            sw.Stop();
            Console.WriteLine($"  List<T> (Insert middle): {sw.ElapsedMilliseconds}ms (100 insertions)");
        }
        
        /// <summary>
        /// Compare deletion performance
        /// </summary>
        private void CompareDeletionPerformance()
        {
            Console.WriteLine("=== DELETION PERFORMANCE TEST ===");
            Console.WriteLine();
            
            int size = MEDIUM_SIZE;
            Console.WriteLine($"Testing deletion with {size:N0} elements:");
            Console.WriteLine();
            
            // Prepare data structures
            var dll = new DoublyLinkedList<int>();
            var list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                dll.Add(i);
                list.Add(i);
            }
            
            // Test deletion from beginning
            Console.WriteLine("Deletion from Beginning (100 deletions):");
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100; i++)
            {
                dll.RemoveFirst();
            }
            sw.Stop();
            Console.WriteLine($"  Doubly Linked List: {sw.ElapsedMilliseconds}ms");
            
            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                list.RemoveAt(0);
            }
            sw.Stop();
            Console.WriteLine($"  List<T> (RemoveAt(0)): {sw.ElapsedMilliseconds}ms");
            
            // Test deletion from end
            Console.WriteLine("Deletion from End (100 deletions):");
            
            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                dll.RemoveLast();
            }
            sw.Stop();
            Console.WriteLine($"  Doubly Linked List: {sw.ElapsedMilliseconds}ms");
            
            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                list.RemoveAt(list.Count - 1);
            }
            sw.Stop();
            Console.WriteLine($"  List<T> (RemoveAt(last)): {sw.ElapsedMilliseconds}ms");
        }
        
        /// <summary>
        /// Compare search performance
        /// </summary>
        private void CompareSearchPerformance()
        {
            Console.WriteLine("=== SEARCH PERFORMANCE TEST ===");
            Console.WriteLine();
            
            int size = MEDIUM_SIZE;
            Console.WriteLine($"Testing search with {size:N0} elements:");
            Console.WriteLine();
            
            // Prepare data structures
            var dll = new DoublyLinkedList<int>();
            var list = new List<int>();
            var array = new int[size];
            
            for (int i = 0; i < size; i++)
            {
                dll.Add(i);
                list.Add(i);
                array[i] = i;
            }
            
            // Test searching for elements at different positions
            int[] searchTargets = { 100, size / 4, size / 2, size * 3 / 4, size - 100 };
            
            foreach (int target in searchTargets)
            {
                if (target >= size) continue;
                
                Console.WriteLine($"Searching for element at position {target}:");
                
                // Doubly Linked List
                var sw = Stopwatch.StartNew();
                bool found = dll.Contains(target);
                sw.Stop();
                Console.WriteLine($"  Doubly Linked List: {sw.ElapsedTicks} ticks");
                
                // List<T>
                sw.Restart();
                found = list.Contains(target);
                sw.Stop();
                Console.WriteLine($"  List<T> (Contains): {sw.ElapsedTicks} ticks");
                
                // Array with linear search
                sw.Restart();
                found = Array.IndexOf(array, target) >= 0;
                sw.Stop();
                Console.WriteLine($"  Array (IndexOf): {sw.ElapsedTicks} ticks");
                
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Compare memory usage
        /// </summary>
        private void CompareMemoryUsage()
        {
            Console.WriteLine("=== MEMORY USAGE ANALYSIS ===");
            Console.WriteLine();
            
            Console.WriteLine("Theoretical Memory Usage per Element:");
            Console.WriteLine();
            Console.WriteLine("Data Structure         | Memory per Element");
            Console.WriteLine("----------------------|-------------------");
            Console.WriteLine("Array of int          | 4 bytes");
            Console.WriteLine("List<int>             | 4 bytes + overhead");
            Console.WriteLine("Doubly Linked List    | 4 bytes + 16 bytes (2 pointers) + object overhead");
            Console.WriteLine();
            
            Console.WriteLine("Trade-offs:");
            Console.WriteLine("• Array: Most memory efficient, fastest access");
            Console.WriteLine("• List<T>: Good balance, dynamic sizing");
            Console.WriteLine("• Doubly Linked List: Higher memory cost, but O(1) insertion/deletion");
            Console.WriteLine();
            
            // Demonstrate actual memory usage
            int size = 10000;
            
            GC.Collect();
            long memBefore = GC.GetTotalMemory(false);
            
            var dll = new DoublyLinkedList<int>();
            for (int i = 0; i < size; i++)
            {
                dll.Add(i);
            }
            
            long memAfter = GC.GetTotalMemory(false);
            long dllMemory = memAfter - memBefore;
            
            GC.Collect();
            memBefore = GC.GetTotalMemory(false);
            
            var list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                list.Add(i);
            }
            
            memAfter = GC.GetTotalMemory(false);
            long listMemory = memAfter - memBefore;
            
            Console.WriteLine($"Actual Memory Usage for {size:N0} integers:");
            Console.WriteLine($"  Doubly Linked List: {dllMemory:N0} bytes ({(double)dllMemory / size:F1} bytes per element)");
            Console.WriteLine($"  List<T>: {listMemory:N0} bytes ({(double)listMemory / size:F1} bytes per element)");
        }
        
        /// <summary>
        /// Compare traversal performance
        /// </summary>
        private void CompareTraversalPerformance()
        {
            Console.WriteLine("=== TRAVERSAL PERFORMANCE TEST ===");
            Console.WriteLine();
            
            int size = LARGE_SIZE;
            Console.WriteLine($"Testing traversal with {size:N0} elements:");
            Console.WriteLine();
            
            // Prepare data structures
            var dll = new DoublyLinkedList<int>();
            var list = new List<int>();
            var array = new int[size];
            
            for (int i = 0; i < size; i++)
            {
                dll.Add(i);
                list.Add(i);
                array[i] = i;
            }
            
            // Test forward traversal
            Console.WriteLine("Forward Traversal:");
            
            var sw = Stopwatch.StartNew();
            foreach (var item in dll)
            {
                // Minimal work to avoid optimization
                var temp = item * 2;
            }
            sw.Stop();
            Console.WriteLine($"  Doubly Linked List (foreach): {sw.ElapsedMilliseconds}ms");
            
            sw.Restart();
            foreach (var item in list)
            {
                var temp = item * 2;
            }
            sw.Stop();
            Console.WriteLine($"  List<T> (foreach): {sw.ElapsedMilliseconds}ms");
            
            sw.Restart();
            for (int i = 0; i < array.Length; i++)
            {
                var temp = array[i] * 2;
            }
            sw.Stop();
            Console.WriteLine($"  Array (for loop): {sw.ElapsedMilliseconds}ms");
        }
        
        /// <summary>
        /// Run complete analysis of all performance aspects
        /// </summary>
        private void RunCompleteAnalysis()
        {
            Console.WriteLine("=== COMPLETE PERFORMANCE ANALYSIS ===");
            Console.WriteLine();
            Console.WriteLine("Running comprehensive tests... This may take a moment.");
            Console.WriteLine();
            
            CompareInsertionPerformance();
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            CompareDeletionPerformance();
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            CompareSearchPerformance();
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            CompareTraversalPerformance();
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            ShowRecommendations();
        }
        
        /// <summary>
        /// Show Big-O complexity comparison
        /// </summary>
        private void ShowBigOComparison()
        {
            Console.WriteLine("=== BIG-O COMPLEXITY COMPARISON ===");
            Console.WriteLine();
            
            Console.WriteLine("Operation            | Array   | List<T> | Doubly Linked List");
            Console.WriteLine("--------------------|---------|---------|-----------------");
            Console.WriteLine("Access by Index     | O(1)    | O(1)    | O(n)");
            Console.WriteLine("Insert at Beginning | O(n)    | O(n)    | O(1)");
            Console.WriteLine("Insert at End       | O(1)*   | O(1)*   | O(1)");
            Console.WriteLine("Insert at Middle    | O(n)    | O(n)    | O(n) to find + O(1) to insert");
            Console.WriteLine("Delete at Beginning | O(n)    | O(n)    | O(1)");
            Console.WriteLine("Delete at End       | O(1)    | O(1)    | O(1)");
            Console.WriteLine("Delete at Middle    | O(n)    | O(n)    | O(n) to find + O(1) to delete");
            Console.WriteLine("Search              | O(n)    | O(n)    | O(n)");
            Console.WriteLine("Traversal           | O(n)    | O(n)    | O(n)");
            Console.WriteLine();
            Console.WriteLine("* Amortized complexity (may require resizing)");
            Console.WriteLine();
            
            Console.WriteLine("Space Complexity:");
            Console.WriteLine("• Array: O(n)");
            Console.WriteLine("• List<T>: O(n) + extra capacity");
            Console.WriteLine("• Doubly Linked List: O(n) with higher constant factor (pointer overhead)");
        }
        
        /// <summary>
        /// Show recommendations for when to use each data structure
        /// </summary>
        private void ShowRecommendations()
        {
            Console.WriteLine("=== RECOMMENDATIONS ===");
            Console.WriteLine();
            
            Console.WriteLine("Use Arrays when:");
            Console.WriteLine("• You need fastest possible access by index");
            Console.WriteLine("• Memory usage is critical");
            Console.WriteLine("• Size is known and doesn't change often");
            Console.WriteLine("• You're doing mostly read operations");
            Console.WriteLine();
            
            Console.WriteLine("Use List<T> when:");
            Console.WriteLine("• You need dynamic sizing");
            Console.WriteLine("• You want a good balance of all operations");
            Console.WriteLine("• You're doing mixed operations (read/write)");
            Console.WriteLine("• You need indexed access occasionally");
            Console.WriteLine();
            
            Console.WriteLine("Use Doubly Linked Lists when:");
            Console.WriteLine("• You frequently insert/delete at the beginning or middle");
            Console.WriteLine("• You need to navigate both directions");
            Console.WriteLine("• You're implementing other data structures (deques, LRU caches)");
            Console.WriteLine("• Memory allocation patterns matter more than total memory usage");
            Console.WriteLine();
            
            Console.WriteLine("Real-world examples:");
            Console.WriteLine("• Music playlist (this assignment) - frequent navigation");
            Console.WriteLine("• Browser history (this assignment) - back/forward navigation");
            Console.WriteLine("• Text editor - insertion/deletion of characters");
            Console.WriteLine("• LRU cache implementation");
            Console.WriteLine("• Undo/redo functionality");
        }
    }
}