namespace AoC.Days;

internal class RobotMicrochipFactory
{
    private Robot[] Robots = [];
    private int[] Outputs = [];

    public void ProcessInstructions(string[] instructions)
    {
        Robots = new Robot[instructions.Length];
        for (int robotIndex = 0; robotIndex < Robots.Length; robotIndex++)
        {
            Robots[robotIndex] = new Robot();
        }
        Outputs = new int[instructions.Length];
        List<MicrochipComparison> comparisons = [];
        foreach (string instruction in instructions)
        {
            string[] splitInstruction = instruction.Split(' ');
            if (splitInstruction[0] == "value")
            {
                int value = int.Parse(splitInstruction[1]);
                int robotIndex = int.Parse(splitInstruction[5]);
                Robots[robotIndex].Give(value);
            }

            if (splitInstruction[0] == "bot")
            {
                int robotIndex = int.Parse(splitInstruction[1]);
                string lowTo = splitInstruction[5] + ' ' + splitInstruction[6];
                string highTo = splitInstruction[^2] + ' ' + splitInstruction[^1];
                MicrochipComparison comparison = new(robotIndex, lowTo, highTo);
                comparisons.Add(comparison);
            }
        }

        while (comparisons.Count > 0)
        {
            foreach (MicrochipComparison comparison in comparisons.ToList())
            {
                if (TryProcessComparison(comparison))
                {
                    comparisons.Remove(comparison);
                }
            }
        }
        
    }

    public int FindComparer(int chipOne, int chipTwo)
    {
        int low = Math.Min(chipOne, chipTwo);
        int high = Math.Max(chipOne, chipTwo);
        for (int robotIndex = 0; robotIndex < Robots.Length; robotIndex++) 
        {
            Robot robot = Robots[robotIndex];
            if (robot.Low == low && robot.High == high)
            {
                return robotIndex;
            }
        }
        return -1;
    }

    public int MultiplyOutputsTogether()
    {
        return Outputs[0] * Outputs[1] * Outputs[2];
    }

    private bool TryProcessComparison(MicrochipComparison comparison)
    {
        Robot robot = Robots[comparison.RobotIndex];
        if (!robot.IsReadyForComparison())
        {
            return false;
        }
        else
        {
            string[] splitLowTo = comparison.LowTo.Split(' ');
            string[] splitHighTo = comparison.HighTo.Split(' ');

            switch (splitLowTo[0])
            {
                case "bot":
                    Robots[int.Parse(splitLowTo[1])].Give(robot.Low);
                    break;

                case "output":
                    Outputs[int.Parse(splitLowTo[1])] = robot.Low!.Value;
                    break;
            }

            switch (splitHighTo[0])
            {
                case "bot":
                    Robots[int.Parse(splitHighTo[1])].Give(robot.High);
                    break;

                case "output":
                    Outputs[int.Parse(splitHighTo[1])] = robot.High!.Value;
                    break;
            }
        }
        return true;
    }
}
