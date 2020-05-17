using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day03 : Solution {
        HashSet<(int,int)> houses = new HashSet<(int, int)>();

        public Day03 () : base(3, 2015, "Perfectly Spherical Houses in a Vacuum") {
        }

        public override string SolvePart1 () {
            (int X,int Y) house = (X: 0, Y: 0);
            houses.Add(house);
            for (int i=0; i<Input[0].Length; i++) {
                switch (Input[0][i]) {
                    case '^': house = (house.X, house.Y - 1); break;
                    case '>': house = (house.X + 1, house.Y); break;
                    case '<': house = (house.X - 1, house.Y); break;
                    case 'v': house = (house.X, house.Y + 1); break;
                    default: throw new System.Exception($"Invalid direction: {Input[0][i]}");
                }
                houses.Add(house);
            }
            return $"{houses.Count}";
        }

        public override string SolvePart2 () {
            return "Not yet implemented.";
        }
    }
}