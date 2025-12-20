namespace AoC.Days;

internal class WizardFightSimulator
{
    // Starting values
    private int BossMaxHP { get; set; }
    private int BossBaseDamage { get; set; }

    private const int HeroMaxHP = 50;
    private const int HeroStartMana = 500;

    private bool IsHardMode { get; set; }

    private readonly Dictionary<SpellEnum, Spell> Spells = [];

    public WizardFightSimulator(string[] inputLines)
    {
        BossMaxHP = int.Parse(inputLines[0].Split(": ")[1]);
        BossBaseDamage = int.Parse(inputLines[1].Split(": ")[1]);
        IsHardMode = false;

        RegisterSpells();
    }
    
    public void HardMode(bool hardMode)
    {
        IsHardMode = hardMode;
    }

    public int FindCheapestWin()
    {
        int maxRechargeCasts = 0;
        while (true)
        {
            int fightCost = FindCheapestWin(maxRechargeCasts);
            if (fightCost != int.MaxValue)
            {
                return fightCost;
            }
            maxRechargeCasts++;
        }
    }

    public int FindCheapestWin(int maxRechargeCasts)
    {
        Queue<FightNode> nodeQueue = new Queue<FightNode>();
        int fightCost = int.MaxValue;

        FightNode startCondition;
        if (IsHardMode)
        {
            startCondition = new(BossMaxHP, HeroMaxHP - 1, HeroStartMana, [], 0, 0, []);
        }
        else
        {
            startCondition = new(BossMaxHP, HeroMaxHP, HeroStartMana, [], 0, 0, []);
        }
        nodeQueue.Enqueue(startCondition);

        var shieldEffect = new EffectInstance(Effect.Shield, int.MaxValue);
        while (nodeQueue.Count > 0)
        {
            FightNode currentNode = nodeQueue.Dequeue();

            // Determine valid spells to cast (mana available, effect not already on)
            List<Spell> validSpells = DetermineValidSpells(currentNode, maxRechargeCasts);

            // Separately simulate casting each spell to get a variety of states.
            foreach (Spell spell in validSpells)
            {
                // Clone the original state.
                FightNode state = currentNode.Clone();
                // Cast spell
                state = CastSpell(state, spell);
                if (state.BossHP <= 0)
                {
                    fightCost = Math.Min(fightCost, state.ManaSpent);
                    continue;
                }

                // Boss turn starts
                // Process effects
                state = ProcessEffects(state);
                if (state.BossHP <= 0)
                {
                    fightCost = Math.Min(fightCost, state.ManaSpent);
                }

                // Boss attack
                if (state.ActiveEffects.Select(effect => effect.Type).Contains(Effect.Shield))
                {
                    state.HeroHP -= BossBaseDamage - 7;
                }
                else
                {
                    state.HeroHP -= BossBaseDamage;
                }

                if (state.HeroHP <= 0)
                {
                    continue;
                }

                // Player turn starts
                // Process hard mode damage
                if (IsHardMode)
                {
                    state.HeroHP -= 1;
                    if (state.HeroHP <= 0)
                    {
                        continue;
                    }
                }

                // Process effects
                state = ProcessEffects(state);
                if (state.BossHP <= 0)
                {
                    fightCost = Math.Min(fightCost, state.ManaSpent);
                }

                // The pruning algorithm doesnt work, omit for now as it runs fast enough anyway.
                //if (Prune(state, fightCost, maxRechargeCasts))
                //{
                //    continue;
                //}

                nodeQueue.Enqueue(state);
            }
        }
        return fightCost;
    }

    private List<Spell> DetermineValidSpells(FightNode state, int maxRechargeCasts)
    {
        List<Spell> output = [];
        int mana = state.HeroMP;
        foreach (SpellEnum spellName in Spells.Keys)
        {
            Spell spell = Spells[spellName];
            if (spell.ManaCost > mana)
            {
                continue;
            }

            if (!(spell.EffectAdded is null))
            {
                if (state.ActiveEffects.Contains(spell.EffectAdded))
                {
                    continue;
                }

                if (spell.EffectAdded.Type == Effect.Recharge && state.RechargeCasts == maxRechargeCasts)
                {
                    continue;
                }
            }

            output.Add(spell);
        }
        return output;
    }

