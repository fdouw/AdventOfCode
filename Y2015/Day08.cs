using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015 {
    class Day08 : Solution {
        public Day08 () : base(8, 2015, "Matchsticks") {
        }

        public override string SolvePart1 () {
            MatchEvaluator eval = new MatchEvaluator(Day08.HexToCharEvaluator);
            int codeLength = 0;
            int textLength = 0;
            foreach (string line in Input) {
                codeLength += line.Length;                
                // No nice Count() method to count substrings, do a transform instead
                string transformed = Regex.Replace(line, "\\\\x([0-9A-Fa-f]{2})", eval); // Translate hex codes
                textLength += transformed.Substring(1, transformed.Length-2)             // Quotes at start and end
                                         .Replace("\\\"", "\"")
                                         .Replace("\\\\", "\\")
                                         .Length;
            }
            return $"{codeLength - textLength}";
        }

        public override string SolvePart2 () {
            int oldLength = 0;
            int newLength = 0;
            foreach (string line in Input) {
                oldLength += line.Length;
                newLength += line.Replace("\\", "\\\\")
                                 .Replace("\"", "\\\"")
                                 .Length + 2;
            }
            return $"{newLength - oldLength}";
        }

        private static string HexToCharEvaluator (Match match) {
            string hex = match.Groups[1].Value;
            // Int to string gives a number (eg "12"), char to string does not compile
            return $"{(char) Convert.ToInt32(hex, 16)}";
        }
    }
}