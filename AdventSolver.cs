
namespace AdventOfCode {
    public class AdventSolver {
        static void Main (string[] args) {
            // TODO: use args to determine year/day to solve
            //Solution day1 = new Y2015.Day01();
            Solution day = new Y2015.Day02();
            System.Console.WriteLine(day.Solve());
        }
    }
}