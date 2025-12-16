using System.Numerics;

namespace AoC.Days;
internal class DinnerGuest
{
    public string Name { get; set; } = String.Empty;
    public Dictionary<string, int> Preferences { get; } = [];

    public DinnerGuest(string name, Dictionary<string, int> preferences)
    {
        Name = name;
        Preferences = preferences;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DinnerGuest other) return false;
        return this.Equals(other);
    }

    public bool Equals(DinnerGuest? otherGuest)
    {
        if (ReferenceEquals(this, otherGuest))
        { 
            return true; 
        }
        if (ReferenceEquals(otherGuest, null)) 
        {
            return false; 
        }
        
        return Name.Equals(otherGuest.Name);
    }
    
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public static bool operator ==(DinnerGuest? first, DinnerGuest? second)
    {
        if (ReferenceEquals(first, second))
        {
            return true;
        }
        if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
        {
            return false;
        }
        return first.Equals(second);
    }

    public static bool operator !=(DinnerGuest? first, DinnerGuest? second)
    {
        return !(first == second);
    }
}
