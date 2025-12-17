using System.Collections.Generic;

namespace AoC.Days;

internal class ContainerSearcher
{
    private readonly int[] _containers;
    public int ValidCombinations { get; private set; } = 0;
    public int ValidMinCombinations { get; private set; } = 0;
    public ContainerSearcher(int[] containers)
    {
        _containers = containers;
    }

    public void ComputeValidCombinations(int eggnog)
    {
        ValidCombinations = 0;
        // Grab every combination of containers
        IEnumerable<int[]> combinations = GetAllCombinations(_containers);
        // For each combination, check if it contains the needed amount
        int minimumContainerCount = int.MaxValue;
        foreach (var combination in combinations)
        {
            int combinationSum = 0;
            int containerCount = combination.Length;
            for (int containerIndex = 0; containerIndex < combination.Length; containerIndex++)
            {
                combinationSum += combination[containerIndex];
            }
            if (combinationSum == eggnog)
            {
                if (containerCount < minimumContainerCount) 
                {
                    minimumContainerCount = containerCount;
                    ValidMinCombinations = 1;
                }
                else if (containerCount == minimumContainerCount)
                {
                    ValidMinCombinations++;
                }
                ValidCombinations++;
            }
        }
    }

    private IEnumerable<int[]> GetAllCombinations(int[] items)
    {
        int itemCount = items.Length;
        int bits = 1 << itemCount;

        for (int mask = 0; mask < bits; mask++)
        {
            var subset = new List<int>(itemCount);

            for (int bit = 0; bit < itemCount; bit++)
            {
                if ((mask & (1 << bit)) != 0)
                {
                    subset.Add(items[bit]);
                }
            }

            yield return subset.ToArray();
        }
    }
}
