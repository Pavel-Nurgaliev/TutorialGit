# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

An exercise from the `07-Interfaces` section of a C# learning repo. The full assignment lives as a comment block at the top of `Program.cs` — read it first; it is the spec any change must satisfy. Goal: use an interface (`IActivity`) to build an *extensible* workflow engine where new behavior is added by creating new classes, not by editing existing ones.

## Commands

Run from this directory (`CSharp_Basics/07-Interfaces/WorkflowEngineApp`):

```
dotnet run      # build + execute
dotnet build    # compile only
```

Targets `net10.0` with `ImplicitUsings` and `Nullable` enabled. There is no test project — verification is done by running and reading console output.

## Architecture

The exercise deliberately splits responsibilities across three roles:

- **`IActivity`** — the extension point. One method, `Execute()`. Every concrete step (`FetchEmployeeDataActivity`, `AwaitManagerApprovalActivity`, `SendConfirmationEmailActivity`) implements it and only does `Console.WriteLine(...)`.
- **`Workflow`** — an ordered container of `IActivity` (`AddActivity` / `GetActivities`). Knows nothing about concrete activities.
- **`WorkflowEngine.Run(workflow)`** — iterates activities and calls `Execute()` on each. Depends only on the interface, never on concrete types. This is the polymorphism the exercise is teaching.

`Program.cs` is the composition root: it builds a `Workflow`, adds activities, and hands it to the engine.

## Constraints that keep the exercise "correct"

- The engine must stay ignorant of concrete activity types — adding a new activity should require **only** a new class implementing `IActivity`, with no edits to `WorkflowEngine` or `Workflow`. If a review reveals otherwise, that's the bug.
- Activities stay trivial (`Console.WriteLine`) on purpose; don't add real I/O — the point is the interface design, not the steps.
