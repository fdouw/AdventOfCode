using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2016 {
    class Day01 : Solution {
        public Day01 () : base(1, 2016, "No Time for a Taxicab") {
        }

        public override string SolvePart1 () {
            string[] instructions = Input[0].Split(", ");

            // General idea: start at (0,0), keep track of direction. Update coordinates. Finally compute taxicab distance.
            int dir = 0;
            int x = 0;
            int y = 0;

            foreach (string instruct in instructions) {
                if (instruct[0] == 'R') {
                    dir = (dir + 1) % 4;
                }
                else {  // 'L'
                    dir = (dir - 1 + 4) % 4;
                }
                int blocks = Int32.Parse(instruct.Substring(1));

                switch (dir) {
                    case 0: y -= blocks; break;
                    case 1: x += blocks; break;
                    case 2: y += blocks; break;
                    case 3: x -= blocks; break;
                    default: throw new Exception($"Invalid direction: {dir}");
                }
            }

            return $"{Abs(x) + Abs(y)}";
        }

        public override string SolvePart2 () {
            string[] instructions = Input[0].Split(", ");

            // General idea: start at (0,0), keep track of direction, keep visited coordinates in a set, and return once we revisit a location.
            int dir = 0;
            int x = 0;
            int y = 0;
            HashSet<(int,int)> visited = new HashSet<(int, int)>();
            visited.Add((0,0));

            foreach (string instruct in instructions) {
                if (instruct[0] == 'R') {
                    dir = (dir + 1) % 4;
                }
                else {  // 'L'
                    dir = (dir - 1 + 4) % 4;
                }
                int blocks = Int32.Parse(instruct.Substring(1));

                int step = (dir == 0 || dir == 3) ? -1 : 1;
                bool horizontal = (dir == 1 || dir == 3);
                if (Walk(blocks, step, horizontal)) {
                    return $"{Abs(x) + Abs(y)}";
                }
            }

            return "No solution found";

            // Walk <blocks> blocks, or less if we revisit a place.
            // Updates x (iff <horizontal> is true), or y. In case of a revisit, x,y is the revisited place.
            // <step> = ±1, to indicate the direction along the axis.
            // Returns true iff we revisit a place.
            bool Walk (int blocks, int step, bool horizontal) {
                for (int i=0; i<blocks; i++) {
                    if (horizontal) {
                        x += step;
                    }
                    else {
                        y += step;
                    }
                    if (visited.Contains((x,y))) {
                        return true;
                    }
                    else {
                        visited.Add((x,y));
                    }
                }
                return false;
            }
        }

        private static int Abs (int x) => (x < 0) ? -x : x;
    }
}