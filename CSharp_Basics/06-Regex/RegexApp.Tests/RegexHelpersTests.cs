using RegexApp;
using Xunit;

namespace RegexApp.Tests;

public class RegexHelpersTests
{
    // Task 1 — IsValidEmail
    [Theory]
    [InlineData("user@example.com", true)]
    [InlineData("user.name+tag@sub.domain.org", true)]
    [InlineData("invalid-email", false)]
    [InlineData("@domain.com", false)]
    [InlineData("user@", false)]
    [InlineData("user@domain", false)]
    public void IsValidEmail_ReturnsExpected(string input, bool expected)
    {
        Assert.Equal(expected, RegexHelpers.IsValidEmail(input));
    }

    // Task 2 — ExtractNumbers
    [Theory]
    [InlineData("abc 42 def 7 xyz 100", new[] { 42, 7, 100 })]
    [InlineData("no numbers here", new int[] { })]
    [InlineData("1 and 2 and 3", new[] { 1, 2, 3 })]
    public void ExtractNumbers_ReturnsAllNumbers(string input, int[] expected)
    {
        Assert.Equal(expected, RegexHelpers.ExtractNumbers(input));
    }

    // Task 3 — IsValidPhoneNumber
    [Theory]
    [InlineData("+79161234567", true)]
    [InlineData("89161234567", true)]
    [InlineData("+7-916-123-45-67", true)]
    [InlineData("9161234567", false)]
    [InlineData("+12345678901", false)]
    [InlineData("phone", false)]
    public void IsValidPhoneNumber_ReturnsExpected(string input, bool expected)
    {
        Assert.Equal(expected, RegexHelpers.IsValidPhoneNumber(input));
    }

    // Task 4 — ParseDate (valid)
    [Theory]
    [InlineData("25.12.2024", 25, 12, 2024)]
    [InlineData("01.01.2000", 1, 1, 2000)]
    public void ParseDate_ValidDate_ReturnsComponents(string input, int day, int month, int year)
    {
        var result = RegexHelpers.ParseDate(input);
        Assert.NotNull(result);
        Assert.Equal((day, month, year), result!.Value);
    }

    // Task 4 — ParseDate (invalid)
    [Theory]
    [InlineData("2024-12-25")]
    [InlineData("not a date")]
    [InlineData("25/12/2024")]
    public void ParseDate_InvalidFormat_ReturnsNull(string input)
    {
        Assert.Null(RegexHelpers.ParseDate(input));
    }

    // Task 5 — CamelToSnakeCase
    [Theory]
    [InlineData("camelCase", "camel_case")]
    [InlineData("myVariableName", "my_variable_name")]
    [InlineData("alreadylower", "alreadylower")]
    [InlineData("firstName", "first_name")]
    public void CamelToSnakeCase_ReturnsSnakeCase(string input, string expected)
    {
        Assert.Equal(expected, RegexHelpers.CamelToSnakeCase(input));
    }

    // Task 6 — IsValidPassword
    [Theory]
    [InlineData("P@ssw0rd", true)]
    [InlineData("Abc1!23X", true)]
    [InlineData("Abc1!2X", false)]       // 7 chars — too short
    [InlineData("abc1!23x", false)]      // no uppercase
    [InlineData("ABC1!23X", false)]      // no lowercase
    [InlineData("Abcdef!X", false)]      // no digit
    [InlineData("Abcdef1X", false)]      // no special character
    public void IsValidPassword_ReturnsExpected(string input, bool expected)
    {
        Assert.Equal(expected, RegexHelpers.IsValidPassword(input));
    }
}
