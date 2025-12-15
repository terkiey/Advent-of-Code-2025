using System.Data;

namespace AoC.Days;

internal class Y2015Day06 : Day
{
    private readonly bool[,] _lightGridOne = new bool[1000, 1000];
    private readonly int[,] _lightGridTwo = new int[1000, 1000];
    protected override void RunLogic(string[] inputLines)
    {
        foreach (string command in inputLines)
        {
            ProcessCommand(ParseCommand(command));
        }

        int countLightsOn = 0;
        int countLightBrightnesses = 0;
        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                if (_lightGridOne[x, y])
                {
                    countLightsOn++;
                }
                countLightBrightnesses += _lightGridTwo[x, y];
            }
        }
        AnswerOne = countLightsOn.ToString();
        AnswerTwo = countLightBrightnesses.ToString();
    }

    private LightGridCommand ParseCommand(string command)
    {
        int[] pointOne = new int[2];
        int[] pointTwo = new int[2];
        string[] splitCommand = command.Split(' ');
        string commandType;
        string[] pointOneSplit;
        string[] pointTwoSplit;
        if (splitCommand[0] == "toggle")
        {
            commandType = splitCommand[0];
            pointOneSplit = splitCommand[1].Split(',');
            pointTwoSplit = splitCommand[3].Split(',');
        }
        else
        {
            commandType = splitCommand[1];
            pointOneSplit = splitCommand[2].Split(',');
            pointTwoSplit = splitCommand[4].Split(',');
        }
        pointOne[0] = int.Parse(pointOneSplit[0]);
        pointOne[1] = int.Parse(pointOneSplit[1]);
        pointTwo[0] = int.Parse(pointTwoSplit[0]);
        pointTwo[1] = int.Parse(pointTwoSplit[1]);
        return new(commandType, pointOne, pointTwo);
    }

    private void ProcessCommand(LightGridCommand command)
    {
        if (command.commandType == "toggle")
        {
            for(int x = command.pointOne[0]; x <= command.pointTwo[0]; x++)
            {
                for (int y = command.pointOne[1]; y <= command.pointTwo[1]; y++)
                {
                    _lightGridOne[x, y] = !_lightGridOne[x, y];
                    _lightGridTwo[x, y] += 2;
                }
            }
            return;
        }
        
        if (command.commandType == "on")
        {
            for (int x = command.pointOne[0]; x <= command.pointTwo[0]; x++)
            {
                for (int y = command.pointOne[1]; y <= command.pointTwo[1]; y++)
                {
                    _lightGridOne[x, y] = true;
                    _lightGridTwo[x, y] += 1;
                }
            }
            return;
        }

        for (int x = command.pointOne[0]; x <= command.pointTwo[0]; x++)
        {
            for (int y = command.pointOne[1]; y <= command.pointTwo[1]; y++)
            {
                _lightGridOne[x, y] = false;
                _lightGridTwo[x, y] -= _lightGridTwo[x, y] > 0 ? 1 : 0;
            }
        }
    }
}
