using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day03 : Solution {

        public Day03 () : base(3, 2015, "Perfectly Spherical Houses in a Vacuum") {
        }

        public override string SolvePart1 () {
            HashSet<(int,int)> houses = new HashSet<(int, int)>();
            (int X,int Y) house = (X: 0, Y: 0);
            houses.Add(house);
            for (int i=0; i<Input[0].Length; i++) {
                house = Move(house, Input[0][i]);
                houses.Add(house);
            }
            return $"{houses.Count}";
        }

        public override string SolvePart2 () {
            HashSet<(int,int)> houses = new HashSet<(int, int)>();
            (int X,int Y) santa = (X: 0, Y: 0);
            (int X,int Y) robot = (X: 0, Y: 0);
            houses.Add(santa);  // Robot is still the same
            for (int i=0; i<Input[0].Length; i+=2) {
                santa = Move(santa, Input[0][i]);
                houses.Add(santa);
            }
            for (int i=1; i<Input[0].Length; i+=2) {
                robot = Move(robot, Input[0][i]);
                houses.Add(robot);
            }
            return $"{houses.Count}";
        }

        private (int X, int Y) Move ((int X, int Y) oldPos, char dir) {
            switch (dir) {
                case '^': return (oldPos.X, oldPos.Y - 1);
                case '>': return (oldPos.X + 1, oldPos.Y);
                case '<': return (oldPos.X - 1, oldPos.Y);
                case 'v': return (oldPos.X, oldPos.Y + 1);
                default: throw new System.Exception($"Invalid direction: {dir}");
            }
        }
    }
}