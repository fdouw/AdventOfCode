using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day09 : Solution {
        public Day09 () : base(9, 2015, "All in a Single Night") {
        }

        public override string SolvePart1 () {
            WeightedGraph graph = new WeightedGraph();

            // Build the graph
            foreach (string line in Input) {
                string[] data = line.Split(' ');
                graph.AddEdge(data[0], data[2], Int32.Parse(data[^1]));
            }

            // Use DFS to find all Hamiltonian paths, remember the shortest length.
            var seen = new HashSet<string>(graph.Count);
            int minDist = Int32.MaxValue;
            foreach (string v in graph.GetVertices()) {
                minDist = DfsHamiltonian(graph, v, seen, 0, minDist);
            }

            return $"{minDist}";
        }

        // public override string SolvePart2 () {
        // }

        /* Recursively looks for a Hamiltonian path using DFS and returns the minimal path length.
         * graph: WeightedGraph to search through
         * v: current vertex, from which to search further
         * seen: set of vertices in the path up to, but excluding, v
         * dist: length of the path up to v
         * minDist: currently the best guess for the shortest total path length
         * returns: updated guess for minDist
         */
        private static int DfsHamiltonian (WeightedGraph graph, string v, HashSet<string> seen, int dist, int minDist) {
            seen.Add(v);
            if (dist >= minDist) {
                // No point in looking further
            }
            else if (seen.Count == graph.Count) {
                // End of the line: we've passed all vertices!
                minDist = (dist < minDist) ? dist : minDist;
            }
            else {
                // Keep looking
                foreach (string w in graph.GetNeighbours(v)) {
                    if (!seen.Contains(w)) {
                        minDist = DfsHamiltonian(graph, w, seen, dist + graph.GetDistance(v,w), minDist);
                    }
                }
            }
            seen.Remove(v);
            return minDist;
        }
    }

    class WeightedGraph {
        internal int Count { get => graph.Count; }  // Number of vertices
        private Dictionary<string,Dictionary<string,int>> graph = new Dictionary<string,Dictionary<string,int>>();
        private Dictionary<string,List<string>> orderedNeighbours = new Dictionary<string, List<string>>();

        // Adds or updates an edge
        internal void AddEdge (string v, string w, int dist) {
            // Store the v->w direction
            if (!graph.ContainsKey(v)) {
                graph.Add(v, new Dictionary<string, int>());
            }
            if (!graph[v].ContainsKey(w)) {
                graph[v].Add(w, dist);
            }
            else {
                graph[v][w] = dist;
            }
            // Store the w->v direction
            if (!graph.ContainsKey(w)) {
                graph.Add(w, new Dictionary<string, int>());
            }
            if (!graph[w].ContainsKey(v)) {
                graph[w].Add(v, dist);
            }
            else {
                graph[w][v] = dist;
            }
        }

        // Distance between v and w. No safety checks!
        internal int GetDistance(string v, string w) => graph[v][w];

        internal IEnumerable<string> GetVertices () => graph.Keys;
        internal IEnumerable<string> GetNeighbours (string v) => graph[v].Keys;
        internal IEnumerable<string> GetNeighboursSorted (string v) {
            if (!orderedNeighbours.ContainsKey(v)) {
                var l = from nb in graph[v] orderby nb.Value ascending select nb.Key;
                orderedNeighbours.Add(v, l.ToList());
            }
            return orderedNeighbours[v];
        }
    }
}