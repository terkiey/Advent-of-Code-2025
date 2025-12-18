using System.Numerics;

namespace AoC.Days;
internal class Molecule
{
    public string[] Elements { get; } = [];
    public int Length => Elements.Length;

    public Molecule(string[] elements)
    {
        Elements = elements;
    }

    public bool Equals(Molecule other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return Elements.SequenceEqual(other.Elements);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj is null || obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((Molecule)obj);
    }

    public static bool operator ==(Molecule lhs, Molecule rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Molecule lhs, Molecule rhs)
    {
        return !lhs.Equals(rhs);
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        foreach (var element in Elements)
        {
            hash.Add(element);
        }
        return hash.ToHashCode();
    }
}
