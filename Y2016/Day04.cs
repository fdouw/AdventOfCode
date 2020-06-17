using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016 {
    class Day04 : Solution {
        public Day04 () : base(4, 2016, "Security Through Obscurity") {
        }

        public override string SolvePart1 () {
            Regex re = new Regex(@"(?<name>[-a-z]+)(?<id>[0-9]+)\[(?<checksum>[a-z]+)\]");
            int idSum = 0;
            foreach (string line in Input) {
                Match match = re.Match(line);
                if (match.Success) {
                    // Compute the checksum of the name
                    char[] ca = match.Groups["name"].Value.Replace("-", null)
                                                .GroupBy(c => c)
                                                .OrderByDescending(g => g.Count())
                                                .ThenBy(g => g.Key)
                                                .Take(5)
                                                .Select(g => g.Key)
                                                .ToArray();
                    string computedChecksum = new string(ca);
                    if (computedChecksum == match.Groups["checksum"].Value) {
                        idSum += Int32.Parse(match.Groups["id"].Value);
                    }
                    //System.Console.WriteLine($"{computedChecksum}\t{match.Groups["checksum"].Value}\t{match.Groups["id"].Value}");
                }
            }
            return $"{idSum}";
        }

        public override string SolvePart2 () {
            Regex re = new Regex(@"(?<name>[-a-z]+)(?<id>[0-9]+)\[(?<checksum>[a-z]+)\]");
            foreach (string line in Input) {
                Match match = re.Match(line);
                if (match.Success) {
                    int shift = Int32.Parse(match.Groups["id"].Value);
                    string s = new string(line.Select(c => (c == '-') ? ' ' : (char)((c - 'a' + shift) % 26 + 'a')).ToArray());
                    //System.Console.WriteLine(s);
                    if (Regex.IsMatch(s,"north ?pole object")) {
                        return $"{shift}";
                    }
                }
            }
            return $"No luck";
        }
    }
}