    private FightNode CastSpell(FightNode state, Spell spell)
    {
        state.BossHP -= spell.DamageDealt;
        state.HeroHP += spell.HealDealt;
        state.ManaSpent += spell.ManaCost;
        state.HeroMP -= spell.ManaCost;
        if (!(spell.EffectAdded is null))
        {
            state.ActiveEffects.Add(spell.EffectAdded.Clone());
            if (spell.EffectAdded.Type == Effect.Recharge)
            {
                state.RechargeCasts++;
            }
        }
        state.SpellsCast.Add(spell);
        return state;
    }

    private FightNode ProcessEffects(FightNode state)
    {
        HashSet<EffectInstance> expiredEffects = [];
        foreach (EffectInstance effect in state.ActiveEffects)
        {
            switch (effect.Type)
            {
                case Effect.Shield:
                    break;

                case Effect.Poison:
                    state.BossHP -= 3;
                    break;

                case Effect.Recharge:
                    state.HeroMP += 101;
                    break;
            }
            effect.RemainingTurns--;
            if (effect.RemainingTurns == 0)
            {
                expiredEffects.Add(effect);
            }
        }

        foreach (EffectInstance effect in expiredEffects)
        {
            state.ActiveEffects.Remove(effect);
        }
        return state;
    }

    private bool Prune(FightNode state, int minFightCost, int maxRechargeCasts)
    {
        // Do you have recharge casts left? If so, and you dont have enough mana for it, then prune.
        int remainingRechargeCasts = maxRechargeCasts - state.RechargeCasts;
        if (remainingRechargeCasts > 0 && state.HeroMP < Spells[SpellEnum.Recharge].ManaCost)
        {
            return true;
        }

        int rechargeTicksLeft = 0;
        int poisonTicksLeft = 0;
        foreach (EffectInstance effect in state.ActiveEffects)
        {
            if (effect.Type == Effect.Recharge)
            {
                rechargeTicksLeft = effect.RemainingTurns;
            }

            if (effect.Type == Effect.Poison)
            {
                poisonTicksLeft = effect.RemainingTurns;
            }
        }

        // (can possibly kill boss within total remaining mana) (assuming all from the cheapest source of dps: poison)
        int manaToSpendOnRecharges = Spells[SpellEnum.Recharge].ManaCost * remainingRechargeCasts;
        int manaToRecharge = (remainingRechargeCasts * 505) + (101 * rechargeTicksLeft);
        int possibleTotalManaSpend = state.HeroMP + state.ManaSpent + manaToRecharge;
        int maxManaSpendAllowed = Math.Min(minFightCost, possibleTotalManaSpend);
        int totalRemainingDpsMana = maxManaSpendAllowed - state.ManaSpent - manaToSpendOnRecharges;

        int poisonCastsUpperBound = totalRemainingDpsMana / Spells[SpellEnum.Poison].ManaCost;
        int RemainingDamageUpperBound = (poisonCastsUpperBound * 18) + (poisonTicksLeft * 3);
        if (RemainingDamageUpperBound < state.BossHP)
        {
            return true;
        }
        return false;
    }

    private void RegisterSpells()
    {
        var magicMissile = new Spell(53, null, 4, 0);
        var drain = new Spell(73, null, 2, 2);

        var shieldSpellEffect = new EffectInstance(Effect.Shield, 6);
        var shield = new Spell(113, shieldSpellEffect, 0, 0);

        var poisonSpellEffect = new EffectInstance(Effect.Poison, 6);
        var poison = new Spell(173, poisonSpellEffect, 0, 0);

        var rechargeSpellEffect = new EffectInstance(Effect.Recharge, 5);
        var recharge = new Spell(229, rechargeSpellEffect, 0, 0);

        Spells.Add(SpellEnum.MagicMissile, magicMissile);
        Spells.Add(SpellEnum.Drain, drain);
        Spells.Add(SpellEnum.Shield, shield);
        Spells.Add(SpellEnum.Poison, poison);
        Spells.Add(SpellEnum.Recharge, recharge);
    }
}
