using System;
using System.Linq;

namespace AdventOfCode.Y2015 {
    class Day02 : Solution {
        public Day02 () : base(2, 2015, "I Was Told There Would Be No Math") {
        }

        public override string SolvePart1 () {
            long countPaper(long l, long m, long n) {
                long a = l * m;
                long b = l * n;
                long c = m * n;
                long d = (a < b) ? a : b;
                d = (d < c) ? d : c;
                return 2 * (a + b + c) + d;
            }
            return Input.Select(x => x.Split('x').Select(c => Int64.Parse(c)).ToArray())
                        .Sum(a => countPaper(a[0], a[1], a[2])).ToString();
        }

        public override string SolvePart2 () {
            return Input.Select(x => x.Split('x').Select(c => Int64.Parse(c)).OrderBy(n => n))
                        .Sum(x => x.Aggregate((acc,n) => acc * n) + 2 * x.Take(2).Sum()).ToString();
        }
    }
}