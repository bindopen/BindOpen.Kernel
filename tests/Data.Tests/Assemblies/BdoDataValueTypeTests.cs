using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Contains unit tests for data type management and string representation within the BindOpen Kernel.
/// </summary>
/// <remarks>
/// These tests verify that the framework correctly handles various data value types, 
/// including standard text and complex object types, and ensures their string representation 
/// is consistent for logging and serialization.
/// </remarks>
[TestFixture, Order(210)]
public class BdoDataValueTypeTests
{
    /// <summary>
    /// Performs one-time setup for the data value type tests.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    /// <summary>
    /// Tests the string representation of basic and complex data types.
    /// </summary>
    /// <remarks>
    /// This test ensures that <see cref="DataValueTypes.Text"/> and custom classes like <see cref="ClassFake"/> 
    /// are correctly resolved to their expected string descriptors.
    /// </remarks>
    [Test, Order(1)]
    public void ToStringTest()
    {
        // 1. Testing standard Text type
        var dataType = BdoData.NewDataType(DataValueTypes.Text);
        var st = dataType.ToString();
        Assert.That(st, Is.EqualTo(nameof(DataValueTypes.Text)), "The string representation of the Text data type is incorrect.");

        // 2. Testing complex Object type with ClassFake
        dataType = BdoData.NewDataType<ClassFake>();
        st = dataType.ToString();

        // Using the constraint model for clearer failure messages
        Assert.That(st, Does.StartWith("Object, BindOpen.Tests.ClassFake"), "The string representation of the complex Object data type is incorrect.");
    }
}