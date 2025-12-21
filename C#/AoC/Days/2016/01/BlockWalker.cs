namespace AoC.Days;

internal class BlockWalker
{
    public int HowFarEnd(string inputLine)
    {
        int x = 0;
        int y = 0;
        int direction = 0;
        string[] commands = inputLine.Split(", ");
        foreach (string command in commands)
        {
            var turn = command[0];
            var distance = int.Parse(command[1..]);

            if (turn == 'R')
            {
                direction++;
            }
            else if (turn == 'L')
            {
                direction--;
            }

            direction = ((direction % 4) + 4) % 4;

            switch(direction)
            {
                case 0:
                    y += distance;
                    break;

                case 1:
                    x += distance; 
                    break;

                case 2:
                    y -= distance;
                    break;

                case 3:
                    x -= distance;
                    break;
            }
        }

        return Math.Abs(x) + Math.Abs(y);
    }

    public int HowFarFirstDoubleVisisted(string inputLine)
    {
        int x = 0;
        int y = 0;
        int direction = 0;
        string[] commands = inputLine.Split(", ");
        HashSet<Position2D> locationsVisited = [];
        foreach (string command in commands)
        {
            var turn = command[0];
            var distance = int.Parse(command[1..]);

            if (turn == 'R')
            {
                direction++;
            }
            else if (turn == 'L')
            {
                direction--;
            }

            direction = ((direction % 4) + 4) % 4;

            switch (direction)
            {
                case 0:
                    foreach(int visitedY in Enumerable.Range(y+1, distance))
                    {
                        if(!locationsVisited.Add(new(x, visitedY)))
                        {
                            return Math.Abs(x) + Math.Abs(visitedY);
                        }
                    }
                    y += distance;
                    break;

                case 1:
                    foreach (int visitedX in Enumerable.Range(x + 1, distance))
                    {
                        if (!locationsVisited.Add(new(visitedX, y)))
                        {
                            return Math.Abs(visitedX) + Math.Abs(y);
                        }
                    }
                    x += distance;
                    break;

                case 2:
                    foreach (int visitedY in Enumerable.Range(y - distance, distance - 1))
                    {
                        if (!locationsVisited.Add(new(x, visitedY)))
                        {
                            return Math.Abs(x) + Math.Abs(visitedY);
                        }
                    }
                    y -= distance;
                    break;

                case 3:
                    foreach (int visitedX in Enumerable.Range(x - distance, distance - 1))
                    {
                        if (!locationsVisited.Add(new(visitedX, y)))
                        {
                            return Math.Abs(visitedX) + Math.Abs(y);
                        }
                    }
                    x -= distance;
                    break;
            }
        }
        return -1;
    }
}
