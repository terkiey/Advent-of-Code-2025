namespace AoC.Days;

internal class Spell
{
    public readonly int ManaCost;
    public readonly EffectInstance? EffectAdded;
    public readonly int DamageDealt;
    public readonly int HealDealt;

    public Spell(int manaCost,
    EffectInstance? effectAdded,
    int damageDealt,
    int healDealt)
    {
        ManaCost = manaCost;
        EffectAdded = effectAdded;
        DamageDealt = damageDealt;
        HealDealt = healDealt;
    }
}