# Memory Bank: Technical Context

## Technology Stack
- **Language:** C# (.NET Framework 2.0)
- **UI Framework:** Windows Forms
- **Platform:** Windows
- **Project Type:** Class Library (DLL)
- **Build System:** MSBuild

## Dependencies
- **UbsBase.dll** - Base UBS functionality
- **UbsChannel.dll** - Channel communication
- **UbsCollections.dll** - Collection utilities
- **UbsCtrlInfo.dll** - Control information
- **UbsForm.dll** - Form base classes
- **UbsFormBase.dll** - Base form class (inherited by UbsBgContractFrm)
- **UbsInterface.dll** - Interface definitions
- **UbsService** - Service layer (namespace reference)

All UBS assemblies located in: `C:\ProgramData\UniSAB\Assembly\Ubs\`

## Development Environment
- **OS:** Windows 10 (Build 26100)
- **IDE:** Visual Studio / Cursor
- **Shell:** PowerShell
- **Project Path:** `d:\UnComProjects\UBS_CLIENT\UBS\FRM\BG\UbsBgContractFrm_CP`

## Build & Deployment
- **Output Type:** Library (DLL)
- **Target Framework:** .NET Framework 2.0
- **Assembly Name:** UbsBgContractFrm
- **Namespace:** UbsBusiness
- **Build Configurations:** Debug, Release

## Infrastructure
- Part of larger UBS (UniSAB) client system
- Uses resource-based channel communication: `ASM:UBS_ASM\\Business\\DllName.dll->UbsBusiness.NameClass`
- Implements IUbs interface for command handling
