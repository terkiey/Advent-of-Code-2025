namespace AoC.Days;

internal class RpgShop
{
    private readonly int[][] Weapons =
    [
        [8, 4, 0],
        [10, 5, 0],
        [25, 6, 0],
        [40, 7, 0],
        [74, 8, 0]
    ];

    private readonly int[][] Armor =
    [
        [13, 0, 1],
        [31, 0, 2],
        [53, 0, 3],
        [75, 0, 4],
        [102, 0, 5]
    ];

    private readonly int[][] Rings =
    [
        [25, 1, 0],
        [50, 2, 0],
        [100, 3, 0],
        [20, 0, 1],
        [40, 0, 2],
        [80, 0, 3]
    ];

    public IEnumerable<int[]> BuyCheapestFirst()
    {
        return CalculatePurchaseCombs().OrderBy(comb => comb[0]);
    }

    public IEnumerable<int[]> BuyExpensiveFirst()
    {
        return CalculatePurchaseCombs().OrderBy(comb => comb[0] * -1);
    }

    private List<int[]> CalculatePurchaseCombs()
    {
        var weaponStatsCombs = WeaponsCombs();
        var armorStatsCombs = ArmorCombs();
        var ringStatsCombs = RingCombs();
        List<int[]> allCombs = [];
        int[] combStats = [0, 0, 0];
        foreach (var weaponComb in weaponStatsCombs)
        {
            foreach (var armorComb in armorStatsCombs)
            {
                foreach (var ringComb in ringStatsCombs)
                {
                    for (int statIndex = 0; statIndex < 3; statIndex++)
                    {
                        combStats[statIndex] = weaponComb[statIndex] + armorComb[statIndex] + ringComb[statIndex];
                    }
                    allCombs.Add([.. combStats]);
                    combStats = [0, 0, 0];
                }
            }
        }
        return allCombs;
    }

    private List<int[]> WeaponsCombs()
    {
        return [.. Weapons];
    }

    private List<int[]> ArmorCombs()
    {
        List<int[]> combList = [.. Armor];
        combList.Insert(0, [0, 0, 0]);
        return combList;
    }

    private List<int[]> RingCombs()
    {
        List<int[]> ringList = [.. Rings];
        List<int[]> combList = [];
        ringList.Insert(0, [0, 0, 0]);
        for (int ringOneIndex = 0; ringOneIndex < ringList.Count - 1; ringOneIndex++)
        {
            var ringOne = ringList[ringOneIndex];
            for (int ringTwoIndex = ringOneIndex + 1; ringTwoIndex < ringList.Count; ringTwoIndex++)
            {
                var ringTwo = ringList[ringTwoIndex];
                int[] ringStats = [ringOne[0] + ringTwo[0], ringOne[1] + ringTwo[1], ringOne[2] + ringTwo[2]];
                combList.Add(ringStats);
            }
        }
        combList.Add([0, 0, 0]);
        return combList;
    }
}
