using System.Diagnostics;

namespace AoC.Days;

public interface IDay
{
    string AnswerOne { get; }
    string AnswerTwo { get; }

    Stopwatch FileTimer { get; }
    Stopwatch LogicTimer { get; }

    void Main(DayArgs args);
}
