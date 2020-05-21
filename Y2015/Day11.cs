using System.Linq;

namespace AdventOfCode.Y2015 {
    class Day11 : Solution {
        private char? prevChar;
        private char nextChar;
        private int charCount;
        
        public Day11 () : base(11, 2015, "Corporate Policy") {
        }

        public override string SolvePart1 () {
            return NextPassword(Input[0]);
        }

        public override string SolvePart2 () {
            return NextPassword(NextPassword(Input[0]));
        }

        // Generate the next valid password, given the current password
        private string NextPassword (string password) {
            while (true) {
                password = IncrementPassword(password);
                bool hasIncrease = false;
                int countPairs = 0;
                Reset();
                foreach (char c in password) {
                    hasIncrease = hasIncrease || TestIncrease(c);
                    if (TestPair(c)) {
                        countPairs++;
                    }
                    if (hasIncrease && countPairs >= 2) {
                        return password;
                    }
                }
            }
        }

        private void Reset () {
            charCount = 0;
            prevChar = null;
        }

        private bool TestPair (char c) {
            if (prevChar == null) {
                // First character
                prevChar = c;
            }
            else if (c == prevChar) {
                prevChar = null;    // Reset, so we won't find overlap
                return true;
            }
            else {
                // No match, start looking for pair of <c>
                prevChar = c;
            }
            return false;
        }

        // Find a strictly increasing sequence of length 3
        // NB: stops working correctly after finding a sequence, until reset
        private bool TestIncrease (char c) {
            if (charCount == 0) {
                // First character
                charCount++;
                nextChar = (char)(c + 1);
            }
            else if (c == nextChar) {
                charCount++;
                nextChar = (char)(c + 1);
                if (charCount == 3) {
                    return true;
                }
            }
            else {
                // Failed match, start again
                charCount = 1;
                nextChar = (char)(c + 1);
            }
            return false;
        }

        private static string IncrementPassword (string oldPass) {
            char[] letters = oldPass.ToCharArray();
            for (int i=letters.Length-1; i>=0; i--) {
                letters[i] = (char) (letters[i] + 1);
                if (letters[i] > 'z') {
                    letters[i] = 'a';
                }
                else {  // No carry
                    break;
                }
            }
            return new string(letters);
        }
    }
}