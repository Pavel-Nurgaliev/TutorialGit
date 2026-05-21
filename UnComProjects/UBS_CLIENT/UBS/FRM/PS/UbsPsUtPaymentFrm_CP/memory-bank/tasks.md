# Tasks

## Current Phase: NO ACTIVE TASK

The previous task (`UbsPsUtPaymentFrm` conversion) is **COMPLETE and ARCHIVED**.
Start the next task with `/van`.

## Last Completed Task

| Field | Value |
|-------|-------|
| **Task ID** | `ubspsutpaymentfrm` |
| **Title** | VB6 `UtPayment.dob` → .NET `UbsPsUtPaymentFrm` Conversion |
| **Complexity** | Level 4 — Complex System / Enterprise |
| **Status** | COMPLETE — ARCHIVED on 2026-05-21 |
| **Archive** | `memory-bank/archive/archive-ubspsutpaymentfrm.md` |
| **Comprehensive reflection** | `memory-bank/reflection/reflection-ubspsutpaymentfrm.md` |
| **Designer milestone reflection** | `memory-bank/reflection/reflection-ubspsutpaymentfrm-designer.md` |

The full plan, checklists, channel inventory, control mapping, partial-class
split, BUILD wave matrix, REFLECT findings, and lessons learned have been
merged into the archive document above and removed from this file in
accordance with the memory-bank paths convention.

## Carry-Forward Follow-Ups (not blocking ARCHIVE)

These items are recorded here so the next workflow cycle (or a build-capable
session for the same project) can pick them up without re-reading the archive.

| ID | Item | Type | Priority |
|----|------|------|----------|
| F-1 | Run full MSBuild verification on a host with .NET Framework 2.0 reference assemblies | Environmental | High |
| F-2 | UBS-host smoke test for ADD / VIEW / COPY / CHANGE_PART / GROUP_ADD / ADD_INCOMING happy-path saves | Integration | High |
| F-3 | Reconcile screenshot parity against `legacy-form/screens/1.png`–`6.png` once the form runs | Visual | Medium |
| F-4 | Optional `Payment_Save` happy-path integration test inside the PS host-level harness | Test | Medium |
| F-5 | Runtime performance profiling around `CalcSumCommiss` and `Payment_Save` | Performance | Low |
| F-6 | Promote the nine-partial split + five-doc CREATIVE bundle into a reusable PS-conversion template | Process | Medium |

See `memory-bank/archive/archive-ubspsutpaymentfrm.md` §"Unresolved Gaps /
Follow-Up Tasks" for the authoritative description of each item.
