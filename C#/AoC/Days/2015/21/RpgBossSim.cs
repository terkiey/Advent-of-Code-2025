namespace AoC.Days;

internal class RpgBossSim
{
    private const int HeroHp = 100;

    private readonly int BossHp;
    private readonly int BossAttack;
    private readonly int BossDefence;

    public RpgBossSim(string[] inputLines)
    {
        BossHp = int.Parse(inputLines[0].Split(": ")[1]);
        BossAttack = int.Parse(inputLines[1].Split(": ")[1]);
        BossDefence = int.Parse(inputLines[2].Split(": ")[1]);
    }

    public bool Fight(int heroAttack, int heroDefence)
    {
        int currentBossHp = BossHp;
        int currentHeroHp = HeroHp;

        while (currentBossHp > 0 && currentHeroHp > 0)
        {
            int bossDamageReceived = Math.Max(1, heroAttack - BossDefence);
            int heroDamageReceived = Math.Max(1, BossAttack - heroDefence);

            currentBossHp -= bossDamageReceived;
            currentHeroHp -= heroDamageReceived;
        }

        if (currentBossHp <= 0)
        {
            return true;
        }
        return false;
    }
}
