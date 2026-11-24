
# BindOpen.Kernel

![BindOpen logo](https://storage.bindopen.org/img/logos/logo_bindopen.png)

[![Nuget](https://img.shields.io/nuget/v/BindOpen.Abstractions.svg?style=flat-square)](https://www.nuget.org/packages/BindOpen.Abstractions)
[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)](https://github.com/bindopen/BindOpen.Kernel/blob/master/LICENSE)

**BindOpen.Kernel** is the foundational toolkit of the BindOpen platform. It defines the
core data model, the scope & extension system, event logging, application hosting, relational
persistence, and the data transfer objects (DTOs) shared by all BindOpen modules.

---

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
| `BindOpen.Logging.Relational` | Relational persistence of logs (EF Core, AutoMapper). |
| `BindOpen.Relational` | Relational persistence of the core data model (Entity Framework Core). |
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
dotnet add package BindOpen.Logging.Relational
dotnet add package BindOpen.Relational
dotnet add package BindOpen.Hosting

# JavaScript / TypeScript
npm install @bindopen/kernel-dtos

```

## Quick Start

### Data

The **Data** package offers a metadata-driven model for defining configurations and objects.

#### Metadata
Define your data context using metadata.
```csharp
var meta = BdoData.NewMeta("host", DataValueTypes.Text, "my-test-host");
```

#### Configuration

Define flexible, declarative setups with nested nodes and scalars.

```csharp
using static BindOpen.Data.BdoData;

var config = NewConfig("test-config",
        NewScalar("comment", "Sunny day"),
        NewScalar("temperature", 25, 26, 26),
        NewScalar("date", DateTime.Now),
        NewNode("subscriber",
            NewScalar("name", "Ernest E."),
            NewScalar("code", 1560))
    )
    .WithTitle("Example of configuration")
    .WithDescription(("en", "This is an example of description"));
```

### Scoping

The Scoping package handles the loading and resolution of your extensions (Functions, Tasks, etc.).

```csharp
var scope = BdoScoping.NewScope()
    .LoadExtensions(q => q.AddAllAssemblies());
```

#### Script

Execute custom logic via BindOpen's scripting engine.

```csharp
[BdoFunction(
    Name = "testFunction",
    Description = "Returns true if second parameter matches first parameter + underscore")]
public static object Fun_Func2a(this string st1, string st2)
{
    return st1 == st2 + "_";
}

// Evaluate script
var exp = "$testFunction('MYTABLE', $text('MYTABLE_'))";
var result = scope.Interpreter.Evaluate<bool?>(exp); // Result: True
```

#### Tasks

Define and execute automated logic with automated property mapping.

```csharp

[BdoTask("taskFake")]
public class TaskFake : BdoTask
{
    [BdoProperty(Name = "boolValue")]
    public bool BoolValue { get; set; }

    [BdoOutput(Name = "stringValue")]
    public string StringValue { get; set; }

    public override Task<bool> ExecuteAsync(
        CancellationToken token,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        RuntimeModes runtimeMode = RuntimeModes.Normal,
        IBdoLog log = null)
    {
        Debug.WriteLine("Task completed");
        return Task.FromResult(true);
    }
}

// Instantiation and execution
var meta = BdoData.NewObject()
    .WithDataType(BdoExtensionKinds.Task, "scoping.tests$taskFake")
    .WithProperties(("boolValue", false))
    .WithOutputs(("stringValue", "test-out"));
                    
var task = scope.CreateTask<TaskFake>(meta);
task.Execute(new CancellationToken(), scope);
```

### Serialize and Deserialize (XML & JSON)

Serialize and deserialize BindOpen objects to/from JSON and XML.

```csharp
// Serialization
var metaSet = BdoData.NewNode("test-io").With(("host", "host-test"), ("address", "0.0.0.0"));
metaSet.ToDto().SaveXml("output.xml");

// Deserialization
var loadedSet = JsonHelper.LoadJson<MetaSetDto>("output.json").ToPoco();
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

### Hosting

`BdoHosting` bootstraps the application environment, known paths, and configuration:

```csharp
using BindOpen.Hosting;

var host = BdoHosting.NewHost(
    options => options.ThrowExceptionOnInitFailure());

host.Start();

```

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

## Documentation and Further Learning

### [BindOpen Docs](https://docs.bindopen.org/)
The BindOpen Docs are the ideal place to start. They are categorized into three broad topics:
* [**Articles**](https://docs.bindopen.org/articles): Learn how to use BindOpen through tutorials.
* [**Notes**](https://docs.bindopen.org/notes): Stay updated with our latest releases.
* [**API Reference**](https://docs.bindopen.org/api): Explore the technical details of BindOpen APIs.

### [BindOpen Blog](https://www.bindopen.org/blog)
The BindOpen Blog is where we announce new features, engineering deep dives, and proof-of-concepts.

## Feedback
If you encounter any issues, please file a bug on the [BindOpen Issue Tracker](https://github.com/bindopen/BindOpen/issues).

## Donation
Support this project! Donations are optional but greatly appreciated.

[![Please donate](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?hosted_button_id=PHG3WSUFYSMH4)

## License
This project is licensed under the terms of the MIT license. [See LICENSE](https://github.com/bindopen/BindOpen.Kernel/blob/master/LICENSE).
