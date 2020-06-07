using System;

namespace AdventOfCode.Y2015 {
    class Day22 : Solution {
        int bossHitPts, bossDamage;

        public Day22 () : base(22, 2015, "Wizard Simulator 20XX") {
            // Read input
            bossHitPts = Int32.Parse(Input[0].Split(" ")[^1]);
            bossDamage = Int32.Parse(Input[1].Split(" ")[^1]);
        }

        public override string SolvePart1 () {
            // 50 hitpoints and 500 mana are given in the problem statement
            return $"{playGame(new Game(50, 500, bossHitPts, bossDamage))}";
        }

        public override string SolvePart2 () {
            // 50 hitpoints and 500 mana are given in the problem statement
            return $"{playGame(new Game(50, 500, bossHitPts, bossDamage), hardMode: true)}";
        }

        // Simulate a game recursively, and return the minimal amount of Mana needed to win a game in the current subtree.
        // Return Int32.MaxValue if the game (always) ends in a loss for the player.
        private int playGame (Game game, int currentOptimal = Int32.MaxValue, bool hardMode = false) {
            // First check the game state (win/lose)
            if (game.Mana < 0) {
                // Strictly less than zero means there was too little mana at the start of the player's turn
                // This is therefor always a loss
                return Int32.MaxValue;                
            }
            if (game.PlayerHitPoints <= 0) {
                // Loss
                return Int32.MaxValue;
            }
            if (game.BossHitPoints <= 0) {
                // Win, but no move. No extra Mana spent.
                return game.ManaSpent;
            }

            if (game.ManaSpent < currentOptimal) {  // Only search subtree if we might find a better solution
                foreach (Spell spell in Enum.GetValues(typeof(Spell))) {
                    int opt = playGame(game.PlayTurn(spell, hardMode), currentOptimal, hardMode);
                    if (opt < currentOptimal) {
                        currentOptimal = opt;
                    }
                }
            }

            return currentOptimal;
        }
    }

    class Game {
        internal int ShieldTimer { get; private set; }
        internal int PoisonTimer { get; private set; }
        internal int RechargeTimer { get; private set; }
        internal int ManaSpent { get; private set; }

        internal int PlayerHitPoints { get; private set; }
        internal int PlayerArmour { get; private set; }
        private int _Mana;
        internal int Mana { 
            get => _Mana;
            private set {
                if (value < _Mana) {
                    // Using mana, rather than recharging
                    ManaSpent += _Mana - value;
                }
                _Mana = value;
            }
        }

        internal int BossHitPoints { get; private set; }
        internal int BossDamage { get; }

        // New games always start with zero effects and zero armour
        internal Game (int pPoints, int pMana, int bPoints, int bDamage) : this(pPoints, 0, pMana, bPoints, bDamage, 0, 0, 0) {}

        // Private constructor to clone game state.
        Game (int pPoints, int pArmour, int pMana, int bPoints, int bDamage, int shield, int poison, int recharge, int manaSpent = 0) {
            ShieldTimer = shield;
            PoisonTimer = poison;
            RechargeTimer = recharge;
            PlayerHitPoints = pPoints;
            PlayerArmour = pArmour;
            Mana = pMana;
            BossHitPoints = bPoints;
            BossDamage = bDamage;
            ManaSpent = manaSpent;
        }

        // Simulates a turn for the player, followed by a turn for the boss.
        // Returns a new Game with the result of the turn.
        // Winning or losing conditions are NOT checked.
        internal Game PlayTurn (Spell spell, bool HardMode = false) {
            Game next = new Game(PlayerHitPoints, PlayerArmour, Mana, BossHitPoints, BossDamage, ShieldTimer, PoisonTimer, RechargeTimer, ManaSpent);

            // Player goes first
            if (HardMode) {
                next.PlayerHitPoints--;
            }
            if (next.PlayerHitPoints > 0) {
                next.applyEffects();
            }
            if (next.BossHitPoints > 0) {
                switch (spell) {
                    case Spell.Missile: next.Mana -= 53; next.BossHitPoints -= 4; break;
                    case Spell.Drain: next.Mana -= 73; next.BossHitPoints -= 2; next.PlayerHitPoints += 2; break;
                    case Spell.Shield: next.Mana -= 113; next.ShieldTimer = 6; break;
                    case Spell.Poison: next.Mana -= 173; next.PoisonTimer = 6; break;
                    case Spell.Recharge: next.Mana -= 229; next.RechargeTimer = 5; break;
                }
            }

            // Boss goes next
            if (next.PlayerHitPoints > 0 && next.BossHitPoints > 0) {
                next.applyEffects();
                if (next.BossHitPoints > 0) {
                    int damage = next.BossDamage - next.PlayerArmour;
                    next.PlayerHitPoints -= (damage > 0) ? damage : 1;
                }
            }

            return next;
        }

        private void applyEffects () {
            PlayerArmour = (ShieldTimer > 0) ? 7 : 0;
            if (ShieldTimer > 0) {
                ShieldTimer--;
            }
            if (PoisonTimer > 0) {
                BossHitPoints -= 3;
                PoisonTimer--;
            }
            if (RechargeTimer > 0) {
                Mana += 101;
                RechargeTimer--;
            }
        }
    }

    public enum Spell {
        Missile,
        Drain,
        Shield,
        Poison,
        Recharge
    }
}