# Memory Bank: Active Context

## Current Focus

**No active task.** The previous task (`UbsPsUtPaymentFrm` conversion) is complete
and archived. Start the next task with **`/van`**.

## Status

- **VAN:** —
- **PLAN:** —
- **CREATIVE:** —
- **BUILD:** —
- **REFLECT:** —
- **ARCHIVE:** —

## Last Completed Task

| Field | Value |
|-------|-------|
| **Task ID** | `ubspsutpaymentfrm` |
| **Title** | VB6 `UtPayment.dob` → .NET `UbsPsUtPaymentFrm` Conversion |
| **Complexity** | Level 4 — Complex System / Enterprise |
| **Completed** | 2026-05-21 |
| **Archive** | `memory-bank/archive/archive-ubspsutpaymentfrm.md` |
| **Reflections** | `memory-bank/reflection/reflection-ubspsutpaymentfrm.md`, `memory-bank/reflection/reflection-ubspsutpaymentfrm-designer.md` |

## Carry-Forward Follow-Ups (not blocking)

Recorded in `memory-bank/tasks.md` (summary) and
`memory-bank/archive/archive-ubspsutpaymentfrm.md` §"Unresolved Gaps" (detailed):

- **F-1** Run full MSBuild verification on a .NET Framework 2.0–capable host.
- **F-2** UBS-host smoke test for ADD / VIEW / COPY / CHANGE_PART / GROUP_ADD /
  ADD_INCOMING happy-path saves.
- **F-3** Reconcile screenshot parity against `legacy-form/screens/1.png`–`6.png`.
- **F-4** Optional `Payment_Save` happy-path integration test.
- **F-5** Runtime performance profiling around `CalcSumCommiss` and
  `Payment_Save`.
- **F-6** Promote the nine-partial split + five-doc CREATIVE bundle into a
  reusable PS-conversion template.

## Stable References (kept for the next task)

- Pattern projects (sibling PS conversions): `UbsPsUtPaymentGroupFrm_CP`,
  `UbsPsContractFrm_CP`, and now `UbsPsUtPaymentFrm_CP` itself.
- Workspace rules (apply to every PS conversion):
  `.cursor/rules/array-rule.mdc`, `.cursor/rules/designer-rules.mdc`,
  `.cursor/rules/style-rule.mdc`,
  `.cursor/rules/isolation_rules/Core/memory-bank-paths.mdc`.
- Memory-bank workflow: `/van` → `/plan` → `/creative` → `/build` →
  `/reflect` → `/archive`.

## Next Step

Invoke **`/van`** with the next legacy form / module / task to begin a new
conversion cycle.
