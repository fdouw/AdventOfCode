using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day24 : Solution {
        private long[] weights;

        public Day24 () : base(24, 2015, "It Hangs in the Balance") {
            weights = Input.Select(s => Int64.Parse(s)).OrderByDescending(n => n).ToArray();
        }

        public override string SolvePart1 () => $"{GetEntanglement(3)}";

        public override string SolvePart2 () => $"{GetEntanglement(4)}";

        /* Compute divisibility if weights are equally distributed among <nGroups> groups.
         * No testing is done if weights are divisible in <nGroups> groups. And we're assuming that
         * if group 1 takes 1/<nGroups>th of the weight, the solution is valid overall (ie groups 2..n can be generated).
         */
        private long GetEntanglement (int nGroups, bool verbose = false) {
            long groupWeight = weights.Sum() / nGroups;    // Assume divisibility
            if (verbose) System.Console.WriteLine($"Total weight: {weights.Sum()}  Single group: {groupWeight}  {nGroups} groups: {groupWeight * nGroups}");

            long entanglement = Int64.MaxValue;
            for (int i = 2; i < weights.Length; i++) {
                if (verbose) System.Console.WriteLine($"Searching length {i}");
                foreach (var combs in GetCombinations(weights, i)) {
                    if (combs.Sum() == groupWeight) {
                        if (verbose) System.Console.WriteLine($"Match: [{string.Join(',', combs)}]\t (Sum = {combs.Sum()})");
                        long tmp = combs.Aggregate((a,b) => a * b);
                        if (tmp < entanglement) {
                            entanglement = tmp;
                        }
                    }
                }
                if (entanglement < Int64.MaxValue) {
                    // Means we found a solution, and therefor an optimal solution
                    return entanglement;
                }
            }
            throw new Exception("No solution found");
        }

        public static IEnumerable<IList<T>> GetCombinations<T> (IList<T> list, int level, int start = 0) {
            if (start < list.Count) {
                if (level == 1) {
                    for (int i = start; i < list.Count; i++) {
                        yield return new T[] { list[i] };
                    }
                }
                else if (level > 1) {
                    // To keep things in order, first select this index and combine with subcombinations...
                    foreach (var sublist in GetCombinations(list, level - 1, start + 1)) {
                        List<T> tmp = new List<T>(sublist);
                        yield return tmp.Prepend(list[start]).ToList();
                    }
                    // ...then skip this index and yield subcombinations
                    foreach (var sublist in GetCombinations(list, level, start + 1)) {
                        yield return sublist;
                    }
                }
            }
        }
    }
}