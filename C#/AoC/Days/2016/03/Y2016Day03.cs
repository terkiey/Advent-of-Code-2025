namespace AoC.Days;

internal class Y2016Day03 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        TriangleRater rater = new();
        AnswerOne = rater.CountValid(inputLines).ToString();
        AnswerTwo = rater.CountValidDifferently(inputLines).ToString();
    }
}
