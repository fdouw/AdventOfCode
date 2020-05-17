using System;
using System.Reflection;

namespace AdventOfCode {
    public class AdventSolver {
        static void Main (string[] args) {
            // Dynamically load the right solution.
            // TODO: implement safety checks!
            string year = (args.Length > 0) ? args[0] : "2015";
            string day = (args.Length > 1) ? args[1] : "01";
            Assembly assem = Assembly.GetExecutingAssembly();
            Type solType = assem.GetType($"AdventOfCode.Y{year}.Day{day}");
            if (solType != null) {
                Solution sol = (Solution)Activator.CreateInstance(solType);
                System.Console.WriteLine(sol.Solve());
            }
            else {
                System.Console.WriteLine($"Type 'AdventOfCode.Y{year}.Day{day}' not found!");
            }
        }
    }
}