
# BindOpen.Kernel

[![Build](https://github.com/BindOpen/BindOpen.Kernel/actions/workflows/build-bindopen-kernel.yml/badge.svg?branch=main)](https://github.com/BindOpen/BindOpen.Kernel/actions/workflows/build-bindopen-kernel.yml)

**BindOpen.Kernel** is the foundational toolkit of the BindOpen platform. It defines the
core data model, the scope & extension system, event logging, application hosting, relational
persistence, and the data transfer objects (DTOs) shared by all BindOpen modules.

## What's Inside

| Package | Description |
|---------|-------------|
| `BindOpen.Data` | Core data model: meta sets, meta objects, scalars, dictionaries, schemas, mergers, assemblies/class references, depots, references and conditions. |
| `BindOpen.Scoping` | Scope management: `BdoScoping.NewScope()`, extension loading (`LoadExtensions`), entities, and connectors. |
| `BindOpen.Scoping.Extensions` | Extension system: tasks (`scope.CreateTask(...)`), extension kinds and options. |
| `BindOpen.Scoping.Script` | Scripting engine: `BdoScript`, scriptwords, interpreter and script tasks. |
| `BindOpen.Dtos` | DTO classes (`*Dto`) and JSON / XML / text serialization (`ToDto`, `ToPoco`, `SaveJson`, `SaveXml`, `JsonHelper`, `XmlHelper`). |
| `BindOpen.Dtos.Xsd` | XSD schema generation for the DTO model. |
| `BindOpen.Logging` | Event logging: `BdoLogging.NewLog()`, log events, hierarchical sub-logs, handlers and filters. |
| `BindOpen.Logging.Dtos` | DTOs (`LogDto`) and JSON / XML serialization for logged events and log queries. |
| `BindOpen.Relational` | Relational persistence of the core data model (Entity Framework Core). |
| `BindOpen.Relational.Models` | Entity models and mapping for the relational layer. |
| `BindOpen.Logging.Relational` | Relational persistence of logs (EF Core, AutoMapper). |
| `BindOpen.Logging.Relational.Models` | Entity models for log persistence. |
| `BindOpen.Hosting` | Application hosting: `BdoHosting.NewHost(...)`, known host paths, and configuration binding. |

A TypeScript package is also published from this repository:

| Package | Description |
|---------|-------------|
| `@bindopen/kernel-dtos` | Typed DTO classes for JavaScript/TypeScript consumers (ESM + CJS), with `./data`, `./logging` and `./scoping` subpath exports. |

## Requirements

- .NET **11.0** or later
- [NuGet](https://www.nuget.org) package manager (or your SDK's equivalent)
- [Node.js](https://nodejs.org) + [npm](https://www.npmjs.com) for the TypeScript package

## Installation

```bash
# .NET (any project)
dotnet add package BindOpen.Data
dotnet add package BindOpen.Scoping
dotnet add package BindOpen.Scoping.Extensions
dotnet add package BindOpen.Scoping.Script
dotnet add package BindOpen.Dtos
dotnet add package BindOpen.Logging
dotnet add package BindOpen.Logging.Dtos
dotnet add package BindOpen.Relational
dotnet add package BindOpen.Relational.Models
dotnet add package BindOpen.Logging.Relational
dotnet add package BindOpen.Logging.Relational.Models
dotnet add package BindOpen.Hosting

# JavaScript / TypeScript
npm install @bindopen/kernel-dtos

```

## Quick Start

### Initialize a Host

`BdoHosting` bootstraps the application environment, known paths, and configuration:

```csharp
using BindOpen.Hosting;

var host = BdoHosting.NewHost(
    options => options.ThrowExceptionOnInitFailure());

host.Start();

```

### Create a Scope and Load Extensions

A *scope* is the runtime container in which BindOpen extensions (tasks, entities, connectors, converters...)
are loaded and executed:

```csharp
using BindOpen.Data;
using BindOpen.Scoping;
using BindOpen.Scoping.Extensions;

// 1. Create an empty scope.
var scope = BdoScoping.NewScope();

// 2. Load the BindOpen extensions into it.
scope.LoadExtensions(opts => opts.AddAssemblyFrom<BdoScoping>());

// ... or load all available BindOpen extension assemblies at once:
scope.LoadExtensions(opts => opts.AddAllAssemblies());

```

### Create Data

Data is described as *meta data* (meta sets, objects, scalars, dictionaries, schemas):

```csharp
using BindOpen.Data;

// A simple scalar value.
var host = BdoData.NewMeta("host", DataValueTypes.Text, "my-test-host");

// A configuration block made of named scalars.
var config = BdoData.NewConfig(
    "app-config",
    BdoData.NewScalar("comment", DataValueTypes.Text, "Sunny day"),
    BdoData.NewScalar("temperature", DataValueTypes.Integer, 25));

// A named set of named values.
var users = BdoData.NewSet(
    "users",
    ("name", "Ernest E."),
    ("code", 1560));

```

### Logging

Structured and hierarchical logs are created with `BdoLogging`:

```csharp
using BindOpen.Data;
using BindOpen.Logging;

var log = BdoLogging.NewLog()
    .WithDetail(("environment", DataValueTypes.Text, "production"));

log.AddMessage("Process started");
log.AddWarning("Low disk space");
log.AddError("Operation failed");

```

### Run a Task

Tasks are BindOpen extensions instantiated inside a scope:

```csharp
using BindOpen.Data;
using BindOpen.Logging;
using BindOpen.Scoping.Extensions;

var log = BdoLogging.NewLog();

// By unique name of a previously loaded task extension.
var task = scope.CreateTask("task-unique-name", config, log: log);

// Or by strongly-typed task interface.
IBdoTask typed = scope.CreateTask<IMyTask>(config, log: log);

```

### Serialize and Deserialize (XML & JSON)

Domain objects convert cleanly to `*Dto` models and back via `ToDto()` and `ToPoco()`:

```csharp
using BindOpen.Data;

// Meta model -> DTO -> XML / JSON file.
users.ToDto().SaveXml("output.xml");
users.ToDto().SaveJson("output.json");

// XML / JSON file -> DTO -> meta model.
var metaSetFromXml = XmlHelper.LoadXml<MetaSetDto>("output.xml").ToPoco();
var metaSetFromJson = JsonHelper.LoadJson<MetaSetDto>("output.json").ToPoco();

```

See [bindopen-kernel-dtos](https://www.google.com/search?q=docs/bindopen-kernel-dtos.md) for the full DTO guide
(JSON, XML and text formats).

### TypeScript

```ts
import { MetaSetDto } from '@bindopen/kernel-dtos/data';

const metaSet: MetaSetDto = {
  name: 'users',
  metas: [
    { name: 'name', type: 'Text', values: ['Ernest E.'] },
    { name: 'code', type: 'Integer', values: ['1560'] },
  ],
};

```

## Running Tests

The solution uses **NUnit** across dedicated test projects (`BindOpen.Data.Tests`, `BindOpen.Scoping.Tests`,
`BindOpen.Logging.Tests`, `BindOpen.Hosting.Tests`, `BindOpen.Dtos.Tests`, etc.) covering domain creation,
JSON/XML round-trip serialization, and relational persistence:

```bash
dotnet test

```

## Documentation

* [BindOpen.Kernel architecture](https://www.google.com/search?q=docs/bindopen-kernel-achitecture.md)
* [BindOpen.Kernel Data](https://www.google.com/search?q=docs/bindopen-kernel-data.md)
* [BindOpen.Kernel DTOs](https://www.google.com/search?q=docs/bindopen-kernel-dtos.md)
* [BindOpen.Kernel Scoping](https://www.google.com/search?q=docs/bindopen-kernel-scoping.md)
* [BindOpen.Kernel Relational](https://www.google.com/search?q=docs/bindopen-kernel-relational.md)
* [API Reference (DocFX)](https://bindopen.github.io/BindOpen.Kernel/)
* [SOLUTION.md](SOLUTION.md) — repository layout, solution projects and build workflow

## Contributing

Contributions are welcome! To report bugs or request features, please
[open an issue](https://github.com/BindOpen/BindOpen.Kernel/issues) on GitHub.
For the full contribution guide (development setup, testing, code style), see [SOLUTION.md](SOLUTION.md).

## License

MIT License — see [LICENSE](https://www.google.com/search?q=LICENSE) for details.
