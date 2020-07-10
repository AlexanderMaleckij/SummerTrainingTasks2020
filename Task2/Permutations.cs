using System.Collections.Generic;
using System.Linq;

namespace Task2Figures
{
    /// <summary>
    /// Serves to get all permutations of the collection without repeting
    /// </summary>
    class Permutations
    {
        /// <summary>
        /// Serves to get all permutations of the collection elements without repeats
        /// </summary>
        /// <typeparam name="T">type of source items collection</typeparam>
        /// <param name="items">list of items for permutation</param>
        /// <returns>list of items permutations lists</returns>
        public static List<List<T>> GetPermutations<T>(List<T> items)
        {
            int n = items.Count;
            if(n == 0)
            {
                return null;
            }
            List<List<int>> naturalSeriesPermutations = GetNaturalSeriesPermutations(n);
            List<List<T>> itemsPermutations = new List<List<T>>();

            foreach(List<int> indexes in naturalSeriesPermutations)
            {
                List<T> permutation = new List<T>();
                foreach(int itemIndex in indexes)
                {
                    permutation.Add(items[itemIndex]);
                }
                itemsPermutations.Add(permutation);
            }
            return itemsPermutations;
        }

        /// <summary>
        /// Serves to obtain all permutations of elements of the natural series
        /// withoit repeats
        /// </summary>
        /// <param name="n">amount of items in permutation</param>
        /// <returns></returns>
        private static List<List<int>> GetNaturalSeriesPermutations(int n)
        {
            List<int> naturalSeries = Enumerable.Range(0, n).ToList();
            List<List<int>> permutations = new List<List<int>>();
            do
            {
                permutations.Add(new List<int>(naturalSeries));
            }
            while (NextPermutation(naturalSeries));

            return permutations;
        }

        /// <summary>
        /// Use Narayana algorithm to get next lexicographic permutation
        /// of the list
        /// </summary>
        /// <param name="list">previous permutation</param>
        /// <returns>is next permutation exists</returns>
        private static bool NextPermutation(List<int> list)
        {
            int i, k, t;
            int n = list.Count;
            //search number k, that:  a[k] < a[k+1] > ... > a[n-1]
            for (k = n - 2; (k >= 0) && (list[k] > list[k + 1]); k--) { };
            //if it is the last permutation
            if(k == 1)
            {
                return false;
            }
            /*search t, t > k such that among a [k + 1], ... , a [n – 1]
             a[t] is the minimum number greater than a[k]*/
            for (t = n - 1; (list[k] > list[t]) && (t >= k + 1); t--) { };
            Swap(list, k, t);
            for(i = k + 1; i <= (n + k) / 2; i++)
            {
                t = n + k - i;
                Swap(list, i, t);
            }
            return true;
        }

        /// <summary>
        /// Swaps collection items with indexes i1 and i2
        /// </summary>
        /// <param name="list">list for swapping</param>
        /// <param name="i1">index of the 1st swapping element</param>
        /// <param name="i2">index of the 2nd swapping element</param>
        private static void Swap(List<int> list, int i1, int i2)
        {
            int tmp = list[i1];
            list[i1] = list[i2];
            list[i2] = tmp;
        }
    }
}
