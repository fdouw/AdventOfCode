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

            // Try each vertex as a starting point.
            // Build a Hamiltonian path by connecting the shortest available edge each time.
            // Then pray that the shortest of these is in fact the shortest Hamiltonian path.
            int minDist = Int32.MaxValue;
            foreach (string v in graph.GetVertices()) {
                System.Console.WriteLine($"Testing starting vertex {v}");
                HashSet<string> marked = new HashSet<string>();
                string curVertex = v;
                string prevVertex = null;
                int curDist = 0;

                // Walk through the graph, starting at v
                // Implicitly assuming that graph has a Hamiltonian path starting at v
                while (curVertex != prevVertex) {
                    System.Console.WriteLine($"\tCurrent vertex: {curVertex}");
                    prevVertex = curVertex;
                    foreach (string w in graph.GetNeighboursSorted(curVertex)) {
                        if (!marked.Contains(w)) {
                            marked.Add(w);
                            curDist += graph.GetDistance(curVertex, w);
                            curVertex = w;
                            break;
                        }
                    }
                }
                if (marked.Count == graph.Count) {  // Make sure the path is Hamiltonian
                    if (curDist < minDist) {
                        minDist = curDist;
                    }
                }
            }
            return $"{minDist}";
        }

        // public override string SolvePart2 () {
        // }
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