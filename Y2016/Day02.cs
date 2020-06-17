using System.Collections.Generic;

namespace AdventOfCode.Y2016 {
    class Day02 : Solution {
        public Day02 () : base(2, 2016, "Bathroom Security") {
        }

        public override string SolvePart1 () {
            int[] numbers = new int[Input.Length];
            int current = 5;
            for (int i = 0; i < Input.Length; i++) {
                foreach (char c in Input[i]) {
                    switch (c) {
                        case 'U':
                            if (current > 3) {
                                current -= 3;
                            }
                            break;
                        case 'R':
                            if (current % 3 != 0) {
                                current += 1;
                            }
                            break;
                        case 'D':
                            if (current < 7) {
                                current += 3;
                            }
                            break;
                        case 'L':
                            if ((current - 1) % 3 != 0) {
                                current -= 1;
                            }
                            break;
                    }
                }
                numbers[i] = current;
            }
            return string.Join(null, numbers);
        }

        public override string SolvePart2 () {
            // Use a simple DFA: for each position, define the possible next positions
            Dictionary<char,string> pad = new Dictionary<char,string>();
            pad.Add('1', "1131");
            pad.Add('2', "2362");
            pad.Add('3', "1472");
            pad.Add('4', "4483");
            pad.Add('5', "5655");
            pad.Add('6', "27A5");
            pad.Add('7', "38B6");
            pad.Add('8', "49C7");
            pad.Add('9', "9998");
            pad.Add('A', "6BAA");
            pad.Add('B', "7CDA");
            pad.Add('C', "8CCB");
            pad.Add('D', "BDDD");
            char[] numbers = new char[Input.Length];
            char current = '5';
            for (int i = 0; i < Input.Length; i++) {
                foreach (char c in Input[i]) {
                    switch (c) {
                        case 'U':
                            current = pad[current][0];
                            break;
                        case 'R':
                            current = pad[current][1];
                            break;
                        case 'D':
                            current = pad[current][2];
                            break;
                        case 'L':
                            current = pad[current][3];
                            break;
                    }
                }
                numbers[i] = current;
            }
            return string.Join(null, numbers);
        }
    }
}