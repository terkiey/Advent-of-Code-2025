namespace AoC.Days;

internal class Day4 : Day
{
    protected override void RunLogic(string[] gridRowStrings)
    {
        IRollRater _paperRater = new RollRater(gridRowStrings);

        _paperRater.RateRolls();
        int peelCounter = _paperRater.AccessibleRollCount;
        AnswerOne = peelCounter.ToString();

        while (_paperRater.PeelLayer())
        {
            _paperRater.RateRolls();
            peelCounter += _paperRater.AccessibleRollCount;
        }

        AnswerTwo = peelCounter.ToString();
    }
}
