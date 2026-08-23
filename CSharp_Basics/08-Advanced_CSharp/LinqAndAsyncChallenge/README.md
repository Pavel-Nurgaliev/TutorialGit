# LINQ Practice Kit + Async Exercise (.NET 8)

A **training** project, not a solutions manual. The 10 LINQ challenges and the
async fetcher are empty stubs for *you* to implement. A built-in checker grades
your work and shows a diff on failure — but never the query.

## Loop

1. Open `Challenges.cs`, pick a challenge, replace `throw new NotImplementedException(...)`
   with your LINQ query so it returns the described data.
2. Run the checker:
   ```bash
   dotnet run            # grade LINQ challenges + async fetcher
   dotnet run -- linq    # grade only the 10 LINQ challenges
   dotnet run -- fetch   # grade only the async fetcher
   ```
   In Visual Studio just press Ctrl+F5 (set the arg in Debug ▸ launch profile).
3. Read the ✓ PASS / ✗ FAIL / – not-attempted report. On FAIL you get expected
   vs. your output to debug the LOGIC.
4. Stuck on one? Open `SOLUTIONS.md` — after you've tried.

The dataset (customers, products, orders) is in `Data.cs`. Each challenge's
comment states the exact ordering/tiebreaks the checker expects, since it
compares sequences in order.

## What each challenge trains

| # | Skill | Operators to reach for |
|---|-------|------------------------|
| 1 | Filter + project | `Where` `OrderBy` `Select` |
| 2 | Aggregation | `Count` `Average` `Min` `Max` |
| 3 | Group + count | `GroupBy` `Count` `OrderByDescending` `ThenBy` |
| 4 | Multi-key order + join | `join` `OrderBy` `ThenByDescending` |
| 5 | Inner join | `Join` |
| 6 | Flatten | `SelectMany` `GroupBy` `Sum` |
| 7 | Set operations | `Distinct` `Except` |
| 8 | Reporting | `SelectMany` + 2× `Join` + `GroupBy` `Sum` |
| 9 | Top-N per group | `GroupBy` → `OrderByDescending().Take(2)` |
| 10 | Running total | ordered projection carrying state |

## Async exercise (`AsyncFetcher.cs`)

Implement two methods; the boilerplate (models, `HttpClient`, JSON options) is
given:

- **A · `ReadLocalAsync`** — read + deserialize `sample-data.json` asynchronously.
- **B · `GetUsersAsync`** — try the API with a per-request timeout
  (`CancellationTokenSource.CancelAfter`), and fall back to `ReadLocalAsync` on
  any network/parse error.

With no internet the API simply triggers your fallback, so it grades anywhere.

## Files

- `Challenges.cs` — the 10 stubs **(your work)**
- `AsyncFetcher.cs` — async stubs **(your work)**
- `Data.cs` — dataset · `Results.cs` — return types
- `Checker.cs` — the auto-grader · `Program.cs` — entry point
- `sample-data.json` — fallback data · `SOLUTIONS.md` — answer key (last resort)

## Notes

- No NuGet packages needed; `nuget.config` clears sources for offline restore.
  Delete it if you later add packages.
- Targets `net8.0`. To use a newer SDK, change `<TargetFramework>` in the .csproj.
