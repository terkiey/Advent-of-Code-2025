namespace AoC.Days;

internal class Y2015Day21 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        RpgShop shop = new RpgShop();
        RpgBossSim boss = new RpgBossSim(inputLines);

        IEnumerable<int[]> loadoutStatsCombs = shop.BuyCheapestFirst();
        int loadoutPrice = -1;
        foreach (var loadoutStatsComb in loadoutStatsCombs)
        {
            if(boss.Fight(loadoutStatsComb[1], loadoutStatsComb[2]))
            {
                loadoutPrice = loadoutStatsComb[0];
                break;
            }
        }
        AnswerOne = loadoutPrice.ToString();

        loadoutStatsCombs = shop.BuyExpensiveFirst();
        foreach (var loadoutStatsComb in loadoutStatsCombs)
        {
            if (!boss.Fight(loadoutStatsComb[1], loadoutStatsComb[2]))
            {
                loadoutPrice = loadoutStatsComb[0];
                break;
            }
        }
        AnswerTwo = loadoutPrice.ToString();
    }
}
