namespace AoC.Days;

internal class TriangleRater
{
    public int CountValid(string[] inputLines)
    {
        int count = 0;
        foreach(string triangleDef in inputLines)
        {
            var sidesStrings = triangleDef.Split(' ').ToList();
            sidesStrings.RemoveAll(sideString => string.IsNullOrWhiteSpace(sideString));
            int[] sides = new int[3];
            int sideIndex = 0;
            foreach (string sideString in sidesStrings)
            {
                sides[sideIndex] = int.Parse(sideString.Trim());
                sideIndex++;
            }
            count += IsTriangle(sides) ? 1 : 0;
        }
        return count;
    }

    public int CountValidDifferently(string[] inputLines)
    {
        int[][] triangles = new int[inputLines.Length][];
        for (int triangleIndex = 0; triangleIndex < triangles.Length; triangleIndex++)
        {
            triangles[triangleIndex] = new int[3];
        }

        for (int rowIndex = 0; rowIndex < triangles.Length; rowIndex++)
        {
            string rowLine = inputLines[rowIndex];
            var sidesStrings = rowLine.Split(' ').ToList();
            sidesStrings.RemoveAll(sideString => string.IsNullOrWhiteSpace(sideString));
            for (int colIndex = 0; colIndex < 3; colIndex++)
            {
                int triangleIndex = ((triangles.Length / 3) * colIndex) + (rowIndex / 3);
                int sideIndex = rowIndex % 3;
                triangles[triangleIndex][sideIndex] = int.Parse(sidesStrings[colIndex]);
            }
        }

        int count = 0;
        foreach(int[] triangle in triangles)
        {
            count += IsTriangle(triangle) ? 1 : 0;
        }
        return count;
    }

    public bool IsTriangle(int[] sides)
    {
        return sides.Max() < sides.Sum() - sides.Max();
    }
}
