using AoC.ConsoleLogic;

namespace AoC.Days;

internal class JaneComputer
{
    private string[] commandType;
    private string[] commandTarget;
    private int[] commandOffset;

    public uint A { get; set; }
    public uint B { get; set; }

    public JaneComputer(string[] inputLines)
    {
        commandType = new string[inputLines.Length];
        commandTarget = new string[inputLines.Length];
        commandOffset = new int[inputLines.Length];

        int lineIndex = 0;
        foreach (string line in inputLines)
        {
            ParseInputLine(line, lineIndex);
            lineIndex++;
        }

        A = 0;
        B = 0;
    }

    private void ParseInputLine(string line, int index)
    {
        commandType[index] = line.Substring(0, 3);
        string[] splitLine = line.Split(' ');
        if (commandType[index] != "jmp")
        {
            commandTarget[index] = splitLine[1][0].ToString();
        }
        else
        {
            commandOffset[index] = int.Parse(splitLine[1]);
        }

        if (commandType[index] == "jie" || commandType[index] == "jio")
        {
            commandOffset[index] = int.Parse(splitLine[2]);
        }
    }

    public void RunProgram()
    {
        int lineIndex = 0;

        while (true)
        {
            RunCommandLine(ref lineIndex);

            if (lineIndex >= commandType.Length)
            {
                return;
            }
        }
    }

    public void RunCommandLine(ref int commandIndex)
    {
        switch (commandType[commandIndex])
        {
            case "hlf":
                if (commandTarget[commandIndex] == "a")
                {
                    A = A / 2;
                }
                else
                {
                    B = B / 2;
                }
                commandIndex++;
                break;

            case "tpl":
                if (commandTarget[commandIndex] == "a")
                {
                    A = A * 3;
                }
                else
                {
                    B = B * 3;
                }
                commandIndex++;
                break;

            case "inc":
                if (commandTarget[commandIndex] == "a")
                {
                    A++;
                }
                else
                {
                    B++;
                }
                commandIndex++;
                break;

            case "jmp":
                commandIndex += commandOffset[commandIndex];
                break;

            case "jie":
                if (commandTarget[commandIndex] == "a")
                {
                    commandIndex += A % 2 == 0 ? commandOffset[commandIndex] : 1;
                }
                else
                {
                    commandIndex += B % 2 == 0 ? commandOffset[commandIndex] : 1;
                }
                break;

            case "jio":
                if (commandTarget[commandIndex] == "a")
                {
                    commandIndex += A == 1 ? commandOffset[commandIndex] : 1;
                }
                else
                {
                    commandIndex += B == 1 ? commandOffset[commandIndex] : 1;
                }
                break;
        }
    }
}
