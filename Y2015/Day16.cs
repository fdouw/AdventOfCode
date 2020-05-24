using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day16 : Solution {
        public Day16 () : base(16, 2015, "Aunt Sue") {
        }

        public override string SolvePart1 () {
            // Store data about the gifter, given in problem
            Dictionary<string,int> gifter = ReadDictionary("children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1");
            
            // Import data about the aunts and compare to the gifter
            var list = new List<int>();
            for (int i=0; i<Input.Length; i++) {
                Dictionary<string, int> aunt = ReadDictionary(Input[i].Split(':', 2)[1]);
                bool isGifter = true;
                foreach (string k in aunt.Keys) {
                    if (aunt[k] != gifter[k]) {
                        isGifter = false;
                    }
                }
                if (isGifter) {
                    list.Add(i + 1);    // Aunts are indexed 1-based
                }
            }
            
            // List all gifters
            string s = "";
            foreach (int n in list) {
                s += $"{n} ";
            }
            return s;
         }

        public override string SolvePart2 () {
            // Store data about the gifter, given in problem
            Dictionary<string,int> gifter = ReadDictionary("children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1");
            
            // Import data about the aunts and compare to the gifter
            var list = new List<int>();
            for (int i=0; i<Input.Length; i++) {
                Dictionary<string, int> aunt = ReadDictionary(Input[i].Split(':', 2)[1]);
                bool isGifter = true;
                foreach (string k in aunt.Keys) {
                    if (k == "cats" || k == "trees") {
                        if (aunt[k] <= gifter[k]) {
                            isGifter = false;
                        }
                    }
                    else if (k == "pomeranians" || k == "goldfish") {
                        if (aunt[k] >= gifter[k]) {
                            isGifter = false;
                        }
                    }
                    else if (aunt[k] != gifter[k]) {
                        isGifter = false;
                    }
                }
                if (isGifter) {
                    list.Add(i + 1);    // Aunts are indexed 1-based
                }
            }
            
            // List all gifters
            string s = "";
            foreach (int n in list) {
                s += $"{n} ";
            }
            return s;
        }

        // Reads a Dictionary from a single line of data, no sanitation / safety checks
        private static Dictionary<string,int> ReadDictionary (string line) {
            var dict = new Dictionary<string,int>();
            string[] data = line.Split(',');
            foreach (var item in data) {
                string[] kvPair = item.Split(':');
                dict.Add(kvPair[0].Trim(), Int32.Parse(kvPair[1]));
            }
            return dict;
        }
    }
}