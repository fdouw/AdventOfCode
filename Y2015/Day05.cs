using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day05 : Solution {
        public Day05 () : base(5, 2015, "Doesn't He Have Intern-Elves For This?") {
        }

        public override string SolvePart1 () {
            int niceCount = 0;

            foreach (string word in Input) {
                char prev = word[0];
                int vowelCount = isVowel(prev) ? 1 : 0;
                bool duplicate = false;
                bool forbidden = false;

                for (int i = 1; i < word.Length; i++) {
                    if (isVowel(word[i])) vowelCount++;
                    duplicate = duplicate || word[i] == prev;   // Only need 1 duplicate: set to true if found
                    if ((prev == 'a' || prev == 'c' || prev == 'p' || prev == 'x') && word[i] == (char)(prev + 1)) {
                        // Pattern: forbidden combinations are to consequtive numbers
                        forbidden = true;
                        break;
                    }
                    prev = word[i];
                }

                if (vowelCount >= 3 && duplicate && !forbidden) {
                    niceCount++;
                }
            }
            return $"{niceCount}";

            bool isVowel (char c) => (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u');
        }

        public override string SolvePart2 () {
            int niceCount = 0;
            Dictionary<string,int> pairDict = new Dictionary<string, int>();
            foreach (string word in Input) {
                bool repeatPair = false;
                bool repeatChar = false;
                string pair;
                pairDict.Clear();
                pairDict.Add($"{word[0]}{word[1]}", 0); // Skipped in the loop
                for (int i = 2; i < word.Length; i++) {
                    if (word[i] == word[i-2]) {
                        repeatChar = true;
                        if (repeatPair) break;  // Found both conditions, so we can stop
                    }
                    if (!repeatPair) {  // Avoid needless checks
                        pair = $"{word[i-1]}{word[i]}";
                        if (pairDict.ContainsKey(pair)) {
                            if (pairDict[pair] < i-2) { // Ignore overlap
                                repeatPair = true;
                                if (repeatChar) break;  // Found both conditions, so we can stop
                            }
                        }
                        else { // !pairDict.ContainsKey(pair)
                            pairDict.Add(pair, i-1);
                        }
                    }
                }
                if (repeatChar && repeatPair) {
                    niceCount++;
                }
            }
            return $"{niceCount}";
        }
    }
}