public class SemanticVersion : IEquatable<SemanticVersion>, IComparable<SemanticVersion>
{
    public SemanticVersion(int major, int minor, int patch)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
    }
    public int Major { get; }
    public int Minor { get; }
    public int Patch { get; }

    //Sort relies on the following contract:
    //1. reflexivity - element must always relate to itself (this.CompareTo(this) should return 0)
    //2. transitivity - relationships with first and second element and with second and third and with first and third (A>B B>C A>C)
    //3. antisymmetry - different elements cannot relate to each other in same direction. If A>B then B should be <A
    // CompareTo is consistent with Equals - both compare Major->Minor->Patch. So CompareTo == 0 - Equals is true. But if it is incosistent, other consumers as SortedSet, BinarySearch would silently misbehave
    public int CompareTo(SemanticVersion? other)
    {
        if (other is null)
        {
            return 1;
        }

        var result = this.Major.CompareTo(other.Major);

        if (result != 0)
        {
            return result;
        }

        result = this.Minor.CompareTo(other.Minor);

        if (result != 0)
        {
            return result;
        }

        result = this.Patch.CompareTo(other.Patch);

        return result;
    }


    public bool Equals(SemanticVersion? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        return this.Major == other.Major && this.Minor == other.Minor && this.Patch == other.Patch;
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj.GetType() != typeof(SemanticVersion))
        {
            return false;
        }

        return Equals(obj as SemanticVersion);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Major, this.Minor, this.Patch);
    }
}
