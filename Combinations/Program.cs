﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinations
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> phases = new LinkedList<int>( new int[] { 0, 1, 2, 3, 4, });
            List<List<int>> results = new List<List<int>>();
            Console.WriteLine($"{phases.Count} combine {phases.Count} Should be: {NPR(phases.Count, phases.Count, false)}");
            Permute(phases, new List<int>(), results);
            Console.WriteLine(results.Count);
            foreach (List<int> workingList in results)
            {
                Console.WriteLine(workingList.Aggregate("", (s, v) => s + $"{v},"));
            }

            List<int> items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 43 };
            int r = 8;

            Console.WriteLine($"{items.Count} combine {r} Should be: {NCR(items.Count, r)}");

            List<List<int>> outputList;
            Combine(items, r, out outputList);
            Console.WriteLine(outputList.Count);
            foreach (List<int> workingList in outputList)
            {
                Console.WriteLine(workingList.Aggregate("", (s, v) => s + $"{v},"));
            }
        }

        public static void Combine<T>(List<T> items, int r, out List<List<T>> outputList)
        {
            if (r > items.Count) throw new ArgumentException($"Cannot choose {r} items from a list of {items.Count}");
            outputList = new List<List<T>>();
            for (int i = 0; i < ((items.Count + 1) - r); i++)
            {
                List<T> workingList = new List<T> { items[i] };
                Combine(workingList, items, r, 1, i, outputList);
            }
        }

        public static void Combine<T>(List<T> workingList, List<T> items, int r, int depth, int parentIndex, List<List<T>> outputList)
        {
            if (depth == r)
            {
                var newlist = new List<T>();
                newlist.AddRange(workingList);
                outputList.Add(newlist);
                return;
            }

            int limit = r > items.Count / 2 ? Math.Min(items.Count, r + depth) : Math.Max(items.Count, r + depth);
            for (int i = parentIndex + 1; i < limit; i++)
            {
                workingList.Add(items[i]);
                Combine(workingList, items, r, depth + 1, i, outputList);
                workingList.RemoveAt(workingList.Count - 1);
            }
        }

        public static void Permute<T>(LinkedList<T> remaining, List<T> constructing, List<List<T>> result)
        {
            if (remaining.Count == 1)
            {
                constructing.Add(remaining.First.Value);
                result.Add(constructing);
                return;
            }

            for (int i = 0; i < remaining.Count; i++)
            {
                LinkedList<T> subLinkedList = new LinkedList<T>(remaining);
                int moves = 0;
                var enumerator = subLinkedList.GetEnumerator();
                enumerator.MoveNext();
                while (moves < i)
                {
                    enumerator.MoveNext();
                    moves++;
                }
                subLinkedList.Remove(enumerator.Current);
                List<T> subConstructing = new List<T>(constructing);
                subConstructing.Add(enumerator.Current);
                Permute(subLinkedList, subConstructing, result);
            }
        }

        public static int NCR(int n, int k)
        {
            if (k > n) return 0;
            return Factorial(n, n - k + 1) / Factorial(k);
        }

        public static int NPR(int n, int k, bool repeat)
        {
            if (k > n) return 0;
            if(repeat)
            {
                return (int)Math.Pow(n, k);
            }
            else
            {
                return Factorial(n) / Factorial(n - k);
            }
        }

        public static int Factorial(int num, int lowerlimit = 1)
        {
            int result = 1;
            while(num >= lowerlimit)
            {
                result *= num--;
            }
            return result;
        }

    }
}
