using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day17 : Solution {
        public Day17 () : base(17, 2015, "No Such Thing as Too Much") {
        }

        public override string SolvePart1 () {
            int[] jars = Input.Select(s => Int32.Parse(s)).ToArray();
            int targetVolume = 150;
            return $"{countCombinations(0, 0)}";

            int countCombinations (int curVolume, int startIndex) {
                if (curVolume > targetVolume) {
                    return 0;
                }
                else if (curVolume == targetVolume) {
                    return 1;
                }
                else {
                    int combis = 0;
                    for (int i = startIndex; i < jars.Length; i++) {
                        combis += countCombinations(curVolume + jars[i], i + 1);
                    }
                    return combis;
                }
            }
        }

        public override string SolvePart2 () {
            int[] jars = Input.Select(s => Int32.Parse(s)).ToArray();
            var targetVolume = 150;
            var combinations = new List<int>();
            countCombinations(0, 0, 0);

            // Each enty in <combinations> is a count of how many jars are used in a combination
            // GroupBy groups on this jar count, then the key in the IGrouping is the number of jars
            // used, the Count() gives the number of combinations that use this number of jars.
            int combiCount = combinations.GroupBy(n => n).OrderBy(g => g.Key).First().Count();
            return $"{combiCount}";

            void countCombinations (int curVolume, int startIndex, int jarsUsed) {
                if (curVolume == targetVolume) {
                    combinations.Add(jarsUsed);
                }
                else if (curVolume < targetVolume) {
                    for (int i = startIndex; i < jars.Length; i++) {
                        countCombinations(curVolume + jars[i], i + 1, jarsUsed + 1);
                    }
                }
            }
        }
    }
}