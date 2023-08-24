# BindOpen.System

![BindOpen logo](https://storage.bindopen.org/img/logos/logo_bindopen.png)

![Github release version](https://img.shields.io/nuget/v/BindOpen.System.Abstractions.svg?style=plastic)


BindOpen is a framework that allows you to build widely-extended applications. It enables you to enhance your .NET projects with custom script functions, connectors, entities, and tasks.

## About

BindOpen.System is the kernel of the BindOpen framework. It is composed of the following modules:

* __System.Data__ offers a comprehensive data model based on metadata.
* __System.Scoping__ offers an effective mechanism for defining and managing your extensions.
* __System.Hosting__ allows you to integrate a BindOpen agent within the .NET service builder.
* __System.Logging__ provides a straightforward and multi-dimensional logging system.
* __System.IO__ provides packages to serialize and deserialize BindOpen.System objects.

This repository contains the System.Data, System.Scoping and System.IO modules.

A [full list of all the repos](https://github.com/bindopen?tab=repositories) is available as well.


## Install

To get started, install the BindOpen.System module you want to use.

Note: Later you will be able to install only the package you need.

### From Visual Studio

| Module | Instruction
|----------|
| [BindOpen.System.Data](https://www.nuget.org/packages/BindOpen.System.Data) | ```PM> Install-Package BindOpen.System.Data```
| [BindOpen.System.Scoping](https://www.nuget.org/packages/BindOpen.System.Scoping) | ```PM> Install-Package BindOpen.System.Scoping```
| [BindOpen.System.IO](https://www.nuget.org/packages/BindOpen.System.IO) | ```PM> Install-Package BindOpen.System.IO```

### From .NET CLI

| Module | Instruction
|----------|
| [BindOpen.System.Data](https://www.nuget.org/packages/BindOpen.System.Data) | ```> dotnet add package BindOpen.System.Data```
| [BindOpen.System.Scoping](https://www.nuget.org/packages/BindOpen.System.Scoping) | ```> dotnet add package BindOpen.System.Scoping```
| [BindOpen.System.IO](https://www.nuget.org/packages/BindOpen.System.IO) | ```> dotnet add package BindOpen.System.IO```

## Get started

### System.Data

```csharp
var meta = BdoData.NewMeta("host", DataValueTypes.Text, "my-test-host");
```

### System.Scoping

```csharp
var scope = BdoScoping.NewScope();
scope.LoadExtensions(q => q.AddAllAssemblies());
```

#### Script

```csharp

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

var exp = "$testFunction('MYTABLE', $text('MYTABLE_'))".ToExpression();
scope.Interpreter.Evaluate<bool?>(exp);
```

#### Connectors

#### Entities

#### Tasks

### System.IO

#### Serialization

```csharp
var meta = JsonHelper.LoadJson<MetaObjectDto>(BdoConnectorFaker.JsonFilePath).ToPoco();
var connector = scope.CreateConnector<ConnectorFake>(meta);
```

#### Deserialization

```csharp
var meta = JsonHelper.LoadJson<MetaObjectDto>(BdoConnectorFaker.JsonFilePath).ToPoco();
var connector = scope.CreateConnector<ConnectorFake>(meta);
```


## License

This project is licensed under the terms of the MIT license. [See LICENSE](https://github.com/bindopen/BindOpen/blob/master/LICENSE).

## Packages

This repository contains the code of the following Nuget packages:

| The package | Provides
|----------|
| [BindOpen.System.Abstractions](https://www.nuget.org/packages/BindOpen.System.Abstractions) | Interfaces and enumerations
| [BindOpen.System.Data](https://www.nuget.org/packages/BindOpen.System.Data) | Core data model
| [BindOpen.System.Scoping](https://www.nuget.org/packages/BindOpen.System.Scoping) | Extension manager
| [BindOpen.System.Scoping.Extensions](https://www.nuget.org/packages/BindOpen.System.Scoping.Extensions) | Classes of extensions
| [BindOpen.System.Scoping.Script](https://www.nuget.org/packages/BindOpen.System.Scoping.Script) | Script interpreter
| [BindOpen.System.IO](https://www.nuget.org/packages/BindOpen.System.IO) | Serialization / Deserialization
| [BindOpen.System.IO.Dtos](https://www.nuget.org/packages/BindOpen.System.IO.Dtos) | Data transfer classes

The atomicity of these packages allows you install only what you need respecting your solution's architecture.

All of our NuGet packages are available from [our NuGet.org profile page](https://www.nuget.org/profiles/bindopen).


## Other repos and Projects

[BindOpen.System.Hosting](https://github.com/bindopen/BindOpen.System.Hosting) enables you to integrate a BindOpen agent within the .NET service builder.

[BindOpen.System.Logging](https://github.com/bindopen/BindOpen.System.Logging) provides a simple and multidimensional logging system, perfect to monitor nested task executions.

[BindOpen.Labs](https://github.com/bindopen/BindOpen.Labs) is a collection of projects based on BindOpen.System.


A [full list of all the repos](https://github.com/bindopen?tab=repositories) is available as well.


## Documentation and Further Learning

### [BindOpen Docs](https://docs.bindopen.org/)

The BindOpen Docs are the ideal place to start if you are new to BindOpen. They are categorized in 3 broader topics:

* [Articles](https://docs.bindopen.org/articles) to learn how to use BindOpen;
* [Notes](https://docs.bindopen.org/notes) to follow our releases;
* [Api](https://docs.bindopen.org/api) to have an overview of BindOpen APIs.

### [BindOpen Blog](https://www.bindopen.org/blog)

The BindOpen Blog is where we announce new features, write engineering blog posts, demonstrate proof-of-concepts and features under development.


## Feedback

If you're having trouble with BindOpen, file a bug on the [BindOpen Issue Tracker](https://github.com/bindopen/BindOpen/issues). 

## Donation

You are welcome to support this project. All donations are optional but are greatly appreciated.

[![Please donate](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?hosted_button_id=PHG3WSUFYSMH4)


