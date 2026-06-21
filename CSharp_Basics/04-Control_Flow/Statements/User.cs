
internal class User
{
    public bool IsActive { get; internal set; }
    public string Role { get; internal set; }
    public bool HasTwoFactor { get; internal set; }
}