# BindOpen.Scoping

![BindOpen logo](https://storage.bindopen.org/img/logos/logo_bindopen.png)

![Github release version](https://img.shields.io/nuget/v/BindOpen.Abstractions.svg?style=plastic)


BindOpen is a framework that enables the development of highly extensible applications. It allows you to enhance your .NET projects with custom script functions, connectors, entities, and tasks.

## About

BindOpen.Kernel is the core module of the BindOpen framework. It enables you to easily model your business context, including data and operations.

BindOpen.Kernel is composed of the following modules:

* __Data__ offers a comprehensive data model based on metadata.
* __Scoping__ offers an effective mechanism for loading and using your extensions.
* [Hosting](https://github.com/bindopen/BindOpen.Hosting) allows you to integrate a BindOpen agent within the .NET service builder.
* [Logging](https://github.com/bindopen/BindOpen.Logging) provides a straightforward and multi-dimensional logging system.
* __Dtos.Converters__ provides packages to serialize and deserialize BindOpen.Kernel objects.

This repository contains the Data, Scoping and Dtos.Converters modules. The two other ones are separate repositories.

A [full list of all the BindOpen repos](https://github.com/bindopen?tab=repositories) is available as well.


## Install

To get started, install the BindOpen.Kernel module you want to use.

Note: We recommend that later on, you install only the package you need.

### From Visual Studio

| Module | Instruction |
|--------|-----|
| [BindOpen.Data](https://www.nuget.org/packages/BindOpen.Data) | ```PM> Install-Package BindOpen.Data``` |
| [BindOpen.Scoping](https://www.nuget.org/packages/BindOpen.Scoping) | ```PM> Install-Package BindOpen.Scoping``` |
| [BindOpen.Dtos.Converters](https://www.nuget.org/packages/BindOpen.Dtos.Converters) | ```PM> Install-Package BindOpen.Dtos.Converters``` |

### From .NET CLI

| Module | Instruction |
|--------|-----|
| [BindOpen.Data](https://www.nuget.org/packages/BindOpen.Data) | ```> dotnet add package BindOpen.Data``` |
| [BindOpen.Scoping](https://www.nuget.org/packages/BindOpen.Scoping) | ```> dotnet add package BindOpen.Scoping``` |
| [BindOpen.Dtos.Converters](https://www.nuget.org/packages/BindOpen.Dtos) | ```> dotnet add package BindOpen.Dtos.Converters``` |

## Get started

### Data

__Data__ package offers a comprehensive data model based on metadata.

#### Metadata

Define your data context using metadata.

```csharp
var meta = BdoData.NewMeta("host", DataValueTypes.Text, "my-test-host");
```

#### Configuration

Your configurations can, for instance, be defined as a set of metadata, allowing flexible and declarative setups.

```csharp
var config = BdoData.NewConfig(
        "test-config",
        BdoData.NewScalar("comment", DataValueTypes.Text, "Sunny day"),
        BdoData.NewScalar("temperature", DataValueTypes.Integer, 25, 26, 26),
        BdoData.NewScalar("date", DataValueTypes.Date, DateTime.Now),
        BdoData.NewNode(
            "subscriber"
            BdoData.NewScalar("name", DataValueTypes.Text, "Ernest E."),
            BdoData.NewScalar("code", DataValueTypes.Integer, 1560))
    )
    .WithTitle("Example of configuration")
    .WithDescription(("en", "This is an example of description"))
```

### Scoping

__Scoping__ package offers an effective mechanism for loading and using your extensions.

```csharp
var scope = BdoScoping.NewScope()
    .LoadExtensions(q => q.AddAllAssemblies());
```

#### Script

Functions can be executed seamlessly using BindOpen's scripting capabilities.

```csharp
// Define your script functions

[BdoFunction(
    Name = "testFunction",
    Description = "Returns true if second string parameter is the first one ending with underscore")]
public static object Fun_Func2a(
    this string st1,
    string st2)
{
    return st1 == st2 + "_";
}

...

// Interprete script

var exp = "$testFunction('MYTABLE', $text('MYTABLE_'))";
var result = scope.Interpreter.Evaluate<bool?>(exp);
// result is True
```

#### Tasks

```csharp

[BdoTask("taskFake")]
public class TaskFake : BdoTask
{
    [BdoProperty(Name = "boolValue")]
    public bool BoolValue { get; set; }

    [BdoOutput(Name = "stringValue")]
    public string StringValue { get; set; }

    [BdoInput(Name = "enumValue")]
    public ActionPriorities EnumValue { get; set; }

    ...

    public override Task<bool> ExecuteAsync(
        CancellationToken token,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        RuntimeModes runtimeMode = RuntimeModes.Normal,
        IBdoLog log = null)
    {
        ...

        Debug.WriteLine("Task completed");

        return Task.FromResult(true);
    }
}

...

var meta = BdoData.NewObject()
    .WithDataType(BdoExtensionKinds.Task, "scoping.tests$taskFake")
    .WithProperties(("boolValue", false))
    .WithInputs(BdoData.NewScalar("enumValue", ActionPriorities.Low))
    .WithOutputs(("stringValue", "test-out"));
                    
var task = scope.CreateTask<TaskFake>(meta);
var cancelToken = new CancellationTokenSource();
task.Execute(cancelToken.Token, scope);
```

### Dtos.Converters

__Dtos.Converters__ provides packages to serialize and deserialize BindOpen.Kernel objects.

#### Serialization

Serialize your models to JSON and XML with minimal configuration.

```csharp
var metaSet = BdoData.NewSet("test-io").With(("host", "host-test"), ("address", "0.0.0.0"));
metaSet.ToDto().SaveXml("output.xml");
```

#### Deserialization

Models can be easily deserialized from JSON and XML formats.

```csharp
var metaSet = JsonHelper.LoadJson<MetaSetDb>("output.xml").ToPoco();
```

## License

This project is licensed under the terms of the MIT license. [See LICENSE](https://github.com/bindopen/BindOpen.Kernel/blob/master/LICENSE).

## Packages

This repository contains the code of the following Nuget packages:

| Package | Provision |
|----------|-----|
| [BindOpen.Abstractions](https://www.nuget.org/packages/BindOpen.Abstractions) | Interfaces and enumerations |
| [BindOpen.Data](https://www.nuget.org/packages/BindOpen.Data) | Core data model |
| [BindOpen.Scoping](https://www.nuget.org/packages/BindOpen.Scoping) | Extension manager |
| [BindOpen.Scoping.Extensions](https://www.nuget.org/packages/BindOpen.Scoping.Extensions) | Classes of extensions |
| [BindOpen.Scoping.Script](https://www.nuget.org/packages/BindOpen.Scoping.Script) | Script interpreter |
| [BindOpen.Dtos.Converters](https://www.nuget.org/packages/BindOpen.Dtos) | Serialization / Deserialization |
| [BindOpen.Dtos](https://www.nuget.org/packages/BindOpen.Dtos.Dbs) | Data transfer classes |

The atomicity of these packages allows you install only what you need respecting your solution's architecture.

All of our NuGet packages are available from [our NuGet.org profile page](https://www.nuget.org/profiles/bindopen).


## Other repos and Projects

[BindOpen.Hosting](https://github.com/bindopen/BindOpen.Hosting) enables you to integrate a BindOpen agent within the .NET service builder.

[BindOpen.Logging](https://github.com/bindopen/BindOpen.Logging) provides a simple and multidimensional logging system, perfect to monitor nested task executions.

[BindOpen.Labs](https://github.com/bindopen/BindOpen.Labs) is a collection of projects based on BindOpen.


A [full list of all the repos](https://github.com/bindopen?tab=repositories) is available as well.


## Documentation and Further Learning

### [BindOpen Docs](https://docs.bindopen.org/)

The BindOpen Docs are the ideal place to start if you are new to BindOpen. They are categorized in 3 broader topics:

* [Articles](https://docs.bindopen.org/articles) to learn how to use BindOpen;
* [Notes](https://docs.bindopen.org/notes) to follow our releases;
* [Api](https://docs.bindopen.org/api) to have an overview of BindOpen APIs.

### [BindOpen Blog](https://www.bindopen.org/blog)

The BindOpen Blog is where we announce new features, write engineering blog posts, demonstrate proof-of-concepts and features under development.


## Feed back

If you're having trouble with BindOpen, file a bug on the [BindOpen Issue Tracker](https://github.com/bindopen/BindOpen/issues). 

## Donation

You are welcome to support this project. All donations are optional but are greatly appreciated.

[![Please donate](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?hosted_button_id=PHG3WSUFYSMH4)

