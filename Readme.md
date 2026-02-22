# BindOpen.Kernel

![BindOpen logo](https://storage.bindopen.org/img/logos/logo_bindopen.png)

[![Nuget](https://img.shields.io/nuget/v/BindOpen.Abstractions.svg?style=flat-square)](https://www.nuget.org/packages/BindOpen.Abstractions)
[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)](https://github.com/bindopen/BindOpen.Kernel/blob/master/LICENSE)

BindOpen is a framework that enables the development of highly extensible applications. It allows you to enhance your .NET projects with custom script functions, connectors, entities, and tasks.

## About

**BindOpen.Kernel** is the core module of the BindOpen framework. It enables you to easily model your business context, including data and operations, through a metadata-driven approach.

BindOpen.Kernel is composed of the following modules:

* **Data**: Offers a comprehensive data model based on metadata.
* **Scoping**: Provides an effective mechanism for loading and using your extensions.
* **Dtos.Converters**: Provides packages to serialize and deserialize BindOpen.Kernel objects.
* [**Hosting**](https://github.com/bindopen/BindOpen.Hosting): (External) Allows you to integrate a BindOpen agent within the .NET service builder.
* [**Logging**](https://github.com/bindopen/BindOpen.Logging): (External) Provides a straightforward and multi-dimensional logging system.

This repository contains the **Data**, **Scoping**, and **Dtos.Converters** modules.

## Install

To get started, install the specific BindOpen.Kernel module you need.

### From Visual Studio (Package Manager)

| Module | Instruction |
|--------|-----|
| [BindOpen.Data](https://www.nuget.org/packages/BindOpen.Data) | `PM> Install-Package BindOpen.Data` |
| [BindOpen.Scoping](https://www.nuget.org/packages/BindOpen.Scoping) | `PM> Install-Package BindOpen.Scoping` |
| [BindOpen.Dtos.Converters](https://www.nuget.org/packages/BindOpen.Dtos.Converters) | `PM> Install-Package BindOpen.Dtos.Converters` |

### From .NET CLI

| Module | Instruction |
|--------|-----|
| [BindOpen.Data](https://www.nuget.org/packages/BindOpen.Data) | `dotnet add package BindOpen.Data` |
| [BindOpen.Scoping](https://www.nuget.org/packages/BindOpen.Scoping) | `dotnet add package BindOpen.Scoping` |
| [BindOpen.Dtos.Converters](https://www.nuget.org/packages/BindOpen.Dtos.Converters) | `dotnet add package BindOpen.Dtos.Converters` |

---

## Get Started

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

### Dtos.Converters

Serialize and deserialize BindOpen objects to/from JSON and XML.

```csharp
// Serialization
var metaSet = BdoData.NewNode("test-io").With(("host", "host-test"), ("address", "0.0.0.0"));
metaSet.ToDto().SaveXml("output.xml");

// Deserialization
var loadedSet = JsonHelper.LoadJson<MetaNodeDto>("output.json").ToPoco();
```

---

## Nuget Packages

This repository contains the code for the following NuGet packages:

| Package | Provision |
|----------|-----|
| [BindOpen.Abstractions](https://www.nuget.org/packages/BindOpen.Abstractions) | Interfaces and enumerations |
| [BindOpen.Data](https://www.nuget.org/packages/BindOpen.Data) | Core data model |
| [BindOpen.Scoping](https://www.nuget.org/packages/BindOpen.Scoping) | Extension manager |
| [BindOpen.Scoping.Script](https://www.nuget.org/packages/BindOpen.Scoping.Script) | Script interpreter |
| [BindOpen.Dtos.Converters](https://www.nuget.org/packages/BindOpen.Dtos.Converters) | Serialization / Deserialization |
| [BindOpen.Dtos](https://www.nuget.org/packages/BindOpen.Dtos) | Data transfer classes |

The atomicity of these packages allows you to install only what you need, respecting your solution's architecture. All our NuGet packages are available on [our NuGet.org profile page](https://www.nuget.org/profiles/bindopen).

---

## Documentation and Further Learning

### [BindOpen Docs](https://docs.bindopen.org/)
The BindOpen Docs are the ideal place to start. They are categorized into three broad topics:
* [**Articles**](https://docs.bindopen.org/articles): Learn how to use BindOpen through tutorials.
* [**Notes**](https://docs.bindopen.org/notes): Stay updated with our latest releases.
* [**API Reference**](https://docs.bindopen.org/api): Explore the technical details of BindOpen APIs.

### [BindOpen Blog](https://www.bindopen.org/blog)
The BindOpen Blog is where we announce new features, engineering deep dives, and proof-of-concepts.

---

## Feedback
If you encounter any issues, please file a bug on the [BindOpen Issue Tracker](https://github.com/bindopen/BindOpen/issues).

## Donation
Support this project! Donations are optional but greatly appreciated.

[![Please donate](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?hosted_button_id=PHG3WSUFYSMH4)

## License
This project is licensed under the terms of the MIT license. [See LICENSE](https://github.com/bindopen/BindOpen.Kernel/blob/master/LICENSE).

