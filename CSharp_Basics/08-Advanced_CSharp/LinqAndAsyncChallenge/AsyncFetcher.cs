using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinqPractical;

// JSON shape (matches https://jsonplaceholder.typicode.com/users and sample-data.json).
public record ApiUser(int Id, string Name, string Username, string Email, Address? Address, Company? Company);
public record Address(string? City);
public record Company(string? Name);

/// <summary>
/// ASYNC EXERCISE. The boilerplate (models, HttpClient, JSON options) is provided.
/// You implement the two async methods so the pattern works:
///   network-first, and fall back to the bundled file on any failure.
///
/// Run `dotnet run -- fetch` to grade just this part.
/// Boilerplate you can rely on: Http, JsonOpts, ApiUrl, LocalFile.
/// </summary>
public sealed class AsyncFetcher
{
    public const string ApiUrl = "https://no-such-host.invalid/users";//"https://jsonplaceholder.typicode.com/users";
    public const string LocalFile = "sample-data.json";

    // Share one HttpClient for the app's lifetime (avoids socket exhaustion).
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(30) };

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    // TODO (A): Read + deserialize the bundled JSON file asynchronously.
    //   - Build the path: Path.Combine(AppContext.BaseDirectory, LocalFile),
    //     falling back to LocalFile if that doesn't exist (running from source).
    //   - Open the file and use JsonSerializer.DeserializeAsync<List<ApiUser>>(...).
    //   - Return an empty list if deserialization yields null. Honour the token.
    public static async Task<IReadOnlyList<ApiUser>> ReadLocalAsync(CancellationToken ct = default)
    {
        var pathToLocalFile = Path.Combine(AppContext.BaseDirectory, LocalFile);
        if (!File.Exists(pathToLocalFile))
        {
            pathToLocalFile = LocalFile;

        }

        if (!File.Exists(pathToLocalFile))
        {
            return Array.Empty<ApiUser>();
        }

        await using var fs = new FileStream(pathToLocalFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
        var result = await JsonSerializer.DeserializeAsync<List<ApiUser>>(fs, JsonOpts, cancellationToken: ct);

        return result ?? [];
    }

    // TODO (B): Try the API first; on ANY network/timeout/parse error, fall back to the file.
    //   - Use a linked CancellationTokenSource with CancelAfter(~5s) for a per-request timeout.
    //   - Http.GetFromJsonAsync<List<ApiUser>>(ApiUrl, JsonOpts, token).
    //   - catch HttpRequestException / TaskCanceledException / JsonException,
    //     then return await ReadLocalAsync(ct).
    public async Task<IReadOnlyList<ApiUser>> GetUsersAsync(CancellationToken ct = default)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);

        cts.CancelAfter(5000);

        try
        {
            var result = await Http.GetFromJsonAsync<List<ApiUser>>(ApiUrl, JsonOpts, cts.Token);

            return result ?? [];
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException)
        {
            return await ReadLocalAsync(ct);
        }
    }

    /// <summary>Grades the two methods above and runs sample LINQ over the result.</summary>
    public static async Task CheckAsync()
    {
        Console.WriteLine("=== ASYNC FETCHER CHECKER ===\n");
        var fetcher = new AsyncFetcher();

        IReadOnlyList<ApiUser> users;
        try
        {
            users = await fetcher.GetUsersAsync();
        }
        catch (NotImplementedException ex)
        {
            Console.WriteLine($"– {ex.Message}: not attempted");
            Console.WriteLine("  Implement ReadLocalAsync then GetUsersAsync in AsyncFetcher.cs.\n");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ GetUsersAsync threw {ex.GetType().Name}: {ex.Message}\n");
            return;
        }

        // Expected from sample-data.json: 5 users, ids 1..5.
        var ids = users.Select(u => u.Id).OrderBy(x => x).ToList();
        var idsOk = ids.SequenceEqual([1, 2, 3, 4, 5]);
        Console.WriteLine(idsOk
            ? $"✓ Fetched {users.Count} users (ids 1..5)"
            : $"✗ Expected 5 users with ids 1..5, got: [{string.Join(",", ids)}]");

        // LINQ over the fetched data (this part is done for you as a payoff).
        var topCompany = users
            .GroupBy(u => u.Company?.Name ?? "(unknown)")
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .First();
        var companyOk = topCompany is { Key: "Romaguera-Crona" } && topCompany.Count() == 2;
        Console.WriteLine(companyOk
            ? "✓ Grouping works (Romaguera-Crona has 2 users)"
            : $"✗ Grouping off: top company was {topCompany.Key} with {topCompany.Count()}");

        Console.WriteLine(idsOk && companyOk ? "\nAsync exercise: PASS\n" : "\nAsync exercise: keep going\n");
    }
}
