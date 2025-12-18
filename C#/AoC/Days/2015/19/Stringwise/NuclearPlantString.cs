namespace AoC.Days;

internal class NuclearPlantString
{
    public readonly string MedicineMolecule = String.Empty;
    private readonly Dictionary<string, List<string>> Replacements = [];
    
    public NuclearPlantString(string[] inputLines)
    {
        for (int lineIndex = 0; lineIndex < inputLines.Length - 1; lineIndex++)
        {
            string line = inputLines[lineIndex];
            if (string.IsNullOrEmpty(line))
            {
                MedicineMolecule = inputLines[++lineIndex];
                break;
            }

            string[] fromTo = line.Split(" => ");
            if (Replacements.TryGetValue(fromTo[0], out List<string>? replacementList))
            {
                replacementList.Add(fromTo[1]);
            }
            else
            {
                Replacements.Add(fromTo[0], [fromTo[1]]);
            }
        }
    }

    public HashSet<string> CalculateAllSingleReplacements(string molecule)
    {
        HashSet<string> singleReplacements = [];
        int maxReplaceableLength = 0;
        foreach(var replaceableString in Replacements.Keys)
        {
            maxReplaceableLength = replaceableString.Length > maxReplaceableLength ? replaceableString.Length : maxReplaceableLength;
        }

        for (int charIndex = 0; charIndex < molecule.Length; charIndex++)
        {
            for(int candidateLength = 1; candidateLength <= maxReplaceableLength;  candidateLength++)
            {
                var replacements = CalculateReplacements(molecule, charIndex, candidateLength);
                singleReplacements.UnionWith(replacements);
            }
        }
        return singleReplacements;
    }

    private HashSet<string> CalculateReplacements(string molecule, int charIndex, int candidateLength)
    {
        HashSet<string> replacementSet = [];
        if (candidateLength + charIndex > molecule.Length)
        {
            return replacementSet;
        }
        string candidateString = molecule.Substring(charIndex, candidateLength);

        if (Replacements.TryGetValue(candidateString, out List<string>? replacementTypes))
        {
            foreach (var replacementType in replacementTypes)
            {
                string firstPart = molecule[..charIndex];
                string secondPart = "";
                if (candidateLength + charIndex < molecule.Length)
                {
                    secondPart = molecule[(charIndex + candidateLength)..];
                }
                replacementSet.Add(firstPart + replacementType + secondPart);
            }
        }
        return replacementSet;
    }
}
