using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day15 : Solution {
        public Day15 () : base(15, 2015, "Science for Hungry People") {
        }

        public override string SolvePart1 () {
            List<Ingredient> ingredients = GetIngredients();

            // Compute the highest combination
            int maxValue = 0;
            int spoonsAvailable = 100;  // Given in problem
            var list = new List<int>();
            foreach (List<int> weights in Partition(ingredients.Count, spoonsAvailable, list)) {
                Ingredient sum = weights[0] * ingredients[0];   // Assuming non-empty lists
                for (int i = 1; i < ingredients.Count; i++) {
                    sum += weights[i] * ingredients[i];
                }
                if (sum.Value > maxValue) {
                    maxValue = sum.Value;
                }
            }

            return $"{maxValue}";
        }

        public override string SolvePart2 () {
            List<Ingredient> ingredients = GetIngredients();

            // Compute the highest combination
            int maxValue = 0;
            int spoonsAvailable = 100;  // Given in problem
            int targetCalories = 500;   // Given in problem
            var list = new List<int>();
            foreach (List<int> weights in Partition(ingredients.Count, spoonsAvailable, list)) {
                Ingredient sum = weights[0] * ingredients[0];   // Assuming non-empty lists
                for (int i = 1; i < ingredients.Count; i++) {
                    sum += weights[i] * ingredients[i];
                }
                if (sum.Calories == targetCalories && sum.Value > maxValue) {
                    maxValue = sum.Value;
                }
            }

            return $"{maxValue}";
        }

        private List<Ingredient> GetIngredients () {
            var ingredients = new List<Ingredient>();
            foreach (string line in Input) {
                string[] data = line.Split(' ');
                // Assume data are in fixed fields, and all have : or , to remove, except calories
                string name = data[0].Substring(0, data[0].Length - 1);
                int capacity = Int32.Parse(data[2].Substring(0, data[2].Length - 1));
                int durability = Int32.Parse(data[4].Substring(0, data[4].Length - 1));
                int flavour = Int32.Parse(data[6].Substring(0, data[6].Length - 1));
                int texture = Int32.Parse(data[8].Substring(0, data[8].Length - 1));
                int calories = Int32.Parse(data[10]);
                ingredients.Add(new Ingredient(name, capacity, durability, flavour, texture, calories));
            }
            return ingredients;
        }

        /* Returns all the partitions of <limit> items into <nparts> parts. <list> is used as
         * storage during recursion. */
        private static IEnumerable<List<int>> Partition (int nparts, int limit, List<int> list) {
            if (list == null) {
                list = new List<int>();
            }
            if (nparts == 1) {
                // Tail end
                list.Add(limit);
                yield return list;
                list.Remove(limit);
            }
            else if (nparts > 0) {
                for (int i = 0; i <= limit; i++) {
                    list.Add(i);
                    foreach (var result in Partition(nparts - 1, limit - i, list)) {
                        yield return result;
                    }
                    list.Remove(i);
                }
            }
        }
    }

    class Ingredient {
        internal string Name { get; }
        internal int Capacity { get; }
        internal int Durability { get; }
        internal int Flavour { get; }
        internal int Texture { get; }
        internal int Calories { get; }
        internal int Value { get => max(Capacity, 0) * max(Durability, 0) * max(Flavour, 0) * max(Texture, 0); }

        internal Ingredient (string n, int cap, int dur, int flav, int text, int cal = 0) {
            Name = n;
            Capacity = cap;
            Durability = dur;
            Flavour = flav;
            Texture = text;
            Calories = cal;
        }

        public static Ingredient operator + (Ingredient a, Ingredient b) {
            return new Ingredient($"{a.Name},{b.Name}",
                                  a.Capacity + b.Capacity,
                                  a.Durability + b.Durability,
                                  a.Flavour + b.Flavour,
                                  a.Texture + b.Texture,
                                  a.Calories + b.Calories);
        }

        public static Ingredient operator * (int n, Ingredient ingredient) {
            return new Ingredient(ingredient.Name,
                                  n * ingredient.Capacity,
                                  n * ingredient.Durability,
                                  n * ingredient.Flavour,
                                  n * ingredient.Texture,
                                  n * ingredient.Calories);
        }

        private static int max (int a, int b) => (a > b) ? a : b;
    }
}