using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Y2015 {
    class Day04 : Solution {
        private int SEARCH_LIMIT = 1_000_000_000;
        private MD5 Hasher = MD5.Create();

        public Day04 () : base(4, 2015, "The Ideal Stocking Stuffer") {
        }

        public override string SolvePart1 () {
            byte[] inputBytes, hashBytes;
            string key = Input[0];
            
            for (int n = 0; n < SEARCH_LIMIT; n++) {
                // Generate the hash
                inputBytes = Encoding.ASCII.GetBytes(key + n);
                hashBytes = Hasher.ComputeHash(inputBytes);
                
                // No need to completely transform the hash: if the first 5 are 0, we are good.
                // Otherwise skip to the next one.
                if (hashBytes[0] == 0 && hashBytes[1] == 0 && hashBytes[2] < 16) {
                    return $"{n}";
                }
            }
            return $"No solution found below {SEARCH_LIMIT}";
        }

        public override string SolvePart2 () {
            byte[] inputBytes, hashBytes;
            string key = Input[0];
            
            for (int n = 0; n < SEARCH_LIMIT; n++) {
                // Generate the hash
                inputBytes = Encoding.ASCII.GetBytes(key + n);
                hashBytes = Hasher.ComputeHash(inputBytes);
                
                // No need to completely transform the hash: if the first 5 are 0, we are good.
                // Otherwise skip to the next one.
                if (hashBytes[0] == 0 && hashBytes[1] == 0 && hashBytes[2] == 0) {
                    return $"{n}";
                }
            }
            return $"No solution found below {SEARCH_LIMIT}";
        }
    }
}