namespace AoC.Days;

internal class HousePresentCalculator
{
    private readonly List<int> primesFound = [];

    public HousePresentCalculator() { }

    public int PresentCount(int houseNumber)
    {
        List<int> primeFactors = PrimeFactorDecomp(houseNumber);
        HashSet<int> elves = TranslatePfdToElves(primeFactors);
        return elves.Aggregate(0, (sum, elf) => sum += elf * 10);
    }

    public int LazyElfPresentCount(int houseNumber)
    {
        List<int> primeFactors = PrimeFactorDecomp(houseNumber);
        HashSet<int> elves = TranslatePfdToElves(primeFactors);
        foreach (int elf in elves.ToHashSet())
        {
            if (elf * 50 < houseNumber)
            {
                elves.Remove(elf);
            }
        }
        return elves.Aggregate(0, (sum, elf) => sum += elf * 11);
    }

    private List<int> PrimeFactorDecomp(int number)
    {
        List<int> primeFactorDecomp = [];
        if (number == 1)
        {
            return [];
        }

        bool primeFactorFound = false;
        foreach (int primeNumber in primesFound)
        {
            if (number % primeNumber == 0)
            {
                primeFactorFound = true;
                primeFactorDecomp.Add(primeNumber);
                primeFactorDecomp.AddRange(PrimeFactorDecomp(number / primeNumber));
                break;
            }
        }

        if (!primeFactorFound)
        {
            primesFound.Add(number);
            primeFactorDecomp.Add(number);
        }

        return primeFactorDecomp;
    }

    private HashSet<int> TranslatePfdToElves(List<int> primeFactors)
    {
        HashSet<int> elves = [1];
        var combinations = GetAllCombinations(primeFactors);
        foreach (var combination in combinations)
        {
            int elf = combination.Aggregate(1, (acc, factor) => acc *= factor);
            elves.Add(elf);
        }
        return elves;
    }

    private IEnumerable<List<int>> GetAllCombinations(List<int> items)
    {
        int itemCount = items.Count;
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

            yield return subset;
        }
    }
}
