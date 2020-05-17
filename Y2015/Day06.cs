using System;
using System.Collections;

namespace AdventOfCode.Y2015 {
    class Day06 : Solution {
        public Day06 () : base(6, 2015, "Probably a Fire Hazard") {
        }

        public override string SolvePart1 () {
            BitArray lights = new BitArray(1_000_000);

            foreach (string line in Input) {
                string[] words = line.Split(' ');
                if (words[0] == "turn") {
                    bool targetValue = (words[1] == "on");  // Switch the whole patch on or off, depending on the command
                    var coords = GetCoords(words);
                    for (int y = coords.yStart; y <= coords.yEnd; y++) {
                        int Y = 1000 * y;
                        for (int x = coords.xStart; x <= coords.xEnd; x++) {
                            lights[Y + x] = targetValue;
                        }
                    }
                }
                else {  // words[0] == "toggle"
                    var coords = GetCoords(words);
                    for (int y = coords.yStart; y <= coords.yEnd; y++) {
                        int Y = 1000 * y;
                        for (int x = coords.xStart; x <= coords.xEnd; x++) {
                            lights[Y + x] ^= true;
                        }
                    }
                }
            }
            int lightsOn = 0;
            foreach (bool light in lights) {
                if (light) {
                    lightsOn++;
                }
            }
            return $"{lightsOn}";

            (int xStart, int yStart, int xEnd, int yEnd) GetCoords(string[] words) {
                string[] coord = words[^3].Split(',');
                int xStart = Int32.Parse(coord[0]);
                int yStart = Int32.Parse(coord[1]);
                coord = words[^1].Split(',');
                int xEnd = Int32.Parse(coord[0]);
                int yEnd = Int32.Parse(coord[1]);
                return (xStart, yStart, xEnd, yEnd);
            }
        }

        // public override string SolvePart2 () {
        // }
    }
}