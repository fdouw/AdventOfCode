
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

        // public override string SolvePart2 () {
        // }
    }
}