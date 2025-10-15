using System.Diagnostics;

public class Program
{
    static void Main()
    {
        
        Console.WriteLine("\nArray:");
        RunArrayDemo();
        Console.WriteLine("\nList<T>:");
        RunListDemo();
        Console.WriteLine("\nStack<T>:");
        RunStackDemo();
        Console.WriteLine("\nQueue<T>:");
        RunQueueDemo();
        Console.WriteLine("\nDictionary<TKey,TValue>:");
        RunDictionaryDemo();
        Console.WriteLine("\nHashSet<T>:");
        RunHashSetDemo();

        Console.WriteLine("\n\nBenchmarks (Median):");
        Benchmark.RunBench();
    }

    static void RunArrayDemo()
    {
        int[] numbers = new int[10];

        numbers[0] = 5;
        numbers[2] = 42;
        numbers[5] = 17;
        Console.WriteLine($"Value at index 2: {numbers[2]}");

        int target = 17;
        bool found = false;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == target)
            {
                found = true;
                break;
            }
        }
        Console.WriteLine(found ? $"Found {target}" : $"Did not find {target}");
    }

    static void RunListDemo()
    {
        List<int> numbers = new List<int>();
        for (int i = 1; i <= 5; i++)
        {
            numbers.Add(i);
        }

        numbers.Insert(2, 99);
        numbers.RemoveAt(2);

        Console.WriteLine($"List count: {numbers.Count}");
    }

    static void RunStackDemo()
    {
        Stack<string> urls = new Stack<string>();
        urls.Push("https://google.com");
        urls.Push("https://bing.com");
        urls.Push("https://duckduckgo.com");
        Console.WriteLine($"Latest URL {urls.Peek()}");

        Console.Write("URLs visited: ");
        foreach (string url in urls)
        {
            Console.Write(url + " ");
        }
        Console.WriteLine();
    }

    static void RunQueueDemo()
    {
        Queue<string> prints = new Queue<string>();
        prints.Enqueue("Taxes");
        prints.Enqueue("Lease");
        prints.Enqueue("Cats");

        Console.WriteLine($"Next print: {prints.Peek()}");

        Console.WriteLine("Print Queue:\nNo\tName");
        for (int num = 1; prints.Count > 0; num++)
        {
            string job = prints.Dequeue();
            Console.WriteLine(num + "\t" + job);
        }
    }

    static void RunDictionaryDemo()
    {
        Dictionary<string, int> items = new Dictionary<string, int>();
        items.Add("banana", 3);
        items.Add("apple", 5);
        items.Add("jet engine", 20);

        items["apple"]++;

        Console.WriteLine("Can I get \"missing\"?: " + items.TryGetValue("missing", out int result));
    }

    static void RunHashSetDemo()
    {
        HashSet<int> numbers = new HashSet<int>();
        List<int> inputNumbers = new List<int> { 0, 1, 2, 2, 1 };
        inputNumbers.ForEach(num =>
        {
            Console.WriteLine($"Number {num} added " + (numbers.Add(num) ? "Succesfully" : "Unsuccesfully"));
        });

        numbers.UnionWith(new List<int> { 3, 4, 5 });
        Console.WriteLine($"HashSetDemo final count: {numbers.Count}");
    }
}

public class Benchmark()
{
    public static void RunBench()
    {
        Bench(1_000);
        Bench(10_000);
        Bench(100_000);
        Bench(250_000);
    }
    static void Bench(int size)
    {
        Console.WriteLine("\nN=" + size);
        List<int> listToTest = Enumerable.Range(0, size).ToList();
        HashSet<int> hashSetToTest = Enumerable.Range(0, size).ToHashSet();
        Dictionary<int, bool> dictToTest = Enumerable.Range(0, size).ToDictionary(x => x, x => false);

        Dictionary<string, List<double>> results = new Dictionary<string, List<double>>()
        {
            { "List1", new List<double>() },
            { "HashSet1", new List<double>() },
            { "Dictionary1", new List<double>() },
            { "List2", new List<double>() },
            { "HashSet2", new List<double>() },
            { "Dictionary2", new List<double>() }
        };

        for (int i = 0; i < 100; i++)
        {
            results["List1"].Add(listBench(listToTest, size - 1));
            results["HashSet1"].Add(hashsetBench(hashSetToTest, size - 1));
            results["Dictionary1"].Add(dictBench(dictToTest, size - 1));
            results["List2"].Add(listBench(listToTest, -1));
            results["HashSet2"].Add(hashsetBench(hashSetToTest, -1));
            results["Dictionary2"].Add(dictBench(dictToTest, -1));
        }
        Console.WriteLine($"List.Contains(N-1):\t\t{Median(results["List1"])} ms");
        Console.WriteLine($"HashSet.Contains(N-1):\t\t{Median(results["HashSet1"])} ms");
        Console.WriteLine($"Dictionary.ContainsKey(N-1):\t{Median(results["Dictionary1"])} ms");
        Console.WriteLine($"List.Contains(-1):\t\t{Median(results["List2"])} ms");
        Console.WriteLine($"HashSet.Contains(-1):\t\t{Median(results["HashSet2"])} ms");
        Console.WriteLine($"Dictionary.ContainsKey(-1):\t{Median(results["Dictionary2"])} ms");
    }

    static double listBench(List<int> where, int what)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        where.Contains(what);
        stopWatch.Stop();
        return stopWatch.Elapsed.TotalMilliseconds;
    }

    static double hashsetBench(HashSet<int> where, int what)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        where.Contains(what);
        stopWatch.Stop();
        return stopWatch.Elapsed.TotalMilliseconds;
    }

    static double dictBench(Dictionary<int, bool> where, int what)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        where.ContainsKey(what);
        stopWatch.Stop();
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    public static double Median(List<double> list)
    {
        var sorted = list.OrderBy(x => x).ToList();
        int n = sorted.Count;
        return n % 2 == 1
            ? sorted[n / 2]
            : (sorted[(n / 2) - 1] + sorted[n / 2]) / 2.0;
    }


}