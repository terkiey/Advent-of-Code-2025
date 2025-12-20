namespace AoC.Days;

internal class SpellRotationGenerator
{
    private readonly int startMana = SpellStat.HeroMana;
    private const int rechargeMana = 505;
    private readonly Dictionary<SpellEnum, int> spellCosts = SpellStat.Cost;

    public IEnumerable<SpellEnum[]> GetSpellRotations(int rechargeCasts)
    {
        int lastMaxManaSpend = startMana + ((rechargeCasts - 1) * rechargeMana);
        int maxManaSpend = startMana + (rechargeCasts * rechargeMana);
        int cheapestSpell = spellCosts.Values.Min();
        int maxSpells = maxManaSpend / cheapestSpell;

        int minManaSpend = lastMaxManaSpend + spellCosts[SpellEnum.Recharge];
        int costliestSpell = spellCosts.Values.Max();
        int minSpells = minManaSpend / costliestSpell;
        if (rechargeCasts == 0)
        {
            minManaSpend = 0;
        }

        for (int spellCount = minSpells; spellCount <= maxSpells; spellCount++)
        {
            var spellRotations = TakeSpellsWithReplacement(spellCount - rechargeCasts);
            var spellRotationsWithRecharges = rechargeCasts == 0 ? spellRotations : InsertRechargeCasts(spellRotations, rechargeCasts);
            var validSpellRotations = spellRotationsWithRecharges.Where(rotation =>
            {
                int manaSpend = rotation.Sum(spell => spellCosts[spell]);
                return manaSpend >= minManaSpend && manaSpend <= maxManaSpend;
            });
            var spellRotationsSorted = validSpellRotations.OrderBy(rotation => rotation.Sum(spell => spellCosts[spell]));
            foreach (var rotation in spellRotationsSorted)
            {
                 yield return rotation;
            }
        }
    }

    private static IEnumerable<SpellEnum[]> InsertRechargeCasts(IEnumerable<SpellEnum[]> spellRotations, int rechargeCasts)
    {
        foreach(var rotation in spellRotations)
        {
            foreach( var newRotation in InsertRechargeCastsIntoRotation(rotation, rechargeCasts))
            {
                yield return newRotation;
            }
        }
    }

    private static IEnumerable<SpellEnum[]> InsertRechargeCastsIntoRotation(SpellEnum[] inputRotation, int rechargeCasts)
    {
        IEnumerable<List<SpellEnum>> current = [ inputRotation.ToList() ];
        for (int i = 0; i < rechargeCasts; i++)
        {
            current = current.SelectMany(rotation =>
                Enumerable.Range(0, rotation.Count + 1)
                    .Select(insertIndex =>
                    {
                        var copy = new List<SpellEnum>(rotation);
                        copy.Insert(insertIndex, SpellEnum.Recharge);
                        return copy;
                    }));
        }

        foreach (var r in current)
        {
            yield return r.ToArray();
        }
    }

    private static IEnumerable<SpellEnum[]> TakeSpellsWithReplacement(int length)
    {
        var values = Enum.GetValues(typeof(SpellEnum)).Cast<SpellEnum>().Where(spell => spell != SpellEnum.Recharge).ToArray();
        int n = values.Length;

        var indices = new int[length];

        while (true)
        {
            yield return indices.Select(i => values[i]).ToArray();

            int pos = length - 1;
            while (pos >= 0 && ++indices[pos] == n)
            {
                indices[pos] = 0;
                pos--;
            }

            if (pos < 0)
                break;
        }
    }
}
