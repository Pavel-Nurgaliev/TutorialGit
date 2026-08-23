using System.Globalization;
using LinqPractical;

// Deterministic formatting for any output.
CultureInfo.CurrentCulture = new CultureInfo("en-US");

// Grade your work:
//   dotnet run            -> check LINQ challenges + async fetcher
//   dotnet run -- linq    -> only the 10 LINQ challenges
//   dotnet run -- fetch   -> only the async fetcher
var mode = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (mode)
{
    case "linq":
        Checker.RunLinq();
        break;
    case "fetch":
        await AsyncFetcher.CheckAsync();
        break;
    default:
        Checker.RunLinq();
        Console.WriteLine(new string('-', 60) + "\n");
        await AsyncFetcher.CheckAsync();
        break;
}
