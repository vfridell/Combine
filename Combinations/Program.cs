using System;
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
            List<int> items = new List<int> { 1, 2, 3, 4, 5 };
            int r = 2;
            int startIndex = 0;

            List<List<int>> outputList;
            Combine(items, r, startIndex, out outputList);
            foreach (List<int> workingList in outputList)
            {
                Console.WriteLine(workingList.Aggregate("", (s, v) => s + $"{v},"));
            }
        }

        public static void Combine<T>(List<T> items, int r, int startIndex, out List<List<T>> outputList)
        {
            outputList = new List<List<T>>();
            for (int i = startIndex; i < (startIndex + r); i++)
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

            for (int i = parentIndex + 1; i < Math.Min(items.Count, r + depth); i++)
            {
                workingList.Add(items[i]);
                Combine(workingList, items, r, depth + 1, i, outputList);
                workingList.RemoveAt(workingList.Count - 1);
            }
        }


    }
}
