using System;
using System.Linq;
using System.Text.RegularExpressions;

using System.Text.Json;

namespace AdventOfCode.Y2015 {
    class Day12 : Solution {
        public Day12 () : base(12, 2015, "JSAbacusFramework.io") {
        }

        public override string SolvePart1 () {
            // Assuming only integers
            return new Regex("-?[0-9]+").Matches(Input[0]).Sum(m => Int32.Parse(m.Value)).ToString();
        }

        public override string SolvePart2 () {
            JsonDocument doc = JsonDocument.Parse(Input[0]);
            int sum = SumNumbers(doc.RootElement);
            return $"{sum}";
        }

        private static int SumNumbers (JsonElement element) {
            // Recursively sums numbers in a Json subtree
            if (element.ValueKind == JsonValueKind.Number) {
                System.Console.WriteLine("Parse number-leaf, probably shouldn't happen");
                return element.GetInt32();
            }
            else if (element.ValueKind == JsonValueKind.Array) {
                int sum = 0;
                foreach (JsonElement child in element.EnumerateArray()) {
                    if (child.ValueKind == JsonValueKind.Object || child.ValueKind == JsonValueKind.Array) {
                        sum += SumNumbers(child);
                    }
                    else if (child.ValueKind == JsonValueKind.Number) {
                        sum += child.GetInt32();
                    }
                }
                return sum;
            }
            else if (element.ValueKind == JsonValueKind.Object) {
                int sum = 0;
                foreach (JsonProperty property in element.EnumerateObject()) {
                    JsonElement child = property.Value;
                    if (child.ValueKind == JsonValueKind.String) {
                        if (child.GetString() == "red") {
                            return 0;   // Ignore object and children
                        }
                    }
                    else if (child.ValueKind == JsonValueKind.Object || child.ValueKind == JsonValueKind.Array) {
                        // Subtree: recurse
                        sum += SumNumbers(child);
                    }
                    else if (child.ValueKind == JsonValueKind.Number) {
                        sum += child.GetInt32();
                    }
                }
                return sum;
            }
            // No subtree or number found:
            System.Console.WriteLine("Called on a non-number leaf");
            return 0;
        }
    }
}