using System.IO;

namespace AdventOfCode {
    abstract class Solution {
        protected string[] Input { get; }
        public int Day { get; }
        public int Year { get; }
        public string Title { get;}

        protected Solution (int day, int year, string title) : this(day, year, title, $"Y{year}/input{day:00}") {
        }

        protected Solution (int day, int year, string title, string inputPath) {
            this.Day = day;
            this.Year = year;
            this.Title = title;
            this.Input = File.ReadAllLines(inputPath);
        }
        
        public string Solve () => $"---- {this.Year}-{this.Day}: {this.Title} ----\n{this.SolvePart1()}\n{this.SolvePart2()}";
        public virtual string SolvePart1 () => "Not yet implemented.";
        public virtual string SolvePart2 () => "Not yet implemented.";
    }
}