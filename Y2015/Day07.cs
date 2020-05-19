using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2015 {
    class Day07 : Solution {
        public Day07 () : base(7, 2015, "Some Assembly Required") {
        }

        public override string SolvePart1 () {
            Dictionary<string,Wire> wires = new Dictionary<string, Wire>();

            // First sweep: only add the wires
            foreach (string line in Input) {
                string name = line.Split(' ')[^1];
                wires.Add(name, new Wire(name));    // Assuming no duplicate wires!
            }

            // Second sweep: create and link the input
            foreach (string line in Input) {
                string[] definition = line.Split(' ');
                if (definition[0] == "NOT") {
                    Node wire = GetNode(definition[1]);
                    Node gate = new Gate("NOT", wire, null);
                    wires[definition[^1]].Input = gate;
                }
                else if (definition.Length == 5) {
                    // One of the other gates
                    Node left = GetNode(definition[0]);
                    Node right = GetNode(definition[2]);
                    Node gate = new Gate(definition[1], left, right);
                    wires[definition[^1]].Input = gate;
                }
                else if (definition.Length == 3) {
                    Node signal = GetNode(definition[0]);
                    wires[definition[^1]].Input = signal;
                }
                else {
                    throw new Exception($"Could not interpret the line: {line}");
                }
            }

            // Question asks for wire a:
            return wires["a"].Read().ToString();

            // Returns a node based on label, assuming all the wires have been identified
            // and if it is not a known wire, it must be numerical
            Node GetNode (string label) {
                if (wires.ContainsKey(label)) {
                    return wires[label];
                }
                else {  // Not one of the wires, assume a constant signal.
                    return new ConstantSignal(UInt16.Parse(label));
                }
            }
        }

        // public override string SolvePart2 () {
        // }
    }

    // Base class for the various wires, signals, and gates
    abstract class Node {
        protected ushort? cache = null;
        internal abstract ushort? Read();
    }

    // Represents an (output) wire
    class Wire : Node {
        internal Node Input { get; set; }
        internal string Name { get; }
        internal Wire(string name) {
            this.Name = name;
        }
        internal override ushort? Read() => cache ?? (cache = Input.Read());
    }

    // Represents an input signal
    class ConstantSignal : Node {
        ushort val;
        internal ConstantSignal(ushort val) {
            this.val = val;
        }
        internal override ushort? Read() => this.val;
    }

    // Represents any of the logic gates
    class Gate : Node {
        internal enum GateType {
            AND,
            OR,
            LSHIFT,
            RSHIFT,
            NOT
        }

        internal GateType GType { get; }
        private Node left, right;

        Gate(GateType type, Node left, Node right) {
            this.GType = type;
            this.left = left;
            this.right = right; // Should be null if type == NOT
        }
        internal Gate(string typeName, Node left, Node right) : this(GateTypeFromName(typeName), left, right) {}

        internal static GateType GateTypeFromName (string typeName) {
            switch (typeName) {
                case "AND":    return GateType.AND;
                case "OR":     return GateType.OR;
                case "LSHIFT": return GateType.LSHIFT;
                case "RSHIFT": return GateType.RSHIFT;
                case "NOT":    return GateType.NOT;
                default: throw new System.Exception($"Invalid GateType: {typeName}");
            }
        }

        internal override ushort? Read () {
            if (cache == null) {
                int? l = left.Read();
                int? r = right?.Read();
                switch (GType) {
                    case GateType.AND: cache = (ushort) (l & r); break;
                    case GateType.OR: cache = (ushort) (l | r); break;
                    case GateType.LSHIFT: cache = (ushort) (l << r); break;
                    case GateType.RSHIFT: cache = (ushort) (l >> r); break;
                    case GateType.NOT: cache = (ushort) (~l); break;
                    default: throw new System.Exception($"Invalid GateType: {GType}");
                }
            }
            return cache;
        }
    }
}