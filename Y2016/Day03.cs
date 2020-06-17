using System;
using System.Linq;

namespace AdventOfCode.Y2016 {
    class Day03 : Solution {
        public Day03 () : base(3, 2016, "Squares With Three Sides") {
        }

        public override string SolvePart1 () {
            var data = Input.Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(t => Int32.Parse(t))
                                          .ToArray());
            int count = 0;
            foreach (int[] points in data) {
                if (feasible(points[0], points[1], points[2])) {
                    count++;                    
                }
            }
            return $"{count}";
        }

        public override string SolvePart2 () {
            var data = Input.Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(t => Int32.Parse(t))
                                          .ToArray())
                            .ToArray();
            int count = 0;
            for (int i = 0; i < data.Length; i+=3) {
                for (int col = 0; col < 3; col++) {
                    if (feasible(data[i][col], data[i+1][col], data[i+2][col])) {
                        count++;
                    }
                }
            }
            return $"{count}";
        }

        static bool feasible (int a, int b, int c) => (a + b > c && b + c > a && c + a > b);
    }
}