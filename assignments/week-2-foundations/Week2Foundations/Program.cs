using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Week2Foundations
{
    class Program
    {
        // PART 2 - Micro-Tasks
        static void Main()
        {
            Console.WriteLine("=== Week 2: Foundations ===");
            Console.WriteLine("by Edwin Pacheco\n");

            // RunArrayDemo();
            // RunListDemo();
            // RunStackDemo();
            // RunQueueDemo();
            // RunDictionaryDemo();
            // RunHashSetDemo();
            RunBenchmarks();
        }

        // A. Array
        static void RunArrayDemo()
        {
            Console.WriteLine("=== Arrays ===");
            int[] scores = new int[10];
            scores[0] = 86; scores[1] = 93; scores[2] = 100;

            Console.WriteLine($"{scores[1]}");

            for (int i = 0; i < scores.Length; i++)
                Console.Write($"{scores[i]} ");
            Console.WriteLine();

            int max = int.MinValue;
            foreach (var t in scores) max = Math.Max(max, t);
            Console.WriteLine($"Max score: {max}\n");
        }

        // B. List<T>
        static void RunListDemo()
        {
            Console.WriteLine("=== List<T> ===");
            var nums = new List<int> { 1, 2, 3, 4, 5 };
            nums.Insert(2, 99); // O(n)
            nums.Remove(99);
            Console.WriteLine($"Count after remove: {nums.Count}\n");
        }

        // C. Stack<T>
        static void RunStackDemo()
        {
            Console.WriteLine("=== Stack<T> ===");
            var stack = new Stack<string>();
            stack.Push("GitHub.com");
            stack.Push("Meetup.com");
            stack.Push("Handshake.com");
            Console.WriteLine($"Peek: {stack.Peek()}\n");
            while (stack.Count > 0)
                Console.WriteLine($"Pop: {stack.Pop()}");
            Console.WriteLine();
        }

        // D. Queue<T>
        static void RunQueueDemo()
        {
            Console.WriteLine("=== Queue<T> ===");
            var q = new Queue<int>();
            q.Enqueue(204); q.Enqueue(205); q.Enqueue(206);
            Console.WriteLine($"Peek Next: {q.Peek()}");
            while (q.Count > 0)
                Console.WriteLine($"Dequeue Processing Order: {q.Dequeue()}");
            Console.WriteLine();
        }

        // E. Dictionary<TKey,TValue>
        static void RunDictionaryDemo()
        {
            Console.WriteLine("=== Dictionary<TKey, TValue> ===");
            var inventory = new Dictionary<string, int>
            {
                ["SKU-206"] = 23,
                ["SKU-863"] = 12,
                ["SKU-444"] = 9
            };
            inventory["SKU-206"] += 5;
            if (inventory.TryGetValue("SKU-999", out var qty))
                Console.WriteLine(qty);
            else
                Console.WriteLine("SKU-999 not found");
            Console.WriteLine();
        }

        // F. HashSet<T>
        static void RunHashSetDemo()
        {
            Console.WriteLine("=== HashSet<T> ===");
            var set = new HashSet<int> { 1, 2, 3 };
            Console.WriteLine($"Add 2 again? {set.Add(2)}");
            var a = new HashSet<int> { 1, 2, 3 };
            var b = new HashSet<int> { 3, 4, 5 };
            a.UnionWith(b);
            Console.WriteLine($"Union size: {a.Count}\n");
        }

        // PART 3 ---------------
        // Benchmark (Observation)
        static void RunBenchmarks()
        {
            Console.WriteLine("=== Benchmarks ===");
            int[] Ns = { 1000, 10000, 100000, 250000 };

            foreach (int N in Ns)
            {
                // Test data
                var list = new List<int>();
                var set = new HashSet<int>();
                var dict = new Dictionary<int, bool>();

                for (int i = 0; i < N; i++)
                {
                    list.Add(i);
                    set.Add(i);
                    dict[i] = true;
                }

                int present = N - 1;
                int missing = -1;
                var sw = new Stopwatch();

                Console.WriteLine($"\nN={N}");

                // Present element (N-1)
                sw.Restart();
                list.Contains(present);
                sw.Stop();
                Console.WriteLine($"List.Contains(N-1):   {sw.Elapsed.TotalMilliseconds:0.000} ms");

                sw.Restart();
                set.Contains(present);
                sw.Stop();
                Console.WriteLine($"HashSet.Contains:     {sw.Elapsed.TotalMilliseconds:0.000} ms");

                sw.Restart();
                dict.ContainsKey(present);
                sw.Stop();
                Console.WriteLine($"Dict.ContainsKey:     {sw.Elapsed.TotalMilliseconds:0.000} ms");

                // Missing element (-1)
                sw.Restart();
                list.Contains(missing);
                sw.Stop();
                Console.WriteLine($"List.Contains(-1):    {sw.Elapsed.TotalMilliseconds:0.000} ms");

                sw.Restart();
                set.Contains(missing);
                sw.Stop();
                Console.WriteLine($"HashSet.Contains(-1): {sw.Elapsed.TotalMilliseconds:0.000} ms");

                sw.Restart();
                dict.ContainsKey(missing);
                sw.Stop();
                Console.WriteLine($"Dict.ContainsKey(-1): {sw.Elapsed.TotalMilliseconds:0.000} ms");
            }
        }
    }
}