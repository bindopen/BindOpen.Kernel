using BindOpen.Data.Meta.Reflection;
using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for <see cref="IBdoMetaObject"/>, focusing on object-to-meta mapping and reflection.
/// </summary>
/// <remarks>
/// These tests ensure that POCO objects can be converted into BindOpen metadata objects 
/// and that the underlying data remains consistent throughout the transformation process.
/// </remarks>
[TestFixture, Order(201)]
public class BdoMetaObjectTests
{
    private object _obj = null;

    /// <summary>
    /// The meta-object instance resulting from the conversion, accessible for cross-test validation.
    /// </summary>
    public IBdoMetaObject _metaObject;

    /// <summary>
    /// Initializes the source POCO object using a faker.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _obj = ClassObjectFaker.Fake();
    }

    /// <summary>
    /// Internal validation method to ensure the data inside the meta-object 
    /// matches the original source object.
    /// </summary>
    private void Test()
    {
        var resultObj = _metaObject?.GetData();

        // Using FluentAssertions to perform a deep comparison
        resultObj.Should().BeEquivalentTo(_obj, "The data retrieved from the meta-object must match the original POCO data.");
    }

    /// <summary>
    /// Tests the conversion of a POCO to a meta-object using the extension method syntax.
    /// </summary>
    [Test, Order(1)]
    public void ToMetaTest()
    {
        _metaObject = _obj.ToMeta<BdoMetaObject>();
        Test();
    }

    /// <summary>
    /// Tests the creation of a meta-object using different factory method patterns.
    /// </summary>
    [Test, Order(2)]
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