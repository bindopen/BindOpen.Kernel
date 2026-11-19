using BindOpen.Data;
using BindOpen.Data.Meta;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class provides factory methods to generate randomized data, 
/// create Meta-Objects, and perform assertions for Task testing.
/// </summary>
public static class BdoTaskFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Task.xml";
    public static readonly string JsonFilePath = DataTestData.WorkingFolder + "Task.json";

    /// <summary>
    /// Generates a randomized data set using the Bogus library.
    /// </summary>
    /// <returns>An anonymous object containing randomized task parameters.</returns>
    public static dynamic NewData()
    {
        var f = new Faker();
        return new
        {
            boolValue = f.Random.Bool(),
            intValue = f.Random.Int(1, 800),
            enumValue = AccessibilityLevels.Private,
            stringValue = f.Lorem.Word()
        };
    }

    /// <summary>
    /// Creates a new BdoMetaObject configured as a Task extension.
    /// Distinguishes between Properties, Inputs, and Outputs for specialized task handling.
    /// </summary>
    /// <param name="data">The source data. If null, fresh data is generated.</param>
    /// <returns>An IBdoMetaObject representing a Task.</returns>
    public static IBdoMetaObject NewMetaObject(dynamic data = null)
    {
        data ??= NewData();

        // Define the meta-object with the 'Task' extension kind
        var meta = BdoData.NewObject()
            .WithDataType(BdoExtensionKinds.Task, "scoping.tests$taskFake")
            .WithProperties(
                ("boolValue", data.boolValue as bool?),
                ("intValue", data.intValue as int?))
            .WithInputs(
                ("enumValue", data.enumValue))
            .WithOutputs(
                ("stringValue", data.stringValue as string));

        return meta;
    }

    /// <summary>
    /// Validates that a Task instance correctly matches the reference data.
    /// </summary>
    /// <param name="task">The instantiated task to validate.</param>
    /// <param name="reference">The original data used for creation.</param>
    public static void AssertFake(TaskFake task, dynamic reference)
    {
        Assert.That(task, Is.Not.Null, "The Task instance is missing.");

        // Standardized assertions using NUnit constraints
        Assert.That(task.BoolValue, Is.EqualTo(reference.boolValue), "Task Boolean property mismatch.");
        Assert.That(task.EnumValue.ToString(), Is.EqualTo(reference.enumValue.ToString()), "Task Enum input mismatch.");
        Assert.That(task.IntValue, Is.EqualTo(reference.intValue), "Task Integer property mismatch.");
        Assert.That(task.StringValue, Is.EqualTo(reference.stringValue), "Task String output mismatch.");
    }
}