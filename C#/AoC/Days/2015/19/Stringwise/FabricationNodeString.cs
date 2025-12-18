using System.Security.Cryptography;

namespace AoC.Days;
internal class FabricationNodeString(string molecule, int steps)
{
    public string Molecule { get; } = molecule;
    public int Steps { get; } = steps;
}
