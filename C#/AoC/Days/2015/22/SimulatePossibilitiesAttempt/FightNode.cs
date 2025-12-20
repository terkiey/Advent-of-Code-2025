namespace AoC.Days;

internal class FightNode
{
    public int BossHP;
    public int HeroHP;
    public int HeroMP;
    public HashSet<EffectInstance> ActiveEffects;
    public int ManaSpent;
    public int RechargeCasts;
    public List<Spell> SpellsCast;

    public FightNode(int bossHP,
        int heroHP,
        int heroMP,
        HashSet<EffectInstance> activeEffects,
        int manaSpent,
        int rechargeCasts,
        List<Spell> spellsCast)
    {
        BossHP = bossHP;
        HeroHP = heroHP;
        HeroMP = heroMP;
        ActiveEffects = activeEffects
                        .Select(effect => effect.Clone())
                        .ToHashSet();
        ManaSpent = manaSpent;
        RechargeCasts = rechargeCasts;
        SpellsCast = [.. spellsCast];
    }

    public FightNode Clone()
    {
        return new FightNode(BossHP, HeroHP, HeroMP, ActiveEffects, ManaSpent, RechargeCasts, SpellsCast);
    }
}
