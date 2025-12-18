namespace AoC.Days;

internal class NuclearFabricationCalculatorElement(NuclearPlantElement inNuclear)
{
    private readonly Molecule fabricationStart = new(["e"]);
    private readonly NuclearPlantElement _nuclearPlant = inNuclear;
    private Molecule MedicineMolecule => _nuclearPlant.MedicineMolecule;
    private HashSet<string> UniqueImmutableElements = [];

    public int FindFastestMedicineFabrication()
    {
        var medicineImmutables = GetImmutableElements(MedicineMolecule);
        UniqueImmutableElements = [.. medicineImmutables];
        foreach (List<Molecule> possibleMolecules in _nuclearPlant.Replacements.Values)
        {
            foreach (Molecule molecule in possibleMolecules)
            {
                var immutables = GetImmutableElements(molecule);
                UniqueImmutableElements.UnionWith(immutables);
            }
        }

        return FastestFabrication();
    }

    private int FastestFabrication()
    {
        // Abuse the input details in order to calculate the number of steps needed.
        int totalElements = MedicineMolecule.Length;
        int ArElements = MedicineMolecule.Elements.Where(e => e == "Ar").Count();
        int YElements = MedicineMolecule.Elements.Where(e => e == "Y").Count();
        int RnElements = MedicineMolecule.Elements.Where(e => e == "Rn").Count();

        return totalElements - 1 - (ArElements + RnElements) - (2 * YElements);
    }

    private int BreadthFirstSearch(Molecule targetMolecule)
    {
        List<string> targetImmutables = GetImmutableElements(targetMolecule);
        Queue<FabricationNodeElement> nodes = new();
        HashSet<Molecule> moleculesVisited = [];

        if (fabricationStart == targetMolecule)
        {
            return 0;
        }
        nodes.Enqueue(new(fabricationStart, 0));
        while (nodes.Count > 0)
        {
            FabricationNodeElement currentNode = nodes.Dequeue();
            Molecule currentMolecule = currentNode.Molecule;
            int currentSteps = currentNode.Steps;

            // -1. Add current molecule to list of currently visited, if already visited, then skip processing.
            if (!moleculesVisited.Add(currentMolecule))
            {
                continue;
            }

            // 0. Check if you cannot reach targetMolecule, if so, end the node.
            // Rules:
            // Has length greater-than or equal to the target length (changes are increasing)
            // Immutable elements are in the same order as target immutable elements
            if (currentMolecule.Length >= targetMolecule.Length)
            {
                continue;
            }

            if (!MatchingImmutableOrder(currentMolecule, targetImmutables))
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

    private bool MatchingImmutableOrder(Molecule currentMolecule, List<string> targetOrder)
    {
        List<string> currentImmutables = [];
        foreach (string element in currentMolecule.Elements)
        {
            if (UniqueImmutableElements.Contains(element))
            {
                currentImmutables.Add(element);
            }
        }
        if (currentImmutables.Count > targetOrder.Count)
        {
            return false;
        }

        for (int elementIndex = 0; elementIndex < currentImmutables.Count; elementIndex++)
        {
            if (currentImmutables[elementIndex] != targetOrder[elementIndex])
            {
                return false;
            }
        }
        return true;
    }

    private List<string> GetImmutableElements(Molecule molecule)
    {
        List<string> immutableElements = [];
        foreach(var element in molecule.Elements)
        {
            Molecule singleElementMolecule = new([element]);
            if (_nuclearPlant.CalculateAllSingleReplacements(singleElementMolecule).Count > 0)
            {
                continue;
            }
            immutableElements.Add(element);
        }
        return immutableElements;
    }
}
