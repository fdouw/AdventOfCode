
namespace AdventOfCode.Y2015 {
    class Day18 : Solution {
        public Day18 () : base(18, 2015, "Like a GIF For Your Yard") {
        }

        public override string SolvePart1 () {
            LightGrid grid = new LightGrid(Input);
            grid.Evolve(100);
            return $"{grid.LightsOn()}";
        }

        public override string SolvePart2 () {
            LightGrid grid = new LightGrid(Input, true);
            grid.Evolve(100, true);
            return $"{grid.LightsOn()}";
        }
    }

    class LightGrid {
        private bool[,] grid;

        internal int Width { get; }
        internal int Height { get; }

        internal LightGrid (int w, int h) {
            grid = new bool[w, h];
            Width = w;
            Height = h;
        }

        /* Creates a LightGrid from data. */
        internal LightGrid (string[] data, bool cornersOn = false) {
            grid = new bool[data[0].Length, data.Length];
            Width = data[0].Length;
            Height = data.Length;
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    grid[x,y] = (data[y][x] == '#');
                }
            }
            if (cornersOn) {
                grid[0,0] = grid[0,Height-1] = grid[Width-1,0] = grid[Width-1,Height-1] = true;
            }
        }

        // Returns the number of lights that are currently on
        internal int LightsOn () {
            int count = 0;
            foreach (bool light in grid) {
                if (light) {
                    count++;
                }
            }
            return count;
        }

        internal void Print () {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    System.Console.Write(grid[x,y] ? '#' : '.');
                }
                System.Console.WriteLine();
            }
        }

        /* Run <steps> timesteps of the simulation */
        internal void Evolve (int steps, bool cornersOn = false) {
            bool[,] next = new bool[Width,Height];
            bool[,] tmp;
            for (int t = 0; t < steps; t++) {
                for (int y = 0; y < Height; y++) {
                    for (int x = 0; x < Width; x++) {
                        int nb = 0;
                        for (int dx = -1; dx < 2; dx++) {
                            for (int dy = -1; dy < 2; dy++) {
                                if (dx != 0 || dy != 0) {
                                    nb += Get(x + dx, y + dy);
                                }
                            }
                        }
                        if (grid[x, y]) {
                            next[x, y] = (nb == 2 || nb == 3);
                        }
                        else {
                            next[x, y] = (nb == 3);
                        }
                    }
                }
                // Swap next to grid after each timestep
                // we don't need to preserve grid, but this saves allocations
                tmp = grid;
                grid = next;
                if (cornersOn) {
                    grid[0,0] = grid[0,Height-1] = grid[Width-1,0] = grid[Width-1,Height-1] = true;
                }
                next = tmp;
            }
        }

        // Returns the status of the light, as an int.
        // Off the grid means off.
        private int Get (int x, int y) {
            if (x < 0 || y < 0 || x >= Width || y >= Height) {
                return 0;
            }
            else {
                return grid[x, y] ? 1 : 0;
            }
        }
    }
}