# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Purpose

This is a C# Basics training project — lesson 06 on Regular Expressions. The goal is to practice `System.Text.RegularExpressions.Regex` by implementing methods in `RegexHelpers.cs` against pre-written unit tests.

## Commands

Build the main project:
```
dotnet build RegexApp.csproj
```

Run the console app:
```
dotnet run --project RegexApp.csproj
```

Run all tests:
```
dotnet test ..\RegexApp.Tests\RegexApp.Tests.csproj
```

Run tests filtered by name:
```
dotnet test ..\RegexApp.Tests\RegexApp.Tests.csproj --filter "DisplayName~IsValidEmail"
```

## Project structure

```
06-Regex/
├── RegexApp/               ← working directory (console app, .NET 10)
│   ├── Program.cs          ← task descriptions as comments; demo calls
│   └── RegexHelpers.cs     ← implement these static methods to pass the tests
└── RegexApp.Tests/         ← xUnit test project that validates RegexHelpers
    └── RegexHelpersTests.cs
```

## Tasks

Each task is a public static method in `RegexApp.RegexHelpers`. Implement the method body using `System.Text.RegularExpressions.Regex` to make its tests pass:

| # | Method | Concept |
|---|--------|---------|
| 1 | `IsValidEmail` | Basic pattern matching (`Regex.IsMatch`) |
| 2 | `ExtractNumbers` | Finding all matches (`Regex.Matches`) |
| 3 | `IsValidPhoneNumber` | Alternation and anchors |
| 4 | `ParseDate` | Named capture groups (`(?<name>...)`) |
| 5 | `CamelToSnakeCase` | Replace with back-references |
| 6 | `IsValidPassword` | Positive lookaheads (`(?=...)`) |

## Conventions from earlier lessons

- All projects target `.NET 10` with `ImplicitUsings` and `Nullable` enabled.
- Helper logic lives in separate `.cs` files; `Program.cs` uses top-level statements.
- No input validation unless the task explicitly requires it.
