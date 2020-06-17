
namespace AdventOfCode.Y2016 {
    class Day06 : Solution {
        public Day06 () : base(6, 2016, "Signals and Noise") {
        }

        public override string SolvePart1 () => Decode(true);

        public override string SolvePart2 () => Decode(false);

        private string Decode (bool findMax) {
            // Assume all lines in Input have the same length
            // Simply tally the characters in each position, and determine the maximum
            string decoded = "";
            for (int col = 0; col < Input[0].Length; col++) {
                int[] count = new int[26];
                foreach (string line in Input) {
                    count[line[col] - 'a']++;
                }
                int cur = 0;
                for (int i = 1; i < 26; i++) {
                    if ((findMax && count[i] > count[cur]) || (!findMax && count[i] < count[cur])) {
                        cur = i;
                    }
                }
                decoded += (char)(cur + 'a');
            }
            return decoded;
        }
    }
}