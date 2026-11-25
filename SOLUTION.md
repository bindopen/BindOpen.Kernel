# BindOpen.Kernel — Solution Guide

This document describes the structure, projects, and workflow of the **BindOpen.Kernel**
repository. For end-user documentation (installation, usage), see [Readme.md](Readme.md).

## Repository Layout

```text
BindOpen.Kernel/
├── BindOpen.Kernel.sln        # Main solution (21 projects: src + tests + docs)
├── Readme.md                  # Public README
├── SOLUTION.md                # This file
├── TODO.md                    # Development roadmap
├── CREDITS.txt                # Contributors and acknowledgements
├── GitVersion.yml             # GitVersion configuration (versioning)
├── LICENSE                    # MIT license
├── .editorconfig              # Editor settings (C#, TS, etc.)
├── .gitattributes             # Git file attributes
├── .gitignore                 # Git ignore rules
├── src/                       # .NET source projects (13) + TypeScript package
│   ├── Data/                  # BindOpen.Data
│   ├── Scoping/               # BindOpen.Scoping
│   ├── Scoping.Extensions/    # BindOpen.Scoping.Extensions
│   ├── Scoping.Script/        # BindOpen.Scoping.Script
│   ├── Dtos/                  # BindOpen.Dtos
│   ├── Dtos.Xsd/              # BindOpen.Dtos.Xsd
│   ├── Logging/               # BindOpen.Logging
│   ├── Logging.Dtos/          # BindOpen.Logging.Dtos
│   ├── Relational/            # BindOpen.Relational
│   ├── Relational.Models/     # BindOpen.Relational.Models
│   ├── Logging.Relational/    # BindOpen.Logging.Relational
│   ├── Logging.Relational.Models/ # BindOpen.Logging.Relational.Models
│   ├── Hosting/               # BindOpen.Hosting
│   ├── Relational.Converters/ # Shared converter sources (no dedicated .csproj)
│   └── @kernel-dtos/          # @bindopen/kernel-dtos (TypeScript)
├── tests/                     # NUnit test projects (7)
│   ├── Data.Tests/
│   ├── Scoping.Tests/
│   ├── Dtos.Tests/
│   ├── Relational.Tests/
│   ├── Logging.Tests/
│   ├── Logging.Dtos.Tests/
│   └── Hosting.Tests/
└── docs/                      # DocFX documentation project + markdown guides
    ├── BindOpen.Kernel.Docs.csproj
    ├── bindopen-kernel-achitecture.md
    ├── bindopen-kernel-data.md
    ├── bindopen-kernel-dtos.md
    ├── bindopen-kernel-scoping.md
    ├── bindopen-kernel-relational.md
    └── Current-Release-Note.md
```

## Solution Projects

All C# projects (source, test and docs) target **.NET 11.0** (`net11.0`).
The TypeScript package targets ES2022 and is built to ESM + CJS.

### Source projects (`src/`)

| Directory | NuGet package | Role |
|-----------|---------------|------|
| `src/Data` | `BindOpen.Data` | Core data model: meta sets, meta objects, scalars, dictionaries, depots, references, conditions, data types. |
| `src/Scoping` | `BindOpen.Scoping` | Scope creation and extension loading (`BdoScoping`, `IBdoScope`, `ExtensionLoadOptions`). |
| `src/Scoping.Extensions` | `BindOpen.Scoping.Extensions` | Extension runtime: tasks (`IBdoTask`, `scope.CreateTask`), extension kinds, load options. |
| `src/Scoping.Script` | `BindOpen.Scoping.Script` | Scripting: `BdoScript`, `BdoScriptword`, interpreter, script tasks. |
| `src/Dtos` | `BindOpen.Dtos` | DTO model + JSON / XML / text converters and helpers (`XmlHelper`, `JsonHelper`, `ToDto`/`ToPoco`). |
| `src/Dtos.Xsd` | `BindOpen.Dtos.Xsd` | XSD schema generation for the DTO model. |
| `src/Logging` | `BindOpen.Logging` | Event logging: `BdoLog`, `IBdoLogEvent`, handlers, filters. |
| `src/Logging.Dtos` | `BindOpen.Logging.Dtos` | DTOs for log events and log queries. |
| `src/Relational` | `BindOpen.Relational` | Relational persistence of the core model (EF Core). |
| `src/Relational.Models` | `BindOpen.Relational.Models` | EF entity models and AutoMapper mapping for the core model. |
| `src/Logging.Relational` | `BindOpen.Logging.Relational` | Relational persistence of logs (EF Core + AutoMapper). |
| `src/Logging.Relational.Models` | `BindOpen.Logging.Relational.Models` | EF entity models for logs. |
| `src/Hosting` | `BindOpen.Hosting` | Application hosting and configuration binding. |
| `src/@kernel-dtos` | `@bindopen/kernel-dtos` (npm) | Typed DTO classes for JS/TS consumers; subpath exports `./data`, `./logging`, `./scoping`. |

### Test projects (`tests/`)

