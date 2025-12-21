namespace AoC.Days;

internal class Y2015Day25 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        WeatherMachineCodeCalculator calculator = new WeatherMachineCodeCalculator();
        string[] splitLine = inputLines[0].Split(' ');
        string rowString = splitLine[^3];
        string colString = splitLine[^1];
        long rowNum = long.Parse(rowString.Substring(0, 4));
        long colNum = long.Parse(colString.Substring(0, 4));

        AnswerOne = calculator.CalculateCodeAt(colNum, rowNum).ToString();
        AnswerTwo = "Freebie";
    }
}
