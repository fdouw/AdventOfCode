using System;

namespace AdventOfCode.Y2015 {
    class Day21 : Solution {
        Item[] weapons, armour, rings;
        int bossHitPts, bossDamage, bossArmour;

        public Day21 () : base(21, 2015, "RPG Simulator 20XX") {
            weapons = new Item[] {
                new Item( 8, 4, 0), // Dagger
                new Item(10, 5, 0), // Shortsword
                new Item(25, 6, 0), // Warhammer
                new Item(40, 7, 0), // Longsword
                new Item(74, 8, 0)  // Greataxe
            };
            armour = new Item[] {
                new Item(  0, 0, 0), // Emtpy: armour is optional
                new Item( 13, 0, 1), // Leather
                new Item( 31, 0, 2), // Chainmail
                new Item( 53, 0, 3), // Splintmail
                new Item( 75, 0, 4), // Bandedmail
                new Item(102, 0, 5)  // Platemail
            };
            rings = new Item[] {
                new Item(  0, 0, 0), // Emtpy: 0-2 rings
                new Item(  0, 0, 0), // Emtpy: 0-2 rings
                new Item( 25, 1, 0), // Damage +1
                new Item( 50, 2, 0), // Damage +2
                new Item(100, 3, 0), // Damage +3
                new Item( 20, 0, 1), // Defense +1
                new Item( 40, 0, 2), // Defense +2
                new Item( 80, 0, 3)  // Defense +3
            };

            // Read input
            bossHitPts = Int32.Parse(Input[0].Split(" ")[^1]);
            bossDamage = Int32.Parse(Input[1].Split(" ")[^1]);
            bossArmour = Int32.Parse(Input[2].Split(" ")[^1]);
        }

        public override string SolvePart1 () {
            // Brute force, as I don't know how to order this efficiently on cost
            int optimalCost = Int32.MaxValue;
            foreach (Item w in weapons) {
                foreach (Item a in armour) {
                    for (int i = 1; i < rings.Length; i++) {
                        for (int j = 0; j < i; j++) {
                            Item r1 = rings[i];
                            Item r2 = rings[j];
                            int damage = w.Damage + r1.Damage + r2.Damage;
                            int armour = a.Armour + r1.Armour + r2.Armour;

                            int offence = damage - bossArmour;
                            int defence = bossDamage - armour;

                            // Attacker always does at least 1 damage, which translates to <hitpoints> turns needed
                            int turnsNeeded = (offence > 0) ? bossHitPts / offence: bossHitPts;
                            int turnsAlive = (defence > 0) ? 100 / defence : 100;   // 100 hitpoints is given in problem

                            // If the division has a remainder, it means some hitpoints < attack are
                            // left after the last attack. This means one more attack is needed.
                            if (offence != 0 && bossHitPts % offence > 0) {
                                turnsNeeded++;
                            }
                            if (defence != 0 && 100 % defence > 0) {
                                turnsAlive++;
                            }

                            // Player goes first: if turnsNeeded == turnsAlive, then player just defeats
                            // the boss.
                            if (turnsNeeded <= turnsAlive) {
                                int cost = w.Cost + a.Cost + r1.Cost + r2.Cost;
                                if (cost < optimalCost) {
                                    optimalCost = cost;
                                }
                            }
                        }
                    }
                }
            }
            return $"{optimalCost}";
        }

        public override string SolvePart2 () {
            // Brute force, as I don't know how to order this efficiently on cost
            int worstCost = 0;
            foreach (Item w in weapons) {
                foreach (Item a in armour) {
                    for (int i = 1; i < rings.Length; i++) {
                        for (int j = 0; j < i; j++) {
                            Item r1 = rings[i];
                            Item r2 = rings[j];
                            int damage = w.Damage + r1.Damage + r2.Damage;
                            int armour = a.Armour + r1.Armour + r2.Armour;

                            int offence = damage - bossArmour;
                            int defence = bossDamage - armour;

                            // Attacker always does at least 1 damage, which translates to <hitpoints> turns needed
                            int turnsNeeded = (offence > 0) ? bossHitPts / offence: bossHitPts;
                            int turnsAlive = (defence > 0) ? 100 / defence : 100;   // 100 hitpoints is given in problem

                            // If the division has a remainder, it means some hitpoints < attack are
                            // left after the last attack. This means one more attack is needed.
                            if (offence != 0 && bossHitPts % offence > 0) {
                                turnsNeeded++;
                            }
                            if (defence != 0 && 100 % defence > 0) {
                                turnsAlive++;
                            }

                            // Player goes first: if turnsNeeded == turnsAlive, then player just defeats
                            // the boss.
                            if (turnsNeeded > turnsAlive) {
                                int cost = w.Cost + a.Cost + r1.Cost + r2.Cost;
                                if (cost > worstCost) {
                                    worstCost = cost;
                                }
                            }
                        }
                    }
                }
            }
            return $"{worstCost}";
        }
    }

    class Item {
        internal int Cost { get; }
        internal int Damage { get; }
        internal int Armour { get; }
        internal Item (int c, int d, int a) {
            Cost = c;
            Damage = d;
            Armour = a;
        }
    }
}