| Project | Tests |
|---------|-------|
| `tests/Data.Tests` | `BindOpen.Data` |
| `tests/Scoping.Tests` | `BindOpen.Scoping` (and extensions/scripting) |
| `tests/Dtos.Tests` | `BindOpen.Dtos` serialization round-trips |
| `tests/Relational.Tests` | `BindOpen.Relational` persistence (SQLite) |
| `tests/Logging.Tests` | `BindOpen.Logging` (with Serilog sinks) |
| `tests/Logging.Dtos.Tests` | `BindOpen.Logging.Dtos` |
| `tests/Hosting.Tests` | `BindOpen.Hosting` (with JSON configuration) |

Tests use **NUnit** (`NUnit` + `NUnit3TestAdapter` + `Microsoft.NET.Test.Sdk`),
with **Bogus** (fake data), **FluentAssertions** / **DeepEqual** (assertions)
and **SQLite** (`Microsoft.EntityFrameworkCore.Sqlite`) for persistence scenarios.

### Documentation project (`docs/`)

`docs/BindOpen.Kernel.Docs.csproj` builds the static documentation with **DocFX**
(metadoc + docfx CLI), publishing the markdown guides plus the generated API reference.
## Dependencies

Internal project references (compiled from source):

```text
Data ──────────────┐
                   ├── Scoping.Extensions ── Scoping.Script ──┬── Dtos
                   │                                          ├── Scoping
                   │                                          └── Relational.Models
Logging ────────────┬── Logging.Dtos
                   ├── Logging.Relational.Models
                   ├── Logging.Relational
                   └── Hosting
Relational.Models ── Relational
```

External dependencies:

| Package | Used by |
|---------|---------|
| `Microsoft.EntityFrameworkCore.Relational` | Relational, Relational.Models, Logging.Relational(.Models) |
| `AutoMapper` | Relational.Models, Logging.Relational |
| `Microsoft.Extensions.Configuration.Binder` / `*FileExtensions` / `*Json` | Hosting (and Hosting.Tests) |
| Published `BindOpen.*` NuGet packages (pinned versions) | Logging, Logging.Dtos, Hosting, Logging.Relational* (consumed from the feed where applicable) |
| `NUnit`, `NUnit3TestAdapter`, `Microsoft.NET.Test.Sdk`, `Bogus`, `FluentAssertions`, `DeepEqual`, `Serilog.*`, `Microsoft.EntityFrameworkCore.Sqlite` | Test projects |
| `DocFX` tooling | docs project |
| `typescript`, `tsup` (dev) | `src/@kernel-dtos` |

> **Note:** `src/Relational.Converters/` contains shared C# source files (e.g.
> `BdoAssemblyReferenceIOConverter.cs`) but **no `.csproj`**. These files are
> compiled by the projects that link them; do not add it as a standalone
> project reference.

## Build & Test

### Prerequisites

- [.NET 11.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org) + npm (only for the TypeScript package)

### Build

```bash
# Restore + build the full solution
dotnet build BindOpen.Kernel.sln

# Or a single project
dotnet build src/Data/Data.csproj
```

### Run tests

```bash
# All tests
dotnet test BindOpen.Kernel.sln

# A single test project
dotnet test tests/Data.Tests

# Filter by name
dotnet test tests/Scoping.Tests --filter "FullyQualifiedName~ExtensionLoader"
```

### Publish documentation

```bash
# Build the DocFX site
dotnet run --project docs/BindOpen.Kernel.Docs.csproj
```

### TypeScript package

```bash
cd src/@kernel-dtos
npm install
npm run build    # tsup -> ESM + CJS bundles in dist/
```
## Code Style & Conventions

- C# follows the repository's `.editorconfig` (file-scoped namespaces, nullable enabled).
- Namespaces: `BindOpen.<Package>` for public API (e.g. `BindOpen.Data`, `BindOpen.Scoping`).
- Factory entry points are static partial classes: `BdoData`, `BdoScoping`, `BdoLog`, `BdoScript`.
- XML documentation comments on all public members.
- Commit messages: [Conventional Commits](https://www.conventionalcommits.org/).
- Branches: feature branches → `main` via pull request.

## CI / CD

The repository is managed with GitHub Actions (workflows in the `.github/workflows/`
folder of the GitHub repository): a build & test workflow (badge in
[Readme.md](Readme.md)), NuGet publishing of the `BindOpen.*` packages, npm publishing of
`@bindopen/kernel-dtos`, and the DocFX site publication to GitHub Pages.

Versioning is handled by **GitVersion** (`GitVersion.yml`).

## Contributing

1. Fork the repository and create a feature branch from `main`.
2. Make your changes and add/update tests under `tests/`.
3. Run the full test suite (`dotnet test BindOpen.Kernel.sln`) before pushing.
4. Open a pull request referencing the related issue.

## Support

- [Open an issue](https://github.com/BindOpen/BindOpen.Kernel/issues)
- [Discuss in the GitHub Discussions](https://github.com/BindOpen/BindOpen.Kernel/discussions)
- License: [MIT](LICENSE)