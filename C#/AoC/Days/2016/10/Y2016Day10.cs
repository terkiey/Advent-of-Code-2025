namespace AoC.Days;

internal class Y2016Day10 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        RobotMicrochipFactory factory = new();
        factory.ProcessInstructions(inputLines);
        AnswerOne = factory.FindComparer(61, 17).ToString();
        AnswerTwo = factory.MultiplyOutputsTogether().ToString();
    }
}
