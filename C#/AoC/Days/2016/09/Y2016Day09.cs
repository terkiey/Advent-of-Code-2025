namespace AoC.Days;

internal class Y2016Day09 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        ExperimentalFormatDecompressor decompressor = new();
        AnswerOne = decompressor.DecompressAndCountLength(inputLines).ToString();
        AnswerTwo = decompressor.CalculateLengthFormatVersionTwo(inputLines).ToString();
    }
}
