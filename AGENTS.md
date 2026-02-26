## Cursor Cloud specific instructions

### Codebase overview

This is a multi-project learning workspace containing:

- **.NET 8.0 console apps** — `Collections`, `LinqTasks`, `AdvancedLinq`, `LinqSimple/Features`, `EnumeratorProj` (all under `.NET projects/`)
- **BethanysPieShop** — ASP.NET Core MVC 8.0 web app with EF Core + SQL Server (`.NET projects/BethanysPieShop/`)
- **HTML/CSS static sites** — Bootstrap, Uber clone, WordPress clone, etc. (under `HTML/`)
- **LinqSamples** — targets .NET Framework 4.8, cannot build on Linux (`.NET projects/LearningProjects/LinqSamples/`)
- **UBS WinForms projects** — target .NET Framework 2.0, require Windows + proprietary DLLs, cannot build on Linux (`UnComProjects/`)

No test frameworks, CI/CD, linters, or package managers (npm/pip) exist in this repo. Dependency management is via NuGet through `.csproj` files.

### Prerequisites (already installed by VM snapshot)

- .NET 8.0 SDK at `$HOME/.dotnet` (PATH/DOTNET_ROOT set in `~/.bashrc`)
- Docker with SQL Server 2022 container (`mcr.microsoft.com/mssql/server:2022-latest`)
- `fuse-overlayfs` and `iptables-legacy` configured for nested Docker

### Running projects

**Console apps** — build and run with `dotnet run --project "<path>.csproj"`:
- `.NET projects/LearningProjects/Collections/Collections.csproj`
- `.NET projects/LearningProjects/LinqTasks/LinqTasks.csproj`
- `.NET projects/LearningProjects/AdvancedLinq/AdvancedLinq.csproj`
- `.NET projects/LinqTutorial/LinqSimple/Features.csproj`
- `.NET projects/LinqTutorial/EnumeratorProj/EnumeratorProj.csproj`

**BethanysPieShop** — requires SQL Server. Start the Docker container first:
```
docker start sqlserver || docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStr0ng!Pass" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```
The app's `appsettings.json` connection string points to `Data Source=PAVEL` (author's Windows machine). Override it for local dev:
```
ConnectionStrings__DefaultConnection="Server=localhost,1433;Database=BethanysPieShop;User Id=sa;Password=YourStr0ng!Pass;TrustServerCertificate=True"
```
**Note:** As of the current codebase state, BethanysPieShop has a pre-existing compile error — duplicate `IShoppingCart` interface in both `Models/Repositories/IShoppingCart.cs` and `Models/ShoppingCart/IShoppingCart.cs`. This must be fixed in the code before the app can build.

**HTML static sites** — serve with `python3 -m http.server 8080` from the `HTML/` directory.

### Non-buildable on Linux

- `LinqSamples` solution (`.NET projects/LearningProjects/LinqSamples/`) — targets .NET Framework 4.8
- `UnComProjects/` — Windows Forms, .NET Framework 2.0, proprietary DLL dependencies
