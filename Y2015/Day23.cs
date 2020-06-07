using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day23 : Solution {
        private Computer computer;
        public Day23 () : base(23, 2015, "Opening the Turing Lock") {
            computer = new Computer(Input);
        }

        public override string SolvePart1 () => $"{computer.Exec()}";

        public override string SolvePart2 () => $"{computer.Exec(1)}";
    }

    class Computer {
        private Dictionary<string,int> register = new Dictionary<string, int>(2);
        private List<string[]> code = new List<string[]>();

        internal Computer (string[] program) {
            register.Add("a", 0);
            register.Add("b", 0);

            foreach (string line in program) {
                code.Add(line.Replace(",", "").Split(" "));
            }
        }

        internal int Exec (int a = 0, int b = 0) {
            register["a"] = a;
            register["b"] = b;
            int idx = 0;
            while (idx >= 0 && idx < code.Count) {
                string r = code[idx][1];
                switch (code[idx][0]) {
                    case "hlf": register[r] /= 2; idx++; break;
                    case "tpl": register[r] *= 3; idx++; break;
                    case "inc": register[r]++; idx++; break;
                    case "jmp": idx += Int32.Parse(r); break;
                    case "jie": idx += (register[r] % 2 == 0) ? Int32.Parse(code[idx][2]) : 1; break;
                    case "jio": idx += (register[r] == 1) ? Int32.Parse(code[idx][2]) : 1; break;
                    default: throw new Exception($"Not a valid instruction: '{code[idx][0]}' at idx {idx}");
                }
            }
            return register["b"];
        }
    }
}