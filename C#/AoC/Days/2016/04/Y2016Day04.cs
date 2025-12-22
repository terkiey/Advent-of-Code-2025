namespace AoC.Days;

internal class Y2016Day04 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        RoomChecker checker = new();
        AnswerOne = checker.SumValidSectorIds(inputLines).ToString();
        AnswerTwo = checker.NorthPoleRoomCandidates(inputLines);
    }
}
