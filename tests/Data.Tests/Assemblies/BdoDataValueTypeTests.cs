using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Contains unit tests for data type management and string representation within the BindOpen Kernel.
/// </summary>
/// <remarks>
/// These tests verify that the framework correctly resolves various <see cref="DataValueTypes"/>,
/// including primitive text types and complex object types, and ensures their string
/// representations remain consistent for logging and serialization.
/// </remarks>
[TestFixture]
public class BdoDataValueTypeTests
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    /// <summary>
    /// Verifies the string representation generated for both primitive and complex data types.
    /// </summary>
    /// <remarks>
    /// Ensures that <see cref="DataValueTypes.Text"/> resolves to its enum identifier and that
    /// custom object types such as <see cref="ClassFake"/> include their fully qualified type descriptor.
    /// </remarks>
    [Test]
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