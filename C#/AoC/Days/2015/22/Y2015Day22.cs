using System.Collections.Concurrent;
using System.ComponentModel;

namespace AoC.Days;

internal class Y2015Day22 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        var fightSim = new WizardFightSimulator(inputLines);
        AnswerOne = fightSim.FindCheapestWin().ToString();
        fightSim.HardMode(true);
        AnswerTwo = fightSim.FindCheapestWin().ToString();
        
    }
}
