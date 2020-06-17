using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016 {
    class Day07 : Solution {
        public Day07 () : base(7, 2016, "Internet Protocol Version 7") {
        }

        public override string SolvePart1 () {
            Regex reAbba = new Regex(@"(?!(\w)\1)(\w)(\w)\3\2");            // Matches ABBA
            Regex reHype = new Regex(@"\[\w*(?!(\w)\1)(\w)(\w)\3\2\w*\]");  // Matches [...ABBA...]
            Regex re = new Regex(@"^(?!.*\[\w*(?!(\w)\1)(\w)(\w)\3\2\w*\]).*(?!(\w)\4)(\w)(\w)\6\5");   // Ugly but cool combination of the two
            int count = 0;
            foreach (string line in Input) {
                // if (reAbba.Match(line).Success && !reHype.Match(line).Success) {
                if (re.Match(line).Success) {
                    count++;
                }
            }
            return $"{count}";
        }

        public override string SolvePart2 () {
            Regex re = new Regex(@"(?!(\w)\1)(\w)(\w)\2.*@.*\3\2\3"); // Matches ABA...@...BAB
            char[] delim = new char[] {'[', ']'};
            int count = 0;
            foreach (string line in Input) {
                // Split on brackets: evens are outside, odds are inside brackets.
                // Put the sections back together, separated by '@', so we can use regex to check the pattern
                // Ie: <outside>@<inside>
                string[] sections = line.Split(delim);
                string transformed = "";
                for (int i = 0; i < sections.Length; i+= 2) {
                    transformed += sections[i];
                }
                transformed += "@";
                for (int i = 1; i < sections.Length; i+= 2) {
                    transformed += sections[i];
                }

                if (re.Match(transformed).Success) {
                    count++;
                }
            }
            return $"{count}";
        }
    }
}