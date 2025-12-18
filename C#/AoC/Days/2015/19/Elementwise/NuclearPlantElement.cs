namespace AoC.Days;

internal class NuclearPlantElement
{
    public readonly Molecule MedicineMolecule = new([]);
    public readonly Dictionary<string, List<Molecule>> Replacements = [];

    public NuclearPlantElement(string[] inputLines)
    {
        for (int lineIndex = 0; lineIndex < inputLines.Length - 1; lineIndex++)
        {
            string line = inputLines[lineIndex];
            if (string.IsNullOrEmpty(line))
            {
                string MedicineMoleculeString = inputLines[++lineIndex];
                MedicineMolecule = ConvertStringToMolecule(MedicineMoleculeString);
                break;
            }

            string[] fromTo = line.Split(" => ");
            Molecule toMolecule = ConvertStringToMolecule(fromTo[1]);
            if (Replacements.TryGetValue(fromTo[0], out List<Molecule>? replacementList))
            {
                replacementList.Add(toMolecule);
            }
            else
            {
                Replacements.Add(fromTo[0], [toMolecule]);
            }
        }
    }

    public HashSet<Molecule> CalculateAllSingleReplacements(Molecule molecule)
    {
        HashSet<Molecule> singleReplacements = [];
        for (int elementIndex = 0; elementIndex < molecule.Length; elementIndex++)
        {
            var elementReplacements = CalculateReplacements(molecule, elementIndex);
            singleReplacements.UnionWith(elementReplacements);
        }
        return singleReplacements;
    }

    private HashSet<Molecule> CalculateReplacements(Molecule molecule, int elementIndex)
    {
        HashSet<Molecule> replacementSet = [];
        string candidateElement = molecule.Elements[elementIndex];

        if (Replacements.TryGetValue(candidateElement, out List<Molecule>? replacementMolecules))
        {
            foreach (var replacementMolecule in replacementMolecules)
            {
                string[] newElements = new string[molecule.Length + replacementMolecule.Length - 1];
                Span<string> firstPart = molecule.Elements[..elementIndex].AsSpan();
                Span<string> secondPart = [];
                if (elementIndex < molecule.Length)
                {
                    secondPart = molecule.Elements[(elementIndex + 1)..].AsSpan();
                }
                int offset = 0;
                firstPart.CopyTo(newElements.AsSpan(offset));
                offset += firstPart.Length;

                replacementMolecule.Elements.CopyTo(newElements.AsSpan(offset));
                offset += replacementMolecule.Length;

                secondPart.CopyTo(newElements.AsSpan(offset));
                replacementSet.Add(new(newElements));
            }
        }
        return replacementSet;
    }

    private static Molecule ConvertStringToMolecule(string moleculeString)
    {
        List<string> elementList = [];
        string currentElement = "";
        for (int charIndex = 0; charIndex < moleculeString.Length; charIndex++)
        {
            currentElement += moleculeString[charIndex];
            if (charIndex == moleculeString.Length - 1)
            {
                elementList.Add(currentElement);
                break;
            }

            if (char.IsUpper(moleculeString[charIndex + 1]))
            {
                elementList.Add(currentElement);
                currentElement = "";
            }
            else
            {
                continue;
            }

        }
        return new([.. elementList]);
    }
}
