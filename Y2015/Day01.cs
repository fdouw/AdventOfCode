using System.Linq;

namespace AdventOfCode.Y2015 {
    class Day01 : Solution {
        public Day01 () : base(1, 2015, "Not Quite Lisp", "Y2015/input01") {
        }

        public override string SolvePart1 () {
            return $"{Input[0].Count(c => c == '(') - Input[0].Count(c => c == ')')}";
        }

        public override string SolvePart2 () {
            int step = 1;
            int floor = 0;
            foreach (char c in Input[0]) {
                if (c == '(') floor++;
                if (c == ')') floor--;
                if (floor == -1) return $"{step}";
                step++;
            }
            return "No solution found.";
        }
    }
}