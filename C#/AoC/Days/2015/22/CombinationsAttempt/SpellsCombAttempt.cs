namespace AoC.Days;

internal static class SpellStat
{
    public const int HeroMana = 500;

    public static Dictionary<SpellEnum, int> Cost = new()
    {
        {SpellEnum.MagicMissile, 53},
        {SpellEnum.Drain, 73},
        {SpellEnum.Shield, 113},
        {SpellEnum.Poison, 173},
        {SpellEnum.Recharge, 229}
    };
}

internal enum SpellEnum
{
    MagicMissile = 0,
    Drain = 1,
    Shield = 2,
    Poison = 3,
    Recharge = 4,
}
