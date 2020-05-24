using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day13 : Solution {
        public Day13 () : base(13, 2015, "Knights of the Dinner Table") {
        }

        public override string SolvePart1 () {
            var links = new Dictionary<string, Dictionary<string, int>>();

            // Read the individual (one-way) links
            foreach (string line in Input) {
                string[] words = line.Split(' ');
                if (!links.ContainsKey(words[0])) {
                    links.Add(words[0], new Dictionary<string, int>());
                }
                int sign = (words[2] == "lose") ? -1 : 1;
                int value = Int32.Parse(words[3]);
                string other = words[^1].Substring(0,words[^1].Length-1);   // Remove full stop at the end
                links[words[0]].Add(other, sign * value);
            }

            // Build a list of two-way links
            var bilinks = new Dictionary<(string, string), int>(links.Keys.Count);
            var queue = new Queue<string>(links.Keys);
            while (queue.Count > 0) {
                string person = queue.Dequeue();
                foreach (string other in queue) {
                    bilinks.Add((person,other), links[person][other] + links[other][person]);
                }
            }

            // Order links by value, to prioritise high value pairs
            // Add links from high to low value, but only if:
            // - both people are unseated, or
            // - 1 is unseated, the other has a seat available, or
            // - the link is the last link, closing the circle
            var sortedLinks = from entry in bilinks orderby entry.Value descending select entry;
            var people = new Dictionary<string, int>(links.Keys.Count); // Keeps track of the number of neighbours
            foreach (string person in links.Keys) {
                people.Add(person, 0);
            }
            int totalWeight = 0;
            int totalLinks = 0;
            foreach (var link in sortedLinks) {
                string a = link.Key.Item1;
                string b = link.Key.Item2;
                if (people[a] + people[b] < 2) {
                    // At most one of the 2 can be seated, unless we are at the end
                    totalWeight += link.Value;
                    people[a]++;
                    people[b]++;
                    totalLinks += 2;
                }
                else if (totalLinks == people.Count && people[a] == 1 && people[b] == 1) {
                    // This is the closing link
                    totalWeight += link.Value;
                    // No need to update counts; we're done
                    break;
                }
            }

            // Not sure if totalWeight is guaranteed to be optimal
            return $"{totalWeight}";
        }

        // public override string SolvePart2 () {
        // }
    }
}