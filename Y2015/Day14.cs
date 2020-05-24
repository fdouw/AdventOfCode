using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day14 : Solution {
        private class Reindeer {
            internal int speed;
            internal int timeFly;
            internal int timeRest;
            internal int points;
            internal int distance;
            internal Reindeer (int s, int f, int r, int p=0, int d=0) {
                speed = s;
                timeFly = f;
                timeRest = r;
                points = p;
                distance = d;
            }
            /* Return true iff reindeer is travelling in the time from sec to sec + 1 */
            internal bool IsTravelling (int sec) {
                int t = timeFly + timeRest;
                return ((sec % t) < timeFly);
            }
        }

        public Day14 () : base(14, 2015, "Reindeer Olympics") {
        }

        public override string SolvePart1 () {
            int timeLimit = 2503;   // Given in description
            int distance = 0;
            foreach (string line in Input) {
                string[] data = line.Split(' ');
                int v = Int32.Parse(data[3]);       // Travelling speed
                int t_v = Int32.Parse(data[6]);     // Travelling time
                int t_r = Int32.Parse(data[^2]);    // Rest time
                int d = v * t_v * (timeLimit / (t_v + t_r));
                d += min(t_v, timeLimit % (t_v + t_r)) * v;
                if (d > distance) {
                    distance = d;
                }
            }

            return $"{distance}";
        }

        public override string SolvePart2 () {
            int timeLimit = 2503;   // Given in description
            // Read the data for the reindeer
            var reindeer = new List<Reindeer>(Input.Length);
            foreach (string line in Input) {
                string[] data = line.Split(' ');
                int v = Int32.Parse(data[3]);       // Travelling speed
                int tf = Int32.Parse(data[6]);     // Travelling time
                int tr = Int32.Parse(data[^2]);   // Rest time
                Reindeer r = new Reindeer(v, tf, tr);
                reindeer.Add(r);
            }
            // Simply simulate the race
            int maxDist = 0;
            for (int t=0; t<timeLimit; t++) {
                // First update the stats and find the current longest distance...
                foreach (Reindeer r in reindeer) { 
                    if (r.IsTravelling(t)) {
                        r.distance += r.speed;
                    }
                    if (r.distance > maxDist) {
                        maxDist = r.distance;
                    }
                }
                // ...then award the reindeer in the lead.
                foreach (Reindeer r in reindeer) {
                    if (r.distance == maxDist) {
                        r.points++;
                    }
                }
            }
            int maxPoints = 0;
            foreach (Reindeer r in reindeer) {
                if (r.points > maxPoints) {
                    maxPoints = r.points;
                }
            }
            return $"{maxPoints}";
        }

        static int min (int a, int b) => (a < b) ? a : b;
    }
}