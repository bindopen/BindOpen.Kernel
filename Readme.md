# BindOpen

![BindOpen logo](https://storage.bindopen.org/img/logos/logo_bindopen.png)


## About

BindOpen is a framework that allows you to build widely-extended applications. It enables you to enhance your project with custom script functions, connectors, entities, and tasks.

### BindOpen.System

BindOpen.System is the core of the BindOpen framework. It is composed of the following modules:

* [System.Data](https://github.com/bindopen/BindOpen.System/blob/master/docs/bindopen-system-data.md).
* [System.Scoping](https://github.com/bindopen/BindOpen.System/blob/master/docs/bindopen-system-scoping.md).
* [System.IO](https://github.com/bindopen/BindOpen.System/blob/master/docs/bindopen-system-io.md).

## Use Data

### Metadata

```csharp
var meta = BdoData.NewMeta("host", DataValueTypes.Text, "my-test-host");
```

### Definition

### Configuration

## Use Scoping

### Script

### Connectors

### Entities

### Tasks

## Use IO

## License

This project is licensed under the terms of the [MIT LICENSE](https://github.com/bindopen/BindOpen/blob/master/LICENSE).

## Other repos and Projects

[BindOpen.System](https://github.com/bindopen/BindOpen.System) contains the core BindOpen packages to manage data and scoping.

[BindOpen.System.Hosting](https://github.com/bindopen/BindOpen.System.Hosting) enables you to integrate a BindOpen agent within the .NET service builder.

[BindOpen.System.Logging](https://github.com/bindopen/BindOpen.System.Logging) provides a simple and multidimensional logging system, perfect to monitor nested task executions.

[BindOpen.Labs.Bpm](https://github.com/bindopen/BindOpen.Labs.Bpm) allows you to streamline business processes.

[BindOpen.Labs.Commands](https://github.com/bindopen/BindOpen.Labs.Commands) allows you to manage arguments of application.

[BindOpen.Labs.Databases](https://github.com/bindopen/BindOpen.Labs.Databases) allows you to fluently build your SQL queries whatever the kind of databases you deal with.

[BindOpen.Labs.Forms](https://github.com/bindopen/BindOpen.Labs.Forms) allows you to design and manage forms.


A [full list of all the repos](https://github.com/bindopen?tab=repositories) is available as well.

## Documentation and Further Learning

### [BindOpen Docs](https://docs.bindopen.org/)

The BindOpen Docs are the ideal place to start if you are new to BindOpen. They are categorized in 3 broader topics:

* [Articles](https://docs.bindopen.org/articles) to learn how to use BindOpen;
* [Notes](https://docs.bindopen.org/notes) to follow our releases;
* [Api](https://docs.bindopen.org/api) to have an overview of BindOpen APIs.

### [BindOpen Blog](https://www.bindopen.org/blog)

The BindOpen Blog is where we announce new features, write engineering blog posts, demonstrate proof-of-concepts and features under development.

## NuGet Packages by the BindOpen team

All of our NuGet packages are available from [our NuGet.org profile page](https://www.nuget.org/profiles/bindopen).

## Feedback

If you're having trouble with BindOpen, file a bug on the [BindOpen Issue Tracker](https://github.com/bindopen/BindOpen/issues). 

