using AoC.ConsoleLogic;

namespace AoC;

internal class Program
{
    static void Main()
    {
        IUserInputProcessor userInputProcessor = new UserInputProcessor();
        IRunCommandProcessor runCommandProcessor = new RunCommandProcessor();

        while (true)
        {
            RunCommand command = userInputProcessor.AskForCommand();
            runCommandProcessor.Process(command);
        }
    }
}
