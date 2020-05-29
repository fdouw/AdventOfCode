using System;

namespace AdventOfCode.Y2015 {
    class Day20 : Solution {
        public Day20 () : base(20, 2015, "Infinite Elves and Infinite Houses") {
        }

        public override string SolvePart1 () {
            int limit = Int32.Parse(Input[0]);
            limit /= 10;    // Scale the whole problem down by a factor 10

            int boundary = limit;
            int[] house = new int[limit];
            for (int n = 1; n < limit; n++) {
                for (int i = n; i < boundary; i += n) {
                    house[i] += n;
                    if (house[i] >= limit) {
                        // Beyond the limit, so houses beyond this point will never be the solution
                        boundary = i;
                        break;
                    }
                }
            }

            return $"{boundary}";
        }

        public override string SolvePart2 () {
            int limit = Int32.Parse(Input[0]);
            //limit /= 10;    // Scale the whole problem down by a factor 10

            int boundary = limit;
            int[] house = new int[boundary];
            int npresent = 0;
            for (int n = 1; n < boundary; n++) {
                int nindex = 0;
                npresent += 11;
                for (int i = 0; i < 50 && nindex < boundary; i++) {
                    nindex += n;
                    house[nindex] += npresent;
                    if (house[nindex] >= limit) {
                        // Beyond the limit, so houses beyond this point will never be the solution
                        boundary = nindex;
                        break;
                    }
                }
            }

            return $"{boundary}";
        }
    }
}