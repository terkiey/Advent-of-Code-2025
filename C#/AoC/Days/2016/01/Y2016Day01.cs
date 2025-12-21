namespace AoC.Days;

internal class Y2016Day01 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        BlockWalker walker = new();
        AnswerOne = walker.HowFarEnd(inputLines[0]).ToString();
        AnswerTwo = walker.HowFarFirstDoubleVisisted(inputLines[0]).ToString();
    }
}
