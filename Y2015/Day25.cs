using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015 {
    class Day25 : Solution {
        public Day25 () : base(25, 2015, "Let It Snow") {
        }

        public override string SolvePart1 () {
            // Problem is to read out row, column from the computed table
            Match data = Regex.Match(Input[0], @"row (?<row>\d+), column (?<col>\d+)");
            long row = Int64.Parse(data.Groups["row"].Value);
            long col = Int64.Parse(data.Groups["col"].Value);

            // Given in problem statement:
            long seed = 20151125;
            long mul = 252533;
            long mod = 33554393;
            Rng rng = new Rng(seed, mul, mod);

            // Derive the index of the result we want from the row and column, using the picture.
            // Note that on the first row, we have the triangular numbers. Ie:
            // (1, c) = T_c = c (c + 1) / 2
            // To move to r rows down, move r columns to the right, and track back r steps along the diagonal:
            // (r, c) = T_(r+c) - r + 1 = (r + c) * (r + c + 1) / 2 - r + 1
            // Finally compensate the fact that rows are 1-based instead of 0-based (this works out for the columns though).
            // Compensation only for computing the right triangular number, not tracking back.
            long target = (row - 1 + col) * (row - 1 + col + 1) / 2 - row + 1;
            long cur = seed;
            for (int i = 1; i < target; i++) {  // Seed is already the first index
                cur = (cur * mul) % mod;
            }
            return $"{cur}";
        }

        // public override string SolvePart2 () {
        // }

        class Rng {
            private long current, mul, mod;
            internal Rng  (long seed, long mul, long mod) {
                current = seed;
                this.mul = mul;
                this.mod = mod;
            }
            long Next () => current = (current * mul) % mod;
        }
    }
}