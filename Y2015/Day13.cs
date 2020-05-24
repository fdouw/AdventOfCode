using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day13 : Solution {
        public Day13 () : base(13, 2015, "Knights of the Dinner Table") {
        }

        public override string SolvePart1 () {
            // Build a list of two-way links
            PairLinkCollection pairs = ReadPairLinks();
            return $"{MaxCircleValue(pairs)}";
        }

        public override string SolvePart2 () {
            // Build a list of two-way links
            PairLinkCollection pairs = ReadPairLinks();

            // Add myself
            string[] people = pairs.Persons.ToArray();  // Copy the list first, because the loop will alter it
            foreach (string other in people) {
                pairs.Add(new PairLink("Myself", other, 0));
            }

            return $"{MaxCircleValue(pairs)}";
        }

        private PairLinkCollection ReadPairLinks () {
            var pairs = new PairLinkCollection();
            PairLink pairlink;
            foreach (string line in Input) {
                string[] words = line.Split(' ');
                int sign = (words[2] == "lose") ? -1 : 1;
                int value = Int32.Parse(words[3]);
                string person = words[0];
                string other = words[^1].Substring(0, words[^1].Length-1);   // Remove full stop at the end
                if ((pairlink = pairs.GetPairLink(person, other)) != null) {
                    pairlink.weight += sign * value;
                }
                else {
                    pairs.Add(new PairLink(person, other, sign * value));
                }
            }
            return pairs;
        }

        private static int MaxCircleValue (PairLinkCollection pairs) {
            // Do a DFS for the optimal path. Because they're sitting in a circle, it doesn't matter
            // with who we start. Just make sure to check all the links.
            int maxValue = 0;
            string root = pairs.Persons.First();
            int maxDepth = pairs.GetLinks(root).Count;  // Assuming root is paired with every other person
            foreach (PairLink link in pairs.GetLinks(root)) {
                maxValue = MaxPathValue(pairs, link, link.GetOtherPerson(root), root, maxDepth, maxValue);
            }
            return maxValue;
        }

        private static int MaxPathValue (PairLinkCollection allLinks, PairLink curLink, string curPerson, string root, int maxDepth, int curMaxValue) {
            if (curLink.Length == maxDepth) {
                // Found the last link, tally up the values
                int val = curLink.ChainWeight + allLinks.GetPairLink(curPerson, root).weight;
                return (val > curMaxValue) ? val : curMaxValue;
            }
            else {
                foreach (PairLink link in allLinks.GetLinks(curPerson)) {
                    string other = link.GetOtherPerson(curPerson);
                    if (!curLink.ChainContainsPerson(other)) {
                        curLink = curLink.AttachLink(link);
                        curMaxValue = MaxPathValue(allLinks, curLink, other, root, maxDepth, curMaxValue);
                        curLink = curLink.Detach();
                    }
                }
                return curMaxValue;
            }
        }

        private class PairLink {
            internal int Length { get => 1 + (previous?.Length ?? 0); }
            internal int ChainWeight { get => weight + (previous?.ChainWeight ?? 0); }

            internal string personA;
            internal string personB;
            internal int weight;
            internal PairLink previous;
            internal PairLink next;

            internal PairLink (string a, string b, int w, PairLink prev = null, PairLink nxt = null) {
                personA = a;
                personB = b;
                weight = w;
                previous = prev;
                next = nxt;
            }

            // Attaches a new link to the chain
            internal PairLink AttachLink (PairLink other) {
                next = other;
                other.previous = this;
                return other;
            }

            // Detaches this link from the preceding chain.
            // NB: consecutive links remain attached to this link, not to the chain!
            // Returns the previous link, or null if this was the root.
            internal PairLink Detach () {
                PairLink tmp = previous;
                if (previous != null) {
                    tmp.next = null;
                    previous = null;
                }
                return tmp;
            }

            internal string GetOtherPerson (string person) {
                if (person == personA) {
                    return personB;
                }
                else if (person == personB) {
                    return personA;
                }
                else {
                    throw new Exception($"Person not in this link: {person} (Link: {personA}–{personB})");
                }
            }

            internal bool ChainContainsLink (PairLink other) {
                return other == this || (previous?.ChainContainsLink(other) ?? false);
            }

            internal bool ChainContainsPerson(string p) {
                return (p == personA || p == personB || (previous?.ChainContainsPerson(p) ?? false));
            }
        }

        private class PairLinkCollection {
            internal IEnumerable<string> Persons { get => links.Keys; }

            Dictionary<string,List<PairLink>> links = new Dictionary<string,List<PairLink>>();
            
            internal void Add(PairLink newLink) {
                if (!links.ContainsKey(newLink.personA)) {
                    links.Add(newLink.personA, new List<PairLink>());
                }
                if (!links.ContainsKey(newLink.personB)) {
                    links.Add(newLink.personB, new List<PairLink>());
                }
                links[newLink.personA].Add(newLink);
                links[newLink.personB].Add(newLink);
            }

            internal List<PairLink> GetLinks (string person) => links[person];

            internal PairLink GetPairLink (string personA, string personB) {
                if (links.ContainsKey(personA)) {
                    foreach (PairLink pair in links[personA]) {
                        if (pair.personA == personB || pair.personB == personB) {
                            return pair;
                        }
                    }
                }
                return null;
            }
        }
    }
}