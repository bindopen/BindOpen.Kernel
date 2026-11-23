using BindOpen.Data.Meta.Reflection;
using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for <see cref="IBdoMetaObject"/>, focusing on object-to-metadata mapping and reflection.
/// </summary>
/// <remarks>
/// These tests verify that POCO instances can be converted into BindOpen metadata objects
/// via extension methods or factory builders, and that their underlying data remains intact
/// throughout the transformation process.
/// </remarks>
[TestFixture]
public class BdoMetaObjectTests
{
    private object _obj = null;

    /// <summary>
    /// Gets or sets the <see cref="IBdoMetaObject"/> instance produced during test execution.
    /// </summary>
    public IBdoMetaObject _metaObject;

    /// <summary>
    /// Initializes the source POCO instance with randomized test data before executing tests.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _obj = ClassObjectFaker.Fake();
    }

    /// <summary>
    /// Validates that the data extracted from <see cref="_metaObject"/> is structurally equivalent
    /// to the original <see cref="_obj"/> source instance.
    /// </summary>
    private void Test()
    {
        var resultObj = _metaObject?.GetData();

        // Using FluentAssertions to perform a deep comparison
        resultObj.Should().BeEquivalentTo(_obj, "The data retrieved from the meta-object must match the original POCO data.");
    }

    /// <summary>
    /// Verifies that a POCO can be converted into a <see cref="BdoMetaObject"/> using the <c>ToMeta</c> extension method.
    /// </summary>
    [Test]
    public void ToMetaTest()
    {
        _metaObject = _obj.ToMeta<BdoMetaObject>();
        Test();
    }

    /// <summary>
    /// Verifies that an <see cref="IBdoMetaObject"/> can be instantiated and populated using direct and fluent factory methods.
    /// </summary>
    [Test]
    public void NewTest()
    {
        // 1. Direct factory creation
        _metaObject = BdoData.NewObject(_obj);
        Test();

        // 2. Fluent configuration pattern
        _metaObject = BdoData.NewObject().WithData(_obj);
        Test();
    }
}