namespace AoC.Days;

internal class NuclearFabricationCalculatorString(NuclearPlantString inNuclear)
{
    private const string fabricationStart = "e";
    private readonly NuclearPlantString _nuclearPlant = inNuclear;
    private string MedicineMolecule => _nuclearPlant.MedicineMolecule;

    public int FindFastestMedicineFabrication()
    {
        return BreadthFirstSearch(MedicineMolecule);
    }

    private int BreadthFirstSearch(string targetMolecule)
    {
        Queue<FabricationNodeString> nodes = new();
        HashSet<string> moleculesVisited = [];

        if (fabricationStart == targetMolecule)
        {
            return 0;
        }
        nodes.Enqueue(new(fabricationStart, 0));
        while (nodes.Count > 0)
        {
            FabricationNodeString currentNode = nodes.Dequeue();
            string currentMolecule = currentNode.Molecule;
            int currentSteps = currentNode.Steps;

            // -1. Add current molecule to list of currently visited, if already visited, then skip processing.
            if (!moleculesVisited.Add(currentMolecule))
            {
                continue;
            }

            // 0. Check if you cannot reach targetMolecule, if so, end the node.
            // Rules:
            // Has more length than the target (changes are non-decreasing)
            if (currentMolecule.Length > targetMolecule.Length)
            {
                continue;
            }

            // 1. Calculate neighbours (all possible single replacements)
            var neighbourMolecules = _nuclearPlant.CalculateAllSingleReplacements(currentMolecule);
            foreach (var neighbourMolecule in neighbourMolecules)
            {
                if (neighbourMolecule == targetMolecule)
                {
                    return currentSteps + 1;
                }
                nodes.Enqueue(new(neighbourMolecule, currentSteps + 1));
            }
        }
        return int.MaxValue;
    }
}
