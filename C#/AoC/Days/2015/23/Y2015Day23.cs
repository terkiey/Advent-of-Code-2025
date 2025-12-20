namespace AoC.Days;

internal class Y2015Day23 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        JaneComputer computer = new(inputLines);
        computer.RunProgram();
        AnswerOne = computer.B.ToString();
        computer.A = 1;
        computer.B = 0;
        computer.RunProgram();
        AnswerTwo = computer.B.ToString();
    }
}
