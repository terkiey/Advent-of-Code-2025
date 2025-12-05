using System.Diagnostics;

namespace AoC.Days;
internal abstract class Day : IDay
{
    public string AnswerOne { get; protected set; } = String.Empty;
    public string AnswerTwo { get; protected set; } = String.Empty;
    public Stopwatch FileTimer { get; protected set; } = new Stopwatch();
    public Stopwatch LogicTimer { get; protected set; } = new Stopwatch();

    public void Main(DayArgs args)
    {
        string[] input = FileInput(args.filename);
        RunLogicTimed(input);
    }

    protected string[] FileInput(string filename)
    {
        FileTimer.Start();
        string path = Path.Combine(AppContext.BaseDirectory, "Data", filename);
        string[] input = File.ReadAllLines(path);
        FileTimer.Stop();

        return input;
    }

    protected void RunLogicTimed(string[] input)
    {
        LogicTimer.Start();
        RunLogic(input);
        LogicTimer.Stop();
    }

    protected abstract void RunLogic(string[] input);
}
