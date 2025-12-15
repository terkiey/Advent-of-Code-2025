namespace AoC.Days;

internal class Y2015Day09 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        HashSet<string> locations = [];
        Dictionary<string, List<DestinationPath>> destinations = [];
        foreach (string line in inputLines)
        {
            string[] splitLine = line.Split(' ');
            string start = splitLine[0];
            string end = splitLine[2];
            int distance = int.Parse(splitLine[4]);
            locations.Add(start);
            locations.Add(end);
            try
            {
                destinations[start].Add(new(end, distance));
            }
            catch
            {
                destinations.Add(start, []);
                destinations[start].Add(new(end, distance));
            }

            try
            {
                destinations[end].Add(new(start, distance));
            }
            catch
            {
                destinations.Add(end, []);
                destinations[end].Add(new(start, distance));
            }
        }

        List<string> shortestPath = [];
        List<string> longestPath = [];
        int shortestPathLength = int.MaxValue;
        int longestPathLength = int.MinValue;
        foreach (IEnumerable<string> path in Permutations(locations.ToList(), locations.Count()))
        {
            List<string> checkPath = path.ToList();
            int checkPathLength = 0;
            bool pathSuccess = true;
            for (int locationIndex = 0; locationIndex < checkPath.Count -1;  locationIndex++)
            {
                string start = checkPath[locationIndex];
                string end = checkPath[locationIndex + 1];
                if (!destinations.TryGetValue(start, out List<DestinationPath>? possibleDestinationPaths))
                {
                    pathSuccess = false;
                    break;
                }

                DestinationPath destinationPath;
                try
                {
                    destinationPath = possibleDestinationPaths.Where(p => p.end == end).First();
                }
                catch
                {
                    pathSuccess = false;
                    break;
                }
                
                checkPathLength += destinationPath.distance;
            }

            if (pathSuccess && checkPathLength < shortestPathLength)
            {
                shortestPathLength = checkPathLength;
                shortestPath = checkPath;
            }
            if (pathSuccess && checkPathLength > longestPathLength)
            {
                longestPathLength = checkPathLength;
                longestPath = checkPath;
            }
        }
        AnswerOne = shortestPathLength.ToString();
        AnswerTwo = longestPathLength.ToString();
    }

    private IEnumerable<IEnumerable<string>> Permutations(IEnumerable<string> list, int length)
    {
        return length == 1
            ? list.Select(t => new[] { t })
            : Permutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t, o) => t.Append(o));
    }
}
