namespace AoC.Days;

internal class FabricationNodeElement(Molecule molecule, int steps)
{
    public Molecule Molecule { get; } = molecule;
    public int Steps { get; } = steps;
}
