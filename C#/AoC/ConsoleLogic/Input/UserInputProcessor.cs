using System.Diagnostics;

namespace AoC.ConsoleLogic;

internal class UserInputProcessor : IUserInputProcessor
{
    public RunCommand AskForCommand()
    {
        RunType runType = AskForRunType();
        int day = AskForDay();
        FileType fileType = AskForFileType();

        int testRuns = 0;
        if (runType == RunType.Test)
        {
            testRuns = AskForTestIterations();
        }

        return new(runType, day, fileType, testRuns);
    }

    private int AskForTestIterations()
    {
        int testCount;

        Console.WriteLine("SPECIFY TEST COUNT:");
        string? userInput = Console.ReadLine();
        if (!TryParseIntInput(userInput, out testCount))
        {
            Console.WriteLine("Try again");
            return AskForTestIterations();
        }

        return testCount;
    }

    private FileType AskForFileType()
    {
        Console.WriteLine("SPECIFY INPUT DATA: real | sample");
        string? userInput = Console.ReadLine();

        if (!TryCleanTextInput(userInput, out string cleanInput))
        {
            Console.WriteLine("Try again");
            return AskForFileType();
        }

        if (cleanInput == "real" || cleanInput == "r")
        {
            return FileType.Real;
        }

        if (cleanInput == "sample" || cleanInput == "s")
        {
            return FileType.Sample;
        }

        Console.WriteLine("you failed" + "\nTry Again");
        return AskForFileType();
    }

    private RunType AskForRunType()
    {
        Console.WriteLine("SPECIFY RUN TYPE: solve | test");
        string? userInput = Console.ReadLine();

        if(!TryCleanTextInput(userInput, out string cleanInput))
        {
            Console.WriteLine("Try again");
            return AskForRunType();
        }
        
        if (cleanInput == "test" || cleanInput == "t")
        {
            return RunType.Test;
        }

        if (cleanInput == "solve" || cleanInput == "s")
        {
            return RunType.Solve;
        }

        Console.WriteLine("you failed" + "\nTry Again");
        return AskForRunType();
    }

    private int AskForDay()
    {
        int day;

        Console.WriteLine("SPECIFY DAY NUMBER:");
        string? userInput = Console.ReadLine();
        if (!TryParseIntInput(userInput, out day))
        {
            Console.WriteLine("Try again");
            return AskForDay();
        }

        if (day < 1 || day > 25)
        {
            Console.WriteLine("kys - not an advent day");
            Console.WriteLine("Try again");
            return AskForDay();
        }

        return day;
    }

    private bool TryParseIntInput(string? userInput, out int cleanInput)
    {
        cleanInput = 0;
        if (userInput is null || string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine("kys - null input");
            return false;
        }

        try
        {
            cleanInput = int.Parse(userInput);
        }
        catch
        {
            Console.WriteLine("kys - wrong format");
            return false;
        }

        return true;
    }

    private bool TryCleanTextInput(string? userInput, out string cleanInput)
    {
        cleanInput = string.Empty;
        if(userInput is null || string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine("kys - null input");
            return false;
        }

        cleanInput = userInput.Trim().ToLower();
        return true;
    }
}
