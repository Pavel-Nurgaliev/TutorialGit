# Memory Bank: Product Context

## Purpose

`UbsPsUtPaymentFrm` is the .NET replacement for the legacy VB6 UtPayment form in the UBS PS area. It should preserve the original operator workflow and visual expectations while fitting the existing UBS WinForms infrastructure.

## User Expectations

- Operators should recognize the legacy form layout and workflow.
- Child dialogs should behave consistently with the VB6 originals.
- Validation, loading, and saving should remain predictable after migration.

## Conversion Priorities

- Preserve business behavior before polishing structure.
- Match the legacy look using `legacy-form/screens/`.
- Reuse proven patterns from neighboring successful conversions.
- Avoid framework upgrades or modern APIs incompatible with .NET 2.0.
