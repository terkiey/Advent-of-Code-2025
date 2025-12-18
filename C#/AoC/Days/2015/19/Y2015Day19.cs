namespace AoC.Days;

internal class Y2015Day19 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        var nuclearPlant = new NuclearPlantElement(inputLines);
        AnswerOne = nuclearPlant.CalculateAllSingleReplacements(nuclearPlant.MedicineMolecule)
                                .Count
                                .ToString();

        var calculator = new NuclearFabricationCalculatorElement(nuclearPlant);
        AnswerTwo = calculator.FindFastestMedicineFabrication().ToString();
    }
}
