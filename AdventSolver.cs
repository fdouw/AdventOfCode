using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode {
    public class AdventSolver {
        private Assembly assembly;
        private Type solutionType;

        AdventSolver () {
            assembly = Assembly.GetExecutingAssembly();
            solutionType = assembly.GetType("AdventOfCode.Solution");
        }

        Solution GetLatestSolution () {
            var dayType = assembly.GetTypes()
                                   .Where(t => t.BaseType == solutionType)
                                   .OrderBy(t => t.FullName)
                                   .Last();
            return (Solution) Activator.CreateInstance(dayType);
        }

        IEnumerable<Solution> GetSolutions (int year) {
            var dayTypes = assembly.GetTypes()
                                   .Where(t => t.Namespace == $"AdventOfCode.Y{year}" && t.BaseType == solutionType)
                                   .OrderBy(t => t.Name);
            foreach (Type dayType in dayTypes) {
                yield return (Solution) Activator.CreateInstance(dayType);
            }
        }

        Solution GetSolution (int year, int day) {
            Type dayType = assembly.GetType($"AdventOfCode.Y{year}.Day{day,2}");
            if (dayType != null) {
                return (Solution)Activator.CreateInstance(dayType);
            }
            else {
                return null;
            }
        }

        static void Main (string[] args) {
            AdventSolver solver = new AdventSolver();
            if (args.Length == 2) {
                // Assume year and day
                int year = getYear();
                int day = Int32.Parse(args[1]);
                
                Solution sol = solver.GetSolution(year, day);
                System.Console.WriteLine(sol?.Solve() ?? $"No solutions found for {year}-{day}");
            }
            else if (args.Length == 1) {
                // Assume only year
                // Try to solve all this year's problems
                foreach (Solution sol in solver.GetSolutions(getYear())) {
                    System.Console.WriteLine(sol.Solve());
                }
            }
            else {
                // Find the last problem available and solve it
                Solution sol = solver.GetLatestSolution();
                System.Console.WriteLine(sol?.Solve() ?? "No solutions found");
            }

            int getYear () {
                int y = Int32.Parse(args[0]);
                return (y < 100) ? y + 2000 : y;
            }
        }
    }
}