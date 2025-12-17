namespace AoC.Days;

internal class Y2015Day17 : Day
{
    private const int target = 150;
    protected override void RunLogic(string[] inputLines)
    {
        int[] containers = new int[inputLines.Length];
        for (int containerIndex = 0; containerIndex < containers.Length;  containerIndex++)
        {
            containers[containerIndex] = int.Parse(inputLines[containerIndex]);
        }

        ContainerSearcher searcher = new ContainerSearcher(containers);
        searcher.ComputeValidCombinations(150);
        AnswerOne = searcher.ValidCombinations.ToString();
        AnswerTwo = searcher.ValidMinCombinations.ToString();
    }
}
