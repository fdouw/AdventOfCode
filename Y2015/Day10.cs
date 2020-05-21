using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day10 : Solution {
        public Day10 () : base(10, 2015, "Elves Look, Elves Say") {
        }

        public override string SolvePart1 () {
            return $"{PlayGame(Input[0], 40)}";
        }

        public override string SolvePart2 () {
            return $"{PlayGame(Input[0], 50)}";
        }

        /* Plays the game for <iterations> iterations, starting with sequence <seed>.
         * Returns the number of digits in the final sequence. */
        private static int PlayGame (string seed, int iterations) {
            List<int> current = new List<int>(seed.Length);
            List<int> next = new List<int>();

            // Read starting sequence
            foreach (char c in seed) {
                current.Add(Int32.Parse($"{c}"));
            }

            // Play game <iterations> times 
            for (int n=0; n<iterations; n++) {
                for (int i=0; i<current.Count; ) {
                    int d = current[i];
                    int len = 0;
                    do {
                        i++;
                        len++;
                    } while (i<current.Count && current[i] == d);
                    next.AddRange(digits(len));
                    next.Add(d);
                }
                current = next;
                next = new List<int>(current.Count);
            }

            return current.Count;
        }

        // Splits an integer into a list of its digits, base 10, in order
        private static List<int> digits (int num) {
            List<int> dlist = new List<int>();
            while (num > 0) {
                int d = num % 10;
                num /= 10;
                dlist.Add(d);
            }
            return dlist;
        }

        // for testing
        private static void DumpArray (int[] arr) => System.Console.WriteLine(string.Concat<int>(arr));
    }
}