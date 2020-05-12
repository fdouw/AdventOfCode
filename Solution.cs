using System.IO;

namespace AdventOfCode {
    abstract class Solution {
        protected string[] Input { get; }
        public int Day { get; }
        public int Year { get; }
        public string Title { get;}

        protected Solution (int day, int year, string title, string inputPath = "input") {
            this.Day = day;
            this.Year = year;
            this.Title = title;
            this.Input = File.ReadAllLines(inputPath);
        }
        
        public string Solve () => $"---- {this.Year}-{this.Day} ----\n{this.SolvePart1()}\n{this.SolvePart2()}";
        public abstract string SolvePart1 ();
        public abstract string SolvePart2 ();
    }
}