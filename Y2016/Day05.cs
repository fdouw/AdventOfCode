using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Y2016 {
    class Day05 : Solution {
        public Day05 () : base(5, 2016, "How About a Nice Game of Chess") {
        }

        public override string SolvePart1 () {
            string prefix = Input[0];
            string password = "";
            using (MD5 md5 = MD5.Create()) {
                int index = 0;
                byte[] hash;
                for (int found = 0; found < 8; ) {
                    hash = md5.ComputeHash(Encoding.UTF8.GetBytes(prefix + index));
                    index++;
                    if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0) {
                        password += "0123456789abcdef"[hash[2]];
                        found++;
                    }
                }
            }
            return password;
        }

        public override string SolvePart2 () {
            string prefix = Input[0];
            char[] password = "________".ToCharArray();
            using (MD5 md5 = MD5.Create()) {
                int index = 0;
                byte[] hash;
                for (int found = 0; found < 8; ) {
                    hash = md5.ComputeHash(Encoding.UTF8.GetBytes(prefix + index));
                    index++;
                    if (hash[0] == 0 && hash[1] == 0 && hash[2] < 8 && password[hash[2]] == '_') {
                        password[hash[2]] = "0123456789abcdef"[hash[3] >> 4];
                        found++;
                    }
                }
            }
            return new string(password);
        }
    }
}