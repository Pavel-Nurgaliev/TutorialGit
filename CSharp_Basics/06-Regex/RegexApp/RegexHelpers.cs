using System.Reflection;
using System.Text.RegularExpressions;

namespace RegexApp;

public static class RegexHelpers
{
    // Task 1
    public static bool IsValidEmail(string input)
    {
        var pattern = @"[a-z].*@\w*\..*";

        return Regex.IsMatch(input, pattern);
    }

    // Task 2
    public static IEnumerable<int> ExtractNumbers(string input)
    {
        var pattern = @"\d*";

        var values = Regex.Matches(input, pattern).Where(x => x.Value != string.Empty);

        var output = values.Select(x => Convert.ToInt32(x.Value));

        return output;
    }

    // Task 3
    public static bool IsValidPhoneNumber(string input)
    {
        string pattern1 = @"[+][7]\d{10}";
        string pattern2 = @"[8]\d{10}";
        string pattern3 = @"[+][7][-]\d{3}[-]\d{3}[-]\d{2}[-]\d{2}";

        return Regex.IsMatch(input, $"{pattern1}|({pattern2})|({pattern3})");
    }

    // Task 4: return (day, month, year) parsed from "DD.MM.YYYY", or null for any other format
    private const int AmountOfDataParams = 3;
    public static (int day, int month, int year)? ParseDate(string input)
    {
        var pattern = @"\.";
        var splitted = Regex.Split(input, pattern);

        var output = splitted.Length == AmountOfDataParams ? splitted.Select(x => Convert.ToInt32(x)).ToArray() : null;

        return output is null ? null : (output[0], output[1], output[2]);
    }

    // Task 5
    public static string CamelToSnakeCase(string input)
    {
        var pattern = @"(?=[A-Z])";
        var splitted = Regex.Split(input, pattern).Select(x => x.ToLower()).ToArray();

        var output = string.Join("_", splitted);

        return output;
    }

    // Task 6
    /*
     * Password must be at least 8 characters and contain:
     - At least one uppercase letter
     - At least one lowercase letter
     - At least one digit
     - At least one special character (!@#$%^&*...)
    Use lookaheads so the checks are independent of order.
    */
    public static bool IsValidPassword(string input)
    {
        var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$";

        return Regex.IsMatch(input, pattern);
    }
}
