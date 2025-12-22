namespace AoC.Days;

internal class Y2016Day05 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        string doorId = inputLines[0];
        SecurityDoorPasswordGenerator passGen = new();
        AnswerOne = passGen.GenerateFromInput(doorId);
        AnswerTwo = passGen.GenerateInspiredFromInput(doorId);
    }
}
