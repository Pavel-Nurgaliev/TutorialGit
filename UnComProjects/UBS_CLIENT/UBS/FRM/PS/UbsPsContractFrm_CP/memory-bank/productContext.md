# Memory Bank: Product Context

## Product

UBS client form for **PS Contract** — portfolio/back-office contract entry and maintenance, hosted in the UniSAB shell with channel-driven business logic.

## Users / Stakeholders

UBS operators and developers maintaining PS contract workflows.

## Constraints

- **.NET Framework 2.0** — no LINQ; match existing UBS form patterns (`UbsFormBase`, explicit channel API).
- Russian UI text and Windows-1251 compatibility as in other UBS forms.
- Designer: preserve template structure (`panelMain`, table layout height per `UbsFormTemplate` / `designer-rules`).
