using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015 {
    class Day19 : Solution {
        private List<(string pattern, string replace)> replacements;
        private Dictionary<string, string> revrep;
        private string molecule;

        public Day19 () : base(19   , 2015, "Medicine for Rudolph") {
            replacements = new List<(string pattern, string replace)>();
            foreach (string line in Input) {
                if (line.Length == 0) {
                    break;
                }
                replacements.Add((line.Split(" => ")[0], line.Split(" => ")[1]));
            }
            molecule = Input[^1];
        }

        public override string SolvePart1 () {
            HashSet<string> result = new HashSet<string>();
            foreach (var item in replacements) {
                foreach (var variant in ReplaceOnce(molecule, item.pattern, item.replace)) {
                    result.Add(variant);
                }
            }
            return $"{result.Count}";
        }

        public override string SolvePart2 () {
            // Idea copied from https://old.reddit.com/r/adventofcode/comments/3xflz8/day_19_solutions/cy4k8ca/
            // Reverse strings to make matching more efficient
            // [TODO: explain more]
            // Simply iterate over replacements until we're back at "e"
            //List<(string pattern, string replace)> revrep = new List<(string pattern, string replace)>();
            revrep = new Dictionary<string, string>();
            foreach (var rep in replacements) {
                revrep.Add(Reverse(rep.replace), Reverse(rep.pattern));
            }
            string revmolecule = Reverse(molecule);
            System.Console.WriteLine($"{revmolecule}");

            Regex re = new Regex(string.Join('|', revrep.Keys));

            int gen = 0;
            while (revmolecule != "e") {
                revmolecule = re.Replace(revmolecule, ReplaceMatch, 1, 0);
                gen++;
            }
            return $"{gen}";
        }

        public string ReplaceMatch (Match m) => revrep[m.Value];

        public static string Reverse (string s) => new string(s.Reverse().ToArray());

        public static IEnumerable<string> ReplaceOnce (string s, string pattern, string replace) {
            int i = 0;
            while ((i = s.IndexOf(pattern, i)) >= 0)
            {
                yield return s.Substring(0, i) + replace + s.Substring(i+pattern.Length);
                i += pattern.Length;
            }
        }
    }